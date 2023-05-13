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

        SpirvInstruction[] _assignments;

        List<SpirvInstruction> _instructions;
        List<SpirvBytecodeElement> _elements;

        List<SpirvCapability> _capabilities;
        List<SpirvSource> _sources;
        List<SpirvFunction> _functions;
        List<EntryPoint> _entryPoints;
        List<string> _extensions;

        internal SpirvReflectionResult(ref SpirvVersion version, uint generator, uint bound, uint schema)
        {
            _instructions = new List<SpirvInstruction>();
            _elements = new List<SpirvBytecodeElement>();

            _capabilities = new List<SpirvCapability>();
            _sources = new List<SpirvSource>();
            _extensions = new List<string>();
            _functions = new List<SpirvFunction>();
            _entryPoints = new List<EntryPoint>();
            _assignments = new SpirvInstruction[bound];

            Version = version;
            Generator  = generator;
            Bound = bound;
            InstructionSchema = schema;
        }

        internal void SetInstructions(List<SpirvInstruction> instructions, IReflectionLogger log)
        {
            _instructions.Clear();
            _capabilities.Clear();
            _instructions.AddRange(instructions);
            _elements.AddRange(_instructions);

            InstructionCount = instructions.Count;
            InitialPass();
            FunctionPass();

            string caps = string.Join(", ", _capabilities.Select(c => c.ToString()));
            string exts = string.Join(", ", _extensions);

            log.WriteLine("Translated:", ConsoleColor.Green);
            log.WriteLine($"Capabilities: {caps}");
            log.WriteLine($"Extensions: {exts}");
            log.WriteLine($"Memory Model: {AddressingModel} -- {MemoryModel}");

            uint eID = 0;
            foreach(SpirvBytecodeElement element in _elements)
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
                            log.WriteLine($"E_{eID}: Function() -- {SpirvReflection.GetOperandString(func.Start)}");
                            log.WriteLine($"{{");
                            foreach (SpirvInstruction inst in func.Instructions)
                            {
                                log.WriteLine($"    {inst}");
                            }
                            log.WriteLine($"}}");
                        }
                        break;
                }

                eID++;
                
            }
        }

        private void Pass(Action<SpirvInstruction> parseCallback)
        {
            if (parseCallback == null)
                throw new ArgumentNullException("parseCallback cannot be null");

            List<SpirvBytecodeElement> elements = new List<SpirvBytecodeElement>(_elements);
            foreach (SpirvBytecodeElement el in elements)
            {
                if (el is not SpirvInstruction inst)
                    continue;

                parseCallback(inst);
            }
        }

        /// <summary>
        /// Builds initial structure data. 
        /// 
        /// <para>Generally, this is pulled from instructions that do not reference any assigned values, as the assignment list has to be built during this pass.</para>
        /// </summary>
        private void InitialPass()
        {
            Pass((inst) =>
            {
                if (inst.Result != null)
                    _assignments[inst.Result.Value] = inst;

                switch (inst.OpCode)
                {
                    case SpirvOpCode.OpCapability:
                        SpirvWord<SpirvCapability> wCap = inst.Operands[0] as SpirvWord<SpirvCapability>;
                        _capabilities.Add(wCap.Value);
                        break;

                    case SpirvOpCode.OpExtension:
                        SpirvLiteralString wExt = inst.Operands[0] as SpirvLiteralString;
                        _extensions.Add(wExt.Value);
                        break;

                    case SpirvOpCode.OpMemoryModel:
                        AddressingModel = inst.GetOperand<SpirvAddressingModel>();
                        MemoryModel = inst.GetOperand<SpirvMemoryModel>();
                        break;

                    case SpirvOpCode.OpSource:
                        SpirvSource src = new SpirvSource()
                        {
                            Language = inst.GetOperand<SpirvSourceLanguage>(),
                            Version = inst.GetOperand<uint>(1),
                            Source = inst.GetOperandString(3),
                        };

                        // Filename is optional.
                        if (inst.Operands.Count >= 3)
                        {
                            uint fnID = inst.GetOperand<uint>(2);
                            SpirvInstruction fn = _assignments[fnID];
                            src.Filename = fn.GetOperandString(1);
                        }

                        _sources.Add(src);
                        break;

                    /*case SpirvOpCode.OpEntryPoint:
                        EntryPoint entry = new EntryPoint();
                        SpirvLiteralString ep = inst.GetOperandWord<SpirvLiteralString>();

                        if (ep != null)
                        {
                            entry.ExecutionModel = inst.GetOperand<SpirvExecutionModel>();
                            entry.Name = ep.Value;
                            _entryPoints.Add(entry);
                        }
                        break;*/

                    default:
                        return;
                }

                _elements.Remove(inst);
            });
        }

        /// <summary>
        /// Builds function information.
        /// </summary>
        private void FunctionPass()
        {
            Stack<SpirvFunction> funcStack = new Stack<SpirvFunction>();
            SpirvFunction curFunc = null;

            Pass((inst) =>
            {
                switch (inst.OpCode)
                {
                    case SpirvOpCode.OpFunction:
                        if (curFunc != null)
                            funcStack.Push(curFunc);

                        uint returnTypeId = inst.GetOperand<uint>(0);
                        curFunc = new SpirvFunction()
                        {
                            ReturnType = _assignments[returnTypeId].Result,
                            Control = inst.GetOperand<SpirvFunctionControl>(2),
                            Start = inst,
                        };
                        _functions.Add(curFunc);
                        _elements.Add(curFunc);
                        break;

                    case SpirvOpCode.OpFunctionEnd:
                        if (curFunc != null)
                        {
                            curFunc.End = inst;

                            if (funcStack.Count > 0)
                                curFunc = funcStack.Pop();
                            else
                                curFunc = null;
                        }
                        break;

                    default:
                        if (curFunc != null)
                        {
                            curFunc.Instructions.Add(inst);
                            break;
                        }
                        return;
                }

                _elements.Remove(inst);
            });
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
        public SpirvAddressingModel AddressingModel { get; private set; }

        /// <summary>
        /// Gets the memory model used by the bytecode.
        /// </summary>
        public SpirvMemoryModel MemoryModel { get; private set; }
    }
}
