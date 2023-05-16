using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpirvReflector
{
    internal class VariableResolver : SpirvProcessor
    {
        protected override void OnProcess(SpirvReflectContext context, SpirvInstruction inst)
        {
            if (inst.Result == null)
                return;

            SpirvBytecodeElement e = null;
            switch (inst.OpCode)
            {
                case SpirvOpCode.OpTypePointer:
                    uint typeID = inst.GetOperandValue<uint>(2);
                    e = new SpirvPointer()
                    {
                        ID = inst.Result.Value,
                        StorageClass = inst.GetOperandValue<SpirvStorageClass>(1),
                        Type = context.AssignedElements[typeID] as SpirvType
                    };
                    break;

                case SpirvOpCode.OpVariable:
                    uint pointerID = inst.GetOperandValue<uint>(0);
                    int initID = -1;
                    SpirvConstant initializer = null;

                    // Retrieve initializer, if available. This is an optional operand.
                    if (inst.WordCount > 4)
                    {
                        initID = (int)inst.GetOperandValue<uint>(4);
                        initializer = context.AssignedElements[(uint)initID] as SpirvConstant;
                    }

                    e = new SpirvVariable()
                    {
                        ID = inst.Result.Value,
                        StorageClass = inst.GetOperandValue<SpirvStorageClass>(2),
                        DefaultValue = initializer,
                        Pointer = context.AssignedElements[pointerID] as SpirvPointer
                    };
                    break;

                default:
                    return;
            }

            context.AssignedElements.Add(inst.Result.Value, e);
            context.ReplaceElement(inst, e);
        }
    }
}
