using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpirvReflector
{
    internal class ConstantResolver : SpirvResolver<SpirvInstruction>
    {
        protected override void OnResolve(SpirvReflectContext context, SpirvInstruction inst)
        {
            switch (inst.OpCode)
            {
                case SpirvOpCode.OpConstant:
                    {
                        uint typeID = inst.GetOperandValue<uint>(0);
                        if (context.TryGetAssignedElement(typeID, out SpirvType type))
                        {
                            object value = ResolveConstantValue(inst, type);
                            SpirvConstant c = new SpirvConstant()
                            {
                                ID = inst.Result.Value,
                                Type = type,
                                Values = new object[] { value },
                            };

                            context.SetAssignedElement(inst.Result.Value, c);
                            context.ReplaceElement(inst, c);
                        }
                        else
                        {
                            context.Log.Error($"Could not find type {typeID} for OpConstant {inst.Result}.");
                        }
                    }
                    break;

                case SpirvOpCode.OpConstantComposite:
                    {
                        uint typeID = inst.GetOperandValue<uint>(0);
                        if (context.TryGetAssignedElement(typeID, out SpirvType type))
                        {
                            int numValues = inst.Operands.Count - 2;
                            object[] values = new object[numValues];

                            for (int i = 2; i < inst.Operands.Count; i++)
                            {
                                uint constID = inst.GetOperandValue<uint>(i);
                                if (context.TryGetAssignedElement(constID, out SpirvConstant constant))
                                    values[i - 2] = constant.Values[0];
                            }

                            SpirvConstant c = new SpirvConstant()
                            {
                                ID = inst.Result.Value,
                                Type = type,
                                Values = values,
                            };

                            context.SetAssignedElement(inst.Result.Value, c);
                            context.ReplaceElement(inst, c);
                        }
                        else
                        {
                            context.Log.Error($"Could not find type {typeID} for OpConstant {inst.Result}.");
                        }
                    }
                    break;
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
        }
    }
}
