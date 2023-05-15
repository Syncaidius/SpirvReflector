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
                        if(typeRefID == 1)
                        {

                        }
                        if (context.AssignedElements.TryGetValue(typeRefID, out SpirvBytecodeElement e))
                        {
                            string name = inst.GetOperandString(1);
                            if (e is SpirvType t)
                                t.Name = name;
                            else if (e is SpirvFunction f)
                                f.Name = name;
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
                        if (context.AssignedElements.TryGetValue(typeRefID, out SpirvBytecodeElement e))
                        {
                            if (e is SpirvType t)
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
                        }
                        else
                        {
                            context.Log.Warning($"Failed to find translated type with ID {typeRefID} for OpMemberName assignment.");
                            return;
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
