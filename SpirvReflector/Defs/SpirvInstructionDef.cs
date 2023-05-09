using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirvReflector
{
    internal class SpirvInstructionDef
    {
        public string OpName { get; set; }

        public string Class { get; set; }

        public uint Opcode { get; set; }

        public SpirvOperandDef[] Operands { get; set; } = new SpirvOperandDef[0];

        public override string ToString()
        {
            return $"Name: {OpName} -- Op: {Opcode} -- Class: {Class}";
        }
    }
}
