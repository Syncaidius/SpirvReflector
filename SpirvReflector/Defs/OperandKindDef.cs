using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirvReflector
{
    public class OperandKindDef
    {
        public string Category { get; set; }

        public string Kind { get; set; }

        public EnumerantDef[] Enumerants { get; set; } = new EnumerantDef[0];
    }
}
