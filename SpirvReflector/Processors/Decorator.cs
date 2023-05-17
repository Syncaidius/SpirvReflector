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

                        // Check if we're applying a binding/slot ID.
                        SpirvDecoratedElement element = context.AssignedElements[targetID] as SpirvDecoratedElement;
                        if (dec == SpirvDecoration.Binding && element is SpirvVariable v)
                            v.Binding = (uint)decValues[0];
                        else
                            element.Decorations.AddDecoration(dec, decValues);
                    }
                    break;

                case SpirvOpCode.OpMemberDecorate:
                    {
                        uint targetID = inst.GetOperandValue<uint>(0);
                        int memberIndex = (int)inst.GetOperandValue<uint>(1);
                        SpirvDecoration dec = inst.GetOperandValue<SpirvDecoration>();
                        List<object> decValues = new List<object>();
                        for (int i = 3; i < inst.Operands.Count; i++)
                        {
                            SpirvWord w = inst.Operands[i];
                            decValues.Add(w.GetValue());
                        }
                        SpirvType parentType = context.AssignedElements[targetID] as SpirvType;
                        if (parentType == null)
                        {
                            context.Log.Warning($"Member decoration on non-type [{targetID}].");
                            return;
                        }

                        if (dec == SpirvDecoration.Offset)
                        {
                            uint offset = (uint)decValues[0];
                            parentType.Members[memberIndex].ByteOffset = offset;
                        }
                        else
                        {
                            parentType.Members[memberIndex].Decorations.AddDecoration(dec, decValues);
                        }
                    }
                    break;                

                default:
                    return;
            }

            // Since names are applied to other instructions, we can dump instructions which provide the names.
            context.Elements.Remove(inst);
        }
    }
}
