using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpirvReflector
{
    internal class NameResolver : SpirvResolver<SpirvInstruction>
    {
        protected override void OnResolve(SpirvReflectContext context, SpirvInstruction inst)
        {
            switch (inst.OpCode)
            {
                case SpirvOpCode.OpName:
                    {
                        uint typeRefID = inst.GetOperandValue<uint>(0);
                        if(typeRefID == 1)
                        {

                        }
                        if (context.TryGetAssignedElement(typeRefID, out SpirvBytecodeElement e))
                        {
                            string name = inst.GetOperandString(1);
                            switch (e)
                            {
                                case SpirvType t:
                                    t.Name = name;
                                    break;

                                case SpirvFunction f:
                                    f.Name = name;
                                    break;

                                case SpirvVariable v:
                                    v.Name = name;
                                    break;
                            }
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
                        if (context.TryGetAssignedElement(typeRefID, out SpirvType t))
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
