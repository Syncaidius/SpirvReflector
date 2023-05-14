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
        Stack<SpirvFunction> _funcStack = new Stack<SpirvFunction>();
        SpirvFunction _curFunc;

        public TypeResolver()
        {
            AddPrerequisite<InitialProcessor>();
        }

        protected override void OnProcess(SpirvReflection reflection, SpirvReflectionResult result, SpirvInstruction inst)
        {
            if (inst.Result == null)
                return;

            if (!result.OpTypes.TryGetValue(inst.Result.Value, out SpirvType t))
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

                result.OpTypes.Add(inst.Result.Value, t);
            }

            result.ReplaceElement(inst, t);
        }

        protected override void OnCompleted()
        {
            base.OnCompleted();
            _funcStack.Clear();
            _curFunc = null;
        }
    }
}
