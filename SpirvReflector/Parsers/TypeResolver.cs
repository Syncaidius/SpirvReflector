using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpirvReflector
{
    internal class TypeResolver : SpirvProcessor
    {
        protected override void OnProcess(SpirvReflectContext context, SpirvInstruction inst)
        {
            if (inst.Result == null)
                return;

            ResolveType(inst.Result.Value, context);
        }

        private SpirvType ResolveType(uint refID, SpirvReflectContext context)
        {
            if (!context.OpTypes.TryGetValue(refID, out SpirvType t))
            {
                t = new SpirvType();
                t.Length = 1;
                t.ID = refID;
                SpirvInstruction inst = context.Assignments[refID];

                switch (inst.OpCode)
                {
                    case SpirvOpCode.OpTypeArray:
                        t.Kind = SpirvTypeKind.Array;
                        break;

                    case SpirvOpCode.OpTypeAccelerationStructureKHR:
                        t.Kind = SpirvTypeKind.AccelerationStructure;
                        break;

                    case SpirvOpCode.OpTypeBool:
                        t.Kind = SpirvTypeKind.Bool;
                        t.NumBytes = 1;
                        break;

                    case SpirvOpCode.OpTypeFloat:
                        t.Kind = SpirvTypeKind.Float;
                        t.NumBytes = inst.GetOperandValue<uint>(1) / 8;
                        break;

                    case SpirvOpCode.OpTypeInt:
                        uint signed = inst.GetOperandValue<uint>(2);
                        t.Kind = signed == 1 ? SpirvTypeKind.Int : SpirvTypeKind.UInt;
                        t.NumBytes = inst.GetOperandValue<uint>(1) / 8;
                        break;

                    case SpirvOpCode.OpTypeMatrix:
                        t.Kind = SpirvTypeKind.Matrix;
                        uint elTypeID = inst.GetOperandValue<uint>(1);
                        t.ElementType = ResolveType(elTypeID, context);
                        t.Length = inst.GetOperandValue<uint>(2);
                        t.NumBytes = t.ElementType.NumBytes * t.Length;
                        break;

                    case SpirvOpCode.OpTypeVector:
                        t.Kind = SpirvTypeKind.Vector;
                        uint comTypeID = inst.GetOperandValue<uint>(1);
                        t.ElementType = ResolveType(comTypeID, context);
                        t.Length = inst.GetOperandValue<uint>(2);
                        t.NumBytes = t.ElementType.NumBytes * t.Length;
                        break;

                    case SpirvOpCode.OpTypeVoid:
                        t.Kind = SpirvTypeKind.Void;
                        break;

                    default:
                        return t;
                }

                context.OpTypes.Add(inst.Result.Value, t);
                context.ReplaceElement(inst, t);
            }

            return t;
        }
    }
}
