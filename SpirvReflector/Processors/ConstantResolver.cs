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
                    Type = type,
                    Value = ResolveConstantValue(inst, type),
                };

                context.SetAssignedElement(inst.Result.Value, c);
                context.ReplaceElement(inst, c);
            }
            else
            {
                context.Log.Error($"Could not find type {typeID} for OpConstant {inst.Result}.");
            }
        }

        private unsafe object ResolveConstantValue(SpirvInstruction inst, SpirvType type)
        {
            uint width = type.NumBytes * 8;
            SpirvLiteralInteger literal = inst.Operands[2] as SpirvLiteralInteger;

            if (inst.UnreadWordCount > 0)
            {
                // Number is larger than 32-bit
                throw new NotImplementedException("Support for larger than 32-bit constants is not implemented yet.");
            }
            else
            {
                uint value = literal.Value;
                switch (type.Kind)
                {
                    case SpirvTypeKind.Int:
                        return *(int*)&value;

                    default:
                    case SpirvTypeKind.UInt:
                        return value;

                    case SpirvTypeKind.Float:
                        return *(float*)&value;
                }
            }

            return null;
        }
    }
}
