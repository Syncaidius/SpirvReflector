using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpirvReflector
{
    internal class ConstantResolver : SpirvProcessor
    {
        protected override void OnProcess(SpirvReflectContext context, SpirvInstruction inst)
        {
            if (inst.OpCode != SpirvOpCode.OpConstant)
                return;

            uint typeID = inst.GetOperandValue<uint>(0);
            if (context.TryGetAssignedElement(typeID, out SpirvType type))
            {
                SpirvConstant c = new SpirvConstant()
                {
                    ID = inst.Result.Value,
                    Type = type
                };

                SpirvWord literal = inst.Operands[2];
                Type t = literal.GetType();
                c.Value = literal.GetValue();

                context.SetAssignedElement(inst.Result.Value, c);
                context.ReplaceElement(inst, c);
            }
            else
            {
                context.Log.Error($"Could not find type {typeID} for OpConstant {inst.Result}.");
            }
        }
    }
}
