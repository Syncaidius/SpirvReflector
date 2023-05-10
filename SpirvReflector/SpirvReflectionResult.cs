using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirvReflector
{
    public class SpirvReflectionResult
    {
        List<SpirvInstruction> _instructions;
        List<SpirvCapability> _capabilities;
        List<string> _extensions;


        internal SpirvReflectionResult()
        {
            _instructions = new List<SpirvInstruction>();
            _capabilities = new List<SpirvCapability>();
            _extensions = new List<string>();
        }

        internal void SetInstructions(List<SpirvInstruction> instructions)
        {
            _instructions.Clear();
            _capabilities.Clear();

            _instructions.AddRange(instructions);

            foreach(SpirvInstruction inst in _instructions)
            {
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
                }
            }
        }

        /// <summary>
        /// Gets the SPIR-V version used by the reflected bytecode.
        /// </summary>
        public SpirvVersion Version { get; internal set; }

        /// <summary>
        /// The original generator's magic number.
        /// </summary>
        public uint Generator { get; internal set; }

        /// <summary>
        /// Gets the Bound value; where all [id]s in this module are guaranteed to satisfy.
        /// <para>Bound should be small, smaller is better, with all [id] in a module being densely packed and near 0.</para>
        /// </summary>
        public uint Bound { get; internal set; }

        /// <summary>
        /// Gets the instruction schema number. This is still reserved if unused.
        /// </summary>
        public uint InstructionSchema { get; internal set; }

        /// <summary>
        /// Gets a read-only list of capabilities required to execute the bytecode.
        /// </summary>
        public IReadOnlyList<SpirvCapability> Capabilities => _capabilities;

        /// <summary>
        /// Gets a read-only list of extensions required to execute the bytecode.
        /// </summary>
        public IReadOnlyList<string> Extensions => _extensions;

        public int InstructionCount => _instructions.Count;
    }
}
