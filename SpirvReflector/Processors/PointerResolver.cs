using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpirvReflector
{
    internal class PointerResolver : SpirvProcessor
    {
        protected override void OnProcess(SpirvReflectContext context, SpirvInstruction inst)
        {
            if (inst.Result == null || inst.OpCode != SpirvOpCode.OpTypePointer)
                return;

            uint typeID = inst.GetOperandValue<uint>(2);
            SpirvBytecodeElement e = new SpirvPointer()
            {
                ID = inst.Result.Value,
                StorageClass = inst.GetOperandValue<SpirvStorageClass>(1),
                Type = context.AssignedElements[typeID] as SpirvType
            };

            context.AssignedElements.Add(inst.Result.Value, e);
            context.ReplaceElement(inst, e);
        }
    }
}
