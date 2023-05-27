using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirvReflector
{
    public class SpirvDecoratedElement : SpirvBytecodeElement
    {
        public override string ToString()
        {
            string r = "";
            foreach (SpirvDecoration dec in Decorations.Keys)
            {
                string p = string.Join(", ", Decorations[dec].Select(x => x.ToString()));
                if (p.Length > 0)
                    p = ":" + p;

                r += $"[{dec}{p}]";
            }

            return r;
        }

        public SpirvDecorationData Decorations { get; } = new SpirvDecorationData();
    }
}
