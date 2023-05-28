using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpirvReflector
{
    internal class DecorationResolver : SpirvResolver<SpirvInstruction>
    {
        protected override void OnResolve(SpirvReflectContext context, SpirvInstruction inst)
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
                        if(context.TryGetAssignedElement(targetID, out SpirvBytecodeElement e))
                        {
                            if (dec == SpirvDecoration.Binding && e is SpirvVariable v)
                                v.Binding = (uint)decValues[0];
                            else if(e is SpirvDecoratedElement de)
                                de.Decorations.Add(dec, decValues);
                        }
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

                        if (context.TryGetAssignedElement(targetID, out SpirvType parentType))
                        {
                            if (dec == SpirvDecoration.Offset)
                            {
                                uint offset = (uint)decValues[0];
                                parentType.Members[memberIndex].ByteOffset = offset;
                            }
                            else
                            {
                                parentType.Members[memberIndex].Decorations.Add(dec, decValues);
                            }
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
