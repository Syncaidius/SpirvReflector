using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SpirvReflector
{
    internal class FunctionParser : SpirvParser
    {
        Stack<SpirvFunction> _funcStack = new Stack<SpirvFunction>();
        SpirvFunction _curFunc;

        public FunctionParser()
        {
            AddPrerequisite<InitialParser>();
        }

        protected override void OnParse(SpirvReflection reflection, SpirvReflectionResult result, SpirvInstruction inst)
        {
            switch (inst.OpCode)
            {
                case SpirvOpCode.OpFunction:
                    if (_curFunc != null)
                        _funcStack.Push(_curFunc);

                    uint returnTypeId = inst.GetOperand<uint>(0);
                    _curFunc = new SpirvFunction()
                    {
                        ReturnType = result.Assignments[returnTypeId],
                        Control = inst.GetOperand<SpirvFunctionControl>(2),
                        Start = inst,
                    };
                    result.Functions.Add(_curFunc);
                    result.Elements.Add(_curFunc);
                    break;

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

            result.Elements.Remove(inst);
        }

        protected override void OnCompleted()
        {
            base.OnCompleted();
            _funcStack.Clear();
            _curFunc = null;
        }
    }
}
