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
    public unsafe class Reflector
    {
        static Dictionary<SpirvOpCode, SpirvInstructionDef> _defs;
        List<SpirvInstruction> _instructions;
        SpirvStream _stream;
        IReflectionLogger _log;

        static Reflector()
        {
            _defs = new Dictionary<SpirvOpCode, SpirvInstructionDef>();
            Stream stream = TryGetEmbeddedStream("spirv.core.grammer.json", typeof(SpirvInstructionDef).Assembly);
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
                        Dictionary<string, SpirvInstructionDef> instructionDefs = JsonConvert.DeserializeObject<Dictionary<string, SpirvInstructionDef>>(json);
                        foreach (SpirvInstructionDef def in instructionDefs.Values)
                            _defs.Add((SpirvOpCode)def.Opcode, def);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Failed to parse SPIR-V opcode definitions: {ex.Message}");
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
        private static Stream TryGetEmbeddedStream(string filename, Assembly assembly = null)
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

        public Reflector(void* byteCode, nuint numBytes, IReflectionLogger log)
        {
            _instructions = new List<SpirvInstruction>();
            _stream = new SpirvStream(byteCode, numBytes);
            _log = log;

            // Next op is the version number.
            SpirvVersion version = (SpirvVersion)_stream.ReadWord();

            // Next op is the generator number.
            uint generator = _stream.ReadWord();

            // Next op is the bound number.
            uint bound = _stream.ReadWord();

            // Next op is the schema number.
            uint schema = _stream.ReadWord();

            ReadInstructions();
        }

        private void ReadInstructions()
        {
            uint instID = 0;
            while (!_stream.IsEndOfStream)
            {
                SpirvInstruction inst = _stream.ReadInstruction();
                _instructions.Add(inst);
            }
        }

        /*private void ReadInstructions(IReflectionLogger log)
        {
            uint instID = 0;
            while (!_stream.IsEndOfStream)
            {
                SpirvInstruction inst = _stream.ReadInstruction();
                _instructions.Add(inst);

                if (_defs.TryGetValue(inst.OpCode, out SpirvInstructionDef def))
                {
                    foreach (SpirvOperandDef opDef in def.Operands)
                    {
                        string wordTypeName = opDef.Words[wordDesc];
                        Type t = GetWordType(log, wordTypeName);

                        if (t != null)
                        {
                            SpirvWord word = Activator.CreateInstance(t) as SpirvWord;
                            word.Name = wordDesc;

                            if (word is SpirvResultID resultID)
                                inst.Result = resultID;
                            else
                                inst.Words.Add(word);

                            word.Read(inst);
                        }
                        else
                        {
                            log.Warning($"Unknown word type: {wordTypeName}");
                        }
                    }

                    string opResult = inst.Result != null ? $"{inst.Result} = " : "";
                    if (inst.Words.Count > 0)
                    {
                        string operands = string.Join(", ", inst.Words.Select(x => x.ToString()));
                        log.WriteLine($"Instruction {instID}: {opResult}{inst.OpCode} -- {operands}");
                    }
                    else
                    {
                        log.WriteLine($"Instruction {instID}: {opResult}{inst.OpCode}");
                    }
                }
                else
                {
                    log.Warning($"Instruction {instID}: Unknown opcode ({inst.OpCode}).");
                }

                instID++;
            }
        }*/

        private Type GetWordType(IReflectionLogger log, string typeName)
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
                    Type gType = GetWordType(log, genericName);
                    if (gType == null)
                    {
                        log.Warning($"Unknown generic type '{genericName}' for type '{typeName}'");
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
