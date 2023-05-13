using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpirvReflector
{
    internal class TypeParser : SpirvParser
    {
        Stack<SpirvFunction> _funcStack = new Stack<SpirvFunction>();
        SpirvFunction _curFunc;

        public TypeParser()
        {
            AddPrerequisite<InitialParser>();
        }

        protected override void OnParse(SpirvReflection reflection, SpirvReflectionResult result, SpirvInstruction inst)
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
                        t.ElementType = typeof(bool);
                        break;

                    case SpirvOpCode.OpTypeFloat:
                        t.Kind = SpirvTypeKind.Float;
                        t.ElementType = typeof(float); // TODO check bit-size and decide on half, float or double (16, 32 or 64-bit).
                        break;

                    case SpirvOpCode.OpTypeInt:
                        t.Kind = SpirvTypeKind.Int;
                        t.ElementType = typeof(int); // TODO check bit-size and decide on short, int or long (16, 32 or 64-bit).
                        break;

                    case SpirvOpCode.OpTypeMatrix:
                        t.Kind = SpirvTypeKind.Matrix;

                        break;

                    case SpirvOpCode.OpTypeVoid:
                        t.Kind = SpirvTypeKind.Void;
                        t.ElementType = typeof(void);
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
