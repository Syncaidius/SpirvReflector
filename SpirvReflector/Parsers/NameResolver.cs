using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpirvReflector
{
    internal class NameResolver : SpirvProcessor
    {
        protected override void OnProcess(SpirvReflectContext context, SpirvInstruction inst)
        {
            switch (inst.OpCode)
            {
                case SpirvOpCode.OpName:
                    {
                        uint typeRefID = inst.GetOperandValue<uint>(0);
                        if (context.OpTypes.TryGetValue(typeRefID, out SpirvType t))
                        {
                            t.Name = inst.GetOperandString(1);
                        }
                        else
                        {
                            context.Log.Warning($"Failed to find translated type with ID {typeRefID} for OpName assignment.");
                            return;
                        }
                    }
                    break;

                case SpirvOpCode.OpMemberName:
                    {
                        uint typeRefID = inst.GetOperandValue<uint>(0);
                        if (context.OpTypes.TryGetValue(typeRefID, out SpirvType t))
                        {
                            uint memberIndex = inst.GetOperandValue<uint>(1);
                            if (memberIndex < t.Members.Count)
                            {
                                t.Members[(int)memberIndex].Name = inst.GetOperandString(2);
                            }
                            else
                            {
                                context.Log.Warning($"Failed to find member with index '{memberIndex}' for OpMemberName assignment.");
                                return;
                            }
                        }
                        else
                        {
                            context.Log.Warning($"Failed to find translated type with ID {typeRefID} for OpMemberName assignment.");
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
