using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirvReflector
{
    public class SpirvOperandKindDef
    {
        public string Category { get; set; }

        public string Kind { get; set; }

        public SpirvEnumerantDef[] Enumerants { get; set; } = new SpirvEnumerantDef[0];

        public string Doc { get; set; }

        public string[] Bases { get; set; } = new string[0];

        internal Dictionary<string, SpirvEnumerantDef> EnumerantLookup { get; } = new Dictionary<string, SpirvEnumerantDef>();

        internal void BuildLookups()
        {
            if (Enumerants != null)
            {
                foreach (SpirvEnumerantDef ed in Enumerants)
                    EnumerantLookup.Add(ed.Enumerant, ed);
            }
        }
    }
}
