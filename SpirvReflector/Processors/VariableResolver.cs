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
            if (inst.Result == null || inst.OpCode != SpirvOpCode.OpVariable)
                return;

            uint pointerID = inst.GetOperandValue<uint>(0);
            int initID = -1;
            SpirvConstant initializer = null;

            // Retrieve initializer, if available. This is an optional operand.
            if (inst.WordCount > 4)
            {
                initID = (int)inst.GetOperandValue<uint>(4);
                initializer = context.AssignedElements[(uint)initID] as SpirvConstant;
            }

            SpirvPointer ptrType = context.AssignedElements[pointerID] as SpirvPointer;
            SpirvVariable v = new SpirvVariable()
            {
                ID = inst.Result.Value,
                StorageClass = inst.GetOperandValue<SpirvStorageClass>(2),
                DefaultValue = initializer,
                Type = ptrType.Type
            };

            if (v.StorageClass == SpirvStorageClass.Uniform)
                context.Result.AddUniform(v);

            // Check if the variable is a resource.
            if(v.StorageClass == SpirvStorageClass.Image || v.Type is SpirvImageType imgType)
                context.Result.AddResource(v);

            context.AssignedElements.Add(inst.Result.Value, v);
            context.ReplaceElement(inst, v);
        }
    }
}
