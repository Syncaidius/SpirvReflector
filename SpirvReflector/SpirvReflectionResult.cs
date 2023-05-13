using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirvReflector
{
    public class SpirvReflectionResult
    {
        public class EntryPoint
        {
            public SpirvExecutionModel ExecutionModel { get; internal set; }

            public string Name { get; internal set; }
        }

        internal List<SpirvBytecodeElement> Elements;

        internal SpirvInstruction[] Assignments;

        internal List<SpirvFunction> Functions;

        List<SpirvInstruction> _instructions;
        List<SpirvCapability> _capabilities;
        List<SpirvSource> _sources;
        List<EntryPoint> _entryPoints;
        List<string> _extensions;
        HashSet<Type> _completedParsers;

        internal SpirvReflectionResult(ref SpirvVersion version, uint generator, uint bound, uint schema)
        {
            _instructions = new List<SpirvInstruction>();
            _completedParsers = new HashSet<Type>();

            Elements = new List<SpirvBytecodeElement>();
            Functions = new List<SpirvFunction>();
            Assignments = new SpirvInstruction[bound];

            _capabilities = new List<SpirvCapability>();
            _sources = new List<SpirvSource>();
            _extensions = new List<string>();
            _entryPoints = new List<EntryPoint>();

            Version = version;
            Generator  = generator;
            Bound = bound;
            InstructionSchema = schema;
        }

        internal void AddCapaibility(SpirvCapability cap)
        {
            _capabilities.Add(cap);
        }

        internal void AddExtension(string ext)
        {
            _extensions.Add(ext);
        }

        internal void AddSource(SpirvSource source)
        {
            _sources.Add(source);
        }

        internal void RunParser<T>(SpirvReflection reflection)
            where T : SpirvParser, new()
        {
            RunParser(typeof(T), reflection);
        }

        internal void RunParser(Type pType, SpirvReflection reflection)
        {
            if (!_completedParsers.Contains(pType))
            {
                SpirvParser parser = reflection.GetParser(pType);
                parser.Parse(reflection, this);
                _completedParsers.Add(pType);
            }
        }


        internal void SetInstructions(SpirvReflection reflection, List<SpirvInstruction> instructions, IReflectionLogger log)
        {
            _completedParsers.Clear();
            _instructions.Clear();
            _capabilities.Clear();
            _instructions.AddRange(instructions);
            Elements.AddRange(_instructions);

            InstructionCount = instructions.Count;
            RunParser<InitialParser>(reflection);
            RunParser<FunctionParser>(reflection);

            string caps = string.Join(", ", _capabilities.Select(c => c.ToString()));
            string exts = string.Join(", ", _extensions);

            log.WriteLine("Translated:", ConsoleColor.Green);
            log.WriteLine($"Capabilities: {caps}");
            log.WriteLine($"Extensions: {exts}");
            log.WriteLine($"Memory Model: {AddressingModel} -- {MemoryModel}");

            uint eID = 0;
            foreach(SpirvBytecodeElement element in Elements)
            {
                switch (element)
                {
                    case SpirvInstruction inst:
                        {
                            string opResult = inst.Result != null ? $"{inst.Result} = " : "";
                            if (inst.Operands.Count > 0)
                            {
                                string operands = SpirvReflection.GetOperandString(inst);
                                log.WriteLine($"E_{eID}: {opResult}{inst.OpCode} -- {operands}");
                            }
                            else
                            {
                                log.WriteLine($"E_{eID}: {opResult}{inst.OpCode}");
                            }
                        }
                        break;

                    case SpirvFunction func:
                        {
                            string returnType = "";
                            if (func.ReturnType != null)
                                returnType = func.ReturnType.OpCode.ToString().Replace("Spirv", "") + " ";

                            // TODO fetch function parameter definition
                            SpirvFunctionControl funcControl = func.Start.GetOperand<SpirvFunctionControl>();
                            uint defID = func.Start.GetOperand<uint>(3);
                            SpirvInstruction funcDef = Assignments[defID];

                            log.WriteLine($"E_{eID}:");
                            log.WriteLine($"[FunctionControl.{funcControl}]");
                            log.WriteLine($"{returnType}Function()");
                            log.WriteLine($"{{");
                            foreach (SpirvInstruction inst in func.Instructions)
                                log.WriteLine($"    {inst}");
                            log.WriteLine($"}}");
                        }
                        break;
                }

                eID++;
                
            }
        }

        /// <summary>
        /// Gets the SPIR-V version used by the reflected bytecode.
        /// </summary>
        public SpirvVersion Version { get; }

        /// <summary>
        /// The original generator's magic number.
        /// </summary>
        public uint Generator { get; }

        /// <summary>
        /// Gets the Bound value; where all [id]s in this module are guaranteed to satisfy.
        /// <para>Bound should be small, smaller is better, with all [id] in a module being densely packed and near 0.</para>
        /// </summary>
        public uint Bound { get; }

        /// <summary>
        /// Gets the instruction schema number. This is still reserved if unused.
        /// </summary>
        public uint InstructionSchema { get; }

        /// <summary>
        /// Gets a read-only list of capabilities required to execute the bytecode.
        /// </summary>
        public IReadOnlyList<SpirvCapability> Capabilities => _capabilities;

        /// <summary>
        /// Gets a read-only list of extensions required to execute the bytecode.
        /// </summary>
        public IReadOnlyList<string> Extensions => _extensions;

        /// <summary>
        /// Gets a list of sources that the bytecode was originally translated from.
        /// </summary>
        public IReadOnlyList<SpirvSource> Sources => _sources;

        /// <summary>
        /// Gets a list of entry points that were found in the bytecode.
        /// </summary>
        public IReadOnlyList<EntryPoint> EntryPoints => _entryPoints;

        /// <summary>
        /// Gets the total number of instructions in the bytecode.
        /// </summary>
        public int InstructionCount { get; private set; }

        /// <summary>
        /// Gets the memory addressing model used by the bytecode.
        /// </summary>
        public SpirvAddressingModel AddressingModel { get; internal set; }

        /// <summary>
        /// Gets the memory model used by the bytecode.
        /// </summary>
        public SpirvMemoryModel MemoryModel { get; internal set; }
    }
}
