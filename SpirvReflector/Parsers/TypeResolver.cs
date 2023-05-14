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

            if (!context.OpTypes.TryGetValue(inst.Result.Value, out SpirvType t))
            {
                t = new SpirvType();

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
                        break;

                    case SpirvOpCode.OpTypeFloat:
                        t.Kind = SpirvTypeKind.Float;
                        break;

                    case SpirvOpCode.OpTypeInt:
                        t.Kind = SpirvTypeKind.Int;
                        break;

                    case SpirvOpCode.OpTypeMatrix:
                        t.Kind = SpirvTypeKind.Matrix;

                        break;

                    case SpirvOpCode.OpTypeVoid:
                        t.Kind = SpirvTypeKind.Void;
                        break;

                    default:
                        return;
                }

                context.OpTypes.Add(inst.Result.Value, t);
            }

            context.ReplaceElement(inst, t);
        }
    }
}
