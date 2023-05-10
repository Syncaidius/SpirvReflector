using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirvReflector
{
    public class SpirvReflectionResult
    {
        SpirvInstruction[] _assignments;

        List<SpirvInstruction> _instructions;
        List<SpirvCapability> _capabilities;
        List<string> _extensions;


        internal SpirvReflectionResult(ref SpirvVersion version, uint generator, uint bound, uint schema)
        {
            _instructions = new List<SpirvInstruction>();
            _capabilities = new List<SpirvCapability>();
            _extensions = new List<string>();
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

            log.WriteLine("Translated:", ConsoleColor.Green);

            foreach(SpirvInstruction inst in _instructions)
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
                        AddressingModel = inst.GetOperandValue<SpirvAddressingModel>();
                        MemoryModel = inst.GetOperandValue<SpirvMemoryModel>();
                        break;
                }
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
        /// Gets the total number of instructions in the bytecode.
        /// </summary>
        public int InstructionCount => _instructions.Count;

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
