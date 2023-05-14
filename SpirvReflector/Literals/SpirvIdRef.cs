using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirvReflector
{
    public class SpirvIdRef : SpirvWord<uint>
    {
        public override string ToString()
        {
            return $"%{Value}";
        }

        /// <summary>
        /// Gets the <see cref="SpirvInstruction"/> that this reference points to. 
        /// <para>This will always be a reference to a <see cref="SpirvInstruction"/> that produces a <see cref="SpirvIdResult"/>.</para>
        /// </summary>
        public SpirvInstruction Ref { get; internal set; }
    }
}
