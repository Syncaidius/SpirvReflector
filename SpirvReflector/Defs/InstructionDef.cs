using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirvReflector
{
    internal class InstructionDef
    {
        public string OpName { get; set; }

        public string Class { get; set; }

        public uint Opcode { get; set; }

        public OperandDef[] Operands { get; set; } = new OperandDef[0];
    }
}
