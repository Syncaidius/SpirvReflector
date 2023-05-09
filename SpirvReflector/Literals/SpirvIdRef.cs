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
    }
}
