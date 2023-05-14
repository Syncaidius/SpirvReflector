using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpirvReflector
{
    /// <summary>
    /// A processor which resolves operand references to other instructions.
    /// </summary>
    internal class RefResolver : SpirvProcessor
    {
        protected override void OnProcess(SpirvReflectContext context, SpirvInstruction inst)
        {
            foreach(SpirvWord operand in inst.Operands)
            {
                if (operand is SpirvIdResult)
                    continue;

                if (operand is SpirvIdRef idRef)
                    idRef.Ref = context.Assignments[idRef.Value];
            }
        }
    }
}
