using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirvReflector
{
    public class SpirvFunction : SpirvBytecodeElement
    {
        internal List<SpirvInstruction> Instructions { get; } = new List<SpirvInstruction>();

        internal SpirvInstruction Start { get; set; }

        internal SpirvInstruction End { get; set; }

        public SpirvType ReturnType { get; internal set; }

        public SpirvFunctionControl Control { get; internal set; }

        public int InstructionCount => Instructions.Count;

        public List<SpirvWord> Parameters { get; } = new List<SpirvWord>();
    }
}
