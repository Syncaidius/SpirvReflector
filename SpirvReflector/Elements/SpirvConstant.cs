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
            return $"[ID: {ID}] Constant {Type.Kind} = {Value}";
        }

        public uint ID { get; internal set; }

        public SpirvType Type { get; internal set; }

        public object Value { get; internal set; }
    }
}
