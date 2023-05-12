using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace SpirvReflector
{
    /// <summary>
    /// Provides access to SPIR-V shader reflection by parsing the shader bytecode.
    /// </summary>
    /// <remarks>For SPIR-V specificational information see the following:
    /// <para>Main specification: https://registry.khronos.org/SPIR-V/specs/unified1/SPIRV.html#_magic_number</para>
    /// <para>Physical/Data layout: https://registry.khronos.org/SPIR-V/specs/unified1/SPIRV.html#PhysicalLayout</para>
    /// </remarks>
    public unsafe class SpirvReflection
    {
        IReflectionLogger _log;
        SpirvDef _def;
        Dictionary<SpirvOpCode, SpirvInstructionDef> _defInstructions;

        public SpirvReflection(IReflectionLogger log)
        {
            _log = log;
            _defInstructions = new Dictionary<SpirvOpCode, SpirvInstructionDef>();

            Stream stream = TryGetEmbeddedStream("spirv.core.grammar.json", typeof(SpirvInstructionDef).Assembly);

            // TODO Parse all json files in the embedded SpirvReflector.Maps folder.

            if (stream != null)
            {
                string json = null;
                using (StreamReader reader = new StreamReader(stream))
                    json = reader.ReadToEnd();

                stream.Dispose();

                if (!string.IsNullOrWhiteSpace(json))
                {
                    try
                    {
                        _def = JsonConvert.DeserializeObject<SpirvDef>(json);
                        foreach (SpirvInstructionDef inst in _def.Instructions)
                        {
                            if (!_defInstructions.TryAdd((SpirvOpCode)inst.Opcode, inst))
                                Console.WriteLine($"Skipping duplicate opcode definition: {inst.OpName} ({inst.Opcode})");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to parse SPIR-V opcode definitions: {ex.Message}");
                    }
                }
            }
            else
            {
                Debug.WriteLine($"SPIR-V opcode definition file not found.");
            }
        }

        /// <summary>
        /// Attempts to open an embedded resource file and returns a <see cref="Stream"/> if successful.
        /// </summary>
        /// <param name="filename">The name of the embedded resource to be loaded.</param>
        /// <param name="assembly">The assembly from which to retrieve a stream. If null, <see cref="Assembly.GetExecutingAssembly()"/> will be used.</param>
        /// <returns></returns>
        private Stream TryGetEmbeddedStream(string filename, Assembly assembly = null)
        {
            if (assembly == null)
                assembly = Assembly.GetExecutingAssembly();

            Stream stream = null;
            try
            {
                stream = assembly.GetManifestResourceStream($"SpirvReflector.Maps.{filename}");
            }
            finally { }

            return stream;
        }

        public SpirvReflectionResult Reflect(void* byteCode, nuint numBytes)
        {
            SpirvStream stream = new SpirvStream(byteCode, numBytes);
            SpirvVersion version = (SpirvVersion)stream.ReadWord();
            uint generator = stream.ReadWord();
            uint bound = stream.ReadWord();
            uint schema = stream.ReadWord();

            SpirvReflectionResult result = new SpirvReflectionResult(ref version, generator, bound, schema);
            List<SpirvInstruction> instructions = ReadInstructions(stream);

            _log.WriteLine("");
            result.SetInstructions(instructions, _log);

            return result;
        }

        private List<SpirvInstruction> ReadInstructions(SpirvStream stream)
        {
            List<SpirvInstruction> instructions = new List<SpirvInstruction>();

            uint instID = 0;
            _log.WriteLine("Raw:", ConsoleColor.Green);
            while (!stream.IsEndOfStream)
            {
                SpirvInstruction inst = stream.ReadInstruction();
                instructions.Add(inst);

                if (_defInstructions.TryGetValue(inst.OpCode, out SpirvInstructionDef def))
                {
                    foreach (SpirvOperandDef opDef in def.Operands)
                    {
                        // Skip optional operands if they are not present.
                        if ((opDef.Quantifier == "?" || opDef.Quantifier == "*") && inst.UnreadWordCount == 0)
                            continue;

                        // Check if the type is an enum.
                        string wordTypeName = $"Spirv{opDef.Kind}";
                        Type t = GetWordType(wordTypeName);
                        if (t.IsEnum)
                        {
                            Type tGeneric = GetWordType("SpirvWord`1");
                            t = tGeneric.MakeGenericType(t);
                        }

                        if (t != null)
                        {
                            SpirvWord word = Activator.CreateInstance(t) as SpirvWord;
                            word.Name = opDef.Name;

                            if (word is SpirvIdResult resultID)
                                inst.Result = resultID;

                            inst.Operands.Add(word);
                            word.Read(inst);
                        }
                        else
                        {
                            _log.Warning($"Unknown word type: {wordTypeName}");
                        }
                    }

                    string opResult = inst.Result != null ? $"{inst.Result} = " : "";
                    if (inst.Operands.Count > 0)
                    {
                        string operands = GetOperandString(inst);
                        _log.WriteLine($"I_{instID}: {opResult}{inst.OpCode} -- {operands}");
                    }
                    else
                    {
                        _log.WriteLine($"I_{instID}: {opResult}{inst.OpCode}");
                    }
                }
                else
                {
                    _log.Warning($"I_{instID}: Unknown opcode '{inst.OpCode}' ({(uint)inst.OpCode}).");
                }

                instID++;
            }

            return instructions;
        }

        internal static string GetOperandString(SpirvInstruction instruction)
        {
            string result = "";
            for(int i = 0; i < instruction.Operands.Count; i++)
            {
                if (instruction.Operands[i] is SpirvIdResult)
                    continue;

                if (result.Length > 0)
                    result += ", ";

                result += instruction.Operands[i].ToString();
            }

            return result;
        }

        private Type GetWordType(string typeName)
        {
            int genericIndex = typeName.IndexOf('{');
            List<Type> genericTypes = new List<Type>();
            Type t = null;

            if (genericIndex > -1)
            {
                string generics = typeName.Substring(genericIndex + 1, typeName.Length - genericIndex - 2);
                typeName = typeName.Substring(0, genericIndex);
                string[] gParams = generics.Split(',');

                foreach (string gp in gParams)
                {
                    string genericName = gp.Trim();
                    Type gType = GetWordType(genericName);
                    if (gType == null)
                    {
                        _log.Warning($"Unknown generic type '{genericName}' for type '{typeName}'");
                        return null;
                    }

                    genericTypes.Add(gType);
                }

                t = Type.GetType($"SpirvReflector.{typeName}`{genericTypes.Count}");
                if (t == null)
                    t = Type.GetType($"System.{typeName}`{genericTypes.Count}");

                if (t != null)
                    t = t.MakeGenericType(genericTypes.ToArray());
            }
            else
            {
                t = Type.GetType($"SpirvReflector.{typeName}");
                if (t == null)
                    t = Type.GetType($"System.{typeName}");
            }

            return t;
        }
    }
}
