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
                        ID = inst.Result.Value,
                    };

                    uint returnTypeId = inst.GetOperandValue<uint>(0);
                    if (context.AssignedElements.TryGetValue(returnTypeId, out SpirvBytecodeElement returnType))
                    {
                        if(returnType is SpirvType t)
                            _curFunc.ReturnType = t;
                    }

                    // Get parameters from referenced OpTypeFunction.
                    uint funcTypeID = inst.GetOperandValue<uint>(3);
                    SpirvInstruction funcType = context.Assignments[funcTypeID];
                    ResolveFunctionType(context, funcType, _curFunc);

                    context.AssignedElements.Add(_curFunc.ID, _curFunc);
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

        private void ResolveFunctionType(SpirvReflectContext context, SpirvInstruction funcType, SpirvFunction func)
        {
            if (funcType.OpCode != SpirvOpCode.OpTypeFunction)
                return;

            for (int i = 2; i < funcType.Operands.Count; i++) // Parameters start at at operand 2 in OpTypeFunction
            {
                uint pTypeID = funcType.GetOperandValue<uint>(i);
                SpirvType pType = context.AssignedElements[pTypeID] as SpirvType;
                _curFunc.Parameters.Add(pType);
            }

            context.Elements.Remove(funcType);
        }

        protected override void OnCompleted()
        {
            base.OnCompleted();
            _funcStack.Clear();
            _curFunc = null;
        }
    }
}
