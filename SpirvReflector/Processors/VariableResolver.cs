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
            SpirvConstant initializer = null;

            if (!context.TryGetAssignedElement(pointerID, out SpirvPointer ptrType))
            {
                context.Log.Warning($"Failed to resolve type for variable {inst.Result.Value}.");
                return;
            }

            // Retrieve initializer, if available. This is an optional operand.
            if (inst.WordCount > 4)
            {
                uint initID = inst.GetOperandValue<uint>(4);
                context.TryGetAssignedElement(initID, out initializer);
            }

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

            context.SetAssignedElement(inst.Result.Value, v);
            context.ReplaceElement(inst, v);
        }
    }
}
