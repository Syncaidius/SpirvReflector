using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirvReflector
{
    public class SpirvConstant : SpirvBytecodeElement
    {
        public override string ToString()
        {
            return $"{Type.Kind}*({ID})";
        }

        public uint ID { get; internal set; }

        public SpirvType Type { get; internal set; }

        public SpirvWord Value { get; internal set; }
    }
}
