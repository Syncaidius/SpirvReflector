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

        internal SpirvReflectionResult()
        {
            _instructions = new List<SpirvInstruction>();
        }

        internal void SetInstructions(List<SpirvInstruction> instructions)
        {
            _instructions.Clear();
            _instructions.AddRange(instructions);
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
    }
}
