using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpirvReflector
{
    internal class FunctionResolver : SpirvProcessor
    {
        Stack<SpirvFunction> _funcStack = new Stack<SpirvFunction>();
        SpirvFunction _curFunc;

        protected override void OnProcess(SpirvReflectContext context, SpirvInstruction inst)
        {
            switch (inst.OpCode)
            {
                case SpirvOpCode.OpFunction:
                    if (_curFunc != null)
                        _funcStack.Push(_curFunc);

                    _curFunc = new SpirvFunction()
                    {
                        Control = inst.GetOperandValue<SpirvFunctionControl>(2),
                        Start = inst,
                    };

                    uint returnTypeId = inst.GetOperandValue<uint>(0);
                    if (context.OpTypes.TryGetValue(returnTypeId, out SpirvType returnType))
                        _curFunc.ReturnType = returnType;

                    // Get parameters from referenced OpTypeFunction.
                    SpirvIdRef fType = inst.GetOperand<SpirvIdRef>(3);
                    for(int i = 2; i < fType.Ref.Operands.Count; i++) // Parameters start at at operand 2 in OpTypeFunction
                    {
                        if (fType.Ref.Operands[i] is SpirvIdRef pRef)
                            _curFunc.Parameters.Add(pRef.Ref);
                    }

                    context.Functions.Add(_curFunc);
                    context.ReplaceElement(inst, _curFunc);
                    return;

                case SpirvOpCode.OpFunctionEnd:
                    if (_curFunc != null)
                    {
                        _curFunc.End = inst;

                        if (_funcStack.Count > 0)
                            _curFunc = _funcStack.Pop();
                        else
                            _curFunc = null;
                    }
                    break;

                default:
                    if (_curFunc != null)
                    {
                        _curFunc.Instructions.Add(inst);
                        break;
                    }
                    return;
            }

            context.Elements.Remove(inst);
        }

        protected override void OnCompleted()
        {
            base.OnCompleted();
            _funcStack.Clear();
            _curFunc = null;
        }
    }
}
