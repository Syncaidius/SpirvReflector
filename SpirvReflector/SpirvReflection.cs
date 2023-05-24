using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
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
        SpirvDef _def;
        Dictionary<Type, SpirvProcessor> _parsers;

        public SpirvReflection(IReflectionLogger log, SpirvReflectionFlags flags)
        {
            Log = log;
            Flags = flags;
            _parsers = new Dictionary<Type, SpirvProcessor>();

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
                        _def.BuildLookups(this);
                    }
                    catch (Exception ex)
                    {
                        Log.Error($"Failed to parse SPIR-V opcode definitions: {ex.Message}");
                    }
                }
            }
            else
            {
                Log.Error($"SPIR-V opcode definition file not found.");
            }
        }

        private void Run<T>(SpirvReflectContext context)
                where T : SpirvProcessor, new()
        {
            Type pType = typeof(T);
            if (!typeof(SpirvProcessor).IsAssignableFrom(pType))
                throw new InvalidOperationException($"The provided parser type must be a derivative of {nameof(SpirvProcessor)}");

            if (!_parsers.TryGetValue(pType, out SpirvProcessor parser))
            {
                parser = Activator.CreateInstance(pType) as SpirvProcessor;
                _parsers.Add(pType, parser);
            };

            parser.Process(context);
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

        /// <summary>
        /// Processes the provided SPIR-V bytecode and returns a <see cref="SpirvReflectionResult"/> containing the results of the reflection process.
        /// </summary>
        /// <param name="byteCode">A an array of bytes which make up valid SPIR-V bytecode.</param>
        /// <returns></returns>
        public SpirvReflectionResult Reflect(byte[] byteCode)
        {
            fixed (void* ptr = &byteCode[0])
                return Reflect(ptr, (nuint)byteCode.Length);
        }

        /// <summary>
        /// Processes the provided SPIR-V bytecode and returns a <see cref="SpirvReflectionResult"/> containing the results of the reflection process.
        /// </summary>
        /// <param name="byteCode">A pointer to SPIR-V bytecode.</param>
        /// <param name="numBytes">The number of bytes in the bytecode.</param>
        /// <returns></returns>
        public SpirvReflectionResult Reflect(void* byteCode, nuint numBytes)
        {
            SpirvReflectContext context = new SpirvReflectContext(this);
            SpirvStream stream = new SpirvStream(byteCode, numBytes);

            context.Result.Version = (SpirvVersion)stream.ReadWord();
            context.Result.Generator = stream.ReadWord();
            context.Result.Bound = stream.ReadWord();
            context.Result.InstructionSchema = stream.ReadWord();
            context.Assignments = new SpirvInstruction[context.Result.Bound];
            ReadInstructions(stream, context);
            context.Result.InstructionCount = context.Instructions.Count;
            context.Elements.AddRange(context.Instructions);

            Run<InitialProcessor>(context);
            Run<TypeResolver>(context);
            Run<FunctionResolver>(context);
            Run<PointerResolver>(context);
            Run<VariableResolver>(context);
            Run<NameResolver>(context);
            Run<Decorator>(context);
            Run<EntryPointProcessor>(context);

            if(Flags.Has(SpirvReflectionFlags.LogInstructions))
                LogInstructions(context);

            if(Flags.Has(SpirvReflectionFlags.LogAssignments))
                LogAssignments(context);

            if(Flags.Has(SpirvReflectionFlags.LogResult))
                LogResult(context);

            return context.Result;
        }

        /// <summary>
        /// Outputs the raw instruction list to <see cref="Log"/>, if available.
        /// </summary>
        private void LogInstructions(SpirvReflectContext context)
        {
            if (Log == null)
                return;

            Log.WriteLine("Instructions:", ConsoleColor.Green);
            for (int i = 0; i < context.Instructions.Count; i++)
            {
                SpirvInstruction inst = context.Instructions[i];
                Log.WriteLabeled($"{i}", inst.ToString());
            }
        }

        private void LogAssignments(SpirvReflectContext context)
        {
            if (Log == null) 
                return;

            Log.WriteLine("\nAssignments:", ConsoleColor.Green);
            for (int i = 1; i < context.Assignments.Length; i++)
                Log.WriteLine(context.Assignments[i].ToString());
        }

        /// <summary>
        /// Outputs the current list of elements to <see cref="Log"/>, if available.
        /// </summary>
        private void LogResult(SpirvReflectContext context)
        {
            if (Log == null)
                return;

            string caps = string.Join(", ", context.Result.Capabilities.Select(c => c.ToString()));
            string exts = string.Join(", ", context.Result.Extensions);
            string sources = string.Join(", ", context.Result.Sources.Select(s => s.Filename));
            string entryPoints = string.Join(", ", context.Result.EntryPoints.Select(e => e.Name));

            Log.WriteLine("\nProcessed:", ConsoleColor.Green);
            Log.WriteLabeled("Capabilities", $"{caps}");
            Log.WriteLabeled("Extensions", $"{exts}");
            Log.WriteLabeled("Sources", $"{sources}");
            Log.WriteLabeled("Memory Model", $"{context.Result.AddressingModel} -- {context.Result.MemoryModel}");
            Log.WriteLabeled("Entry Point(s)", $"{entryPoints}");

            for (int i = 0; i < context.Elements.Count; i++)
                Log.WriteLabeled($"{i}", context.Elements[i].ToString());
        }

        private void ReadInstructions(SpirvStream stream, SpirvReflectContext context)
        {
            uint instID = 0;
            while (!stream.IsEndOfStream)
            {
                SpirvInstruction inst = stream.ReadInstruction();
                context.Instructions.Add(inst);

                if (_def.OpcodeLookup.TryGetValue(inst.OpCode, out SpirvInstructionDef def))
                {
                    foreach (SpirvOperandDef opDef in def.Operands)
                    {
                        // Skip optional operands if they are not present.
                        if ((opDef.Quantifier == "?" || opDef.Quantifier == "*") && inst.UnreadWordCount == 0)
                            continue;

                        if (opDef.Quantifier == "*")
                        {
                            while (inst.UnreadWordCount > 0)
                                ReadWord(inst, opDef.Kind, opDef.Name);
                        }
                        else
                        {
                            ReadWord(inst, opDef.Kind, opDef.Name);
                        }
                    }
                }
                else
                {
                    Log.Warning($"{instID}: Unknown opcode '{inst.OpCode}' ({(uint)inst.OpCode}).");
                }

                instID++;
            }
        }

        private void ReadWord(SpirvInstruction inst, string kind, string name)
        {
            // Check if the type is an enum.
            string wordTypeName = $"Spirv{kind}";
            Type t = GetWordType(wordTypeName);
            bool isEnum = t.IsEnum;

            // Check if we need to convert the word type to SpirvWord<T> to accomodate an enum value.
            if (isEnum)
            {
                Type tGeneric = GetWordType("SpirvWord`1");
                t = tGeneric.MakeGenericType(t);
            }

            if (t != null)
            {
                SpirvWord word = Activator.CreateInstance(t) as SpirvWord;
                inst.Operands.Add(word);
                word.Name = name;
                word.Read(inst);

                if (word is SpirvIdResult resultID)
                {
                    inst.Result = resultID;
                }
                else if (isEnum)
                {
                    object enumValue = word.GetValue();
                    SpirvEnumerantDef d = _def.GetEnumDef(enumValue.GetType(), enumValue);

                    foreach (SpirvParameterDef pd in d.Parameters)
                        ReadWord(inst, pd.Kind, pd.Name);
                }
            }
            else
            {
                Log.Warning($"Unknown word type: {wordTypeName}");
            }
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
                        Log.Warning($"Unknown generic type '{genericName}' for type '{typeName}'");
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

        /// <summary>
        /// Gets the log that was provided when the current <see cref="SpirvReflection"/> instance was created.
        /// </summary>
        internal IReflectionLogger Log { get; }

        /// <summary>
        /// Gets the flags that were passed in when the current <see cref="SpirvReflection"/> instance was created.
        /// </summary>
        public SpirvReflectionFlags Flags { get; }
    }
}
