using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpirvReflector
{
    internal class Decorator : SpirvProcessor
    {
        protected override void OnProcess(SpirvReflectContext context, SpirvInstruction inst)
        {
            switch (inst.OpCode)
            {
                case SpirvOpCode.OpDecorateStringGOOGLE:
                case SpirvOpCode.OpDecorate:
                    {
                        uint targetID = inst.GetOperandValue<uint>(0);
                        SpirvDecoration dec = inst.GetOperandValue<SpirvDecoration>();
                        List<object> decValues = new List<object>();
                        for (int i = 2; i < inst.Operands.Count; i++)
                        {
                            SpirvWord w = inst.Operands[i];
                            decValues.Add(w.GetValue());
                        }
                        SpirvDecoratedElement element = context.AssignedElements[targetID] as SpirvDecoratedElement;
                        element.Decorations.AddDecoration(dec, decValues);
                    }
                    break;

                /*case SpirvOpCode.OpMemberDecorate:

                    break;

                */

                default:
                    return;
            }

            // Since names are applied to other instructions, we can dump instructions which provide the names.
            context.Elements.Remove(inst);
        }
    }
}
