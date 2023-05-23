using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpirvReflector
{
    internal class EntryPointProcessor : SpirvProcessor
    {
        protected override void OnProcess(SpirvReflectContext context, SpirvInstruction inst)
        {
            switch(inst.OpCode)
            {
                case SpirvOpCode.OpEntryPoint:
                    {
                        uint funcID = inst.GetOperandValue<uint>(1);
                        SpirvEntryPoint ep = new SpirvEntryPoint()
                        {
                            Function = context.AssignedElements[funcID] as SpirvFunction,
                            Name = inst.GetOperandString(2),
                        };

                        for (int i = 3; i < inst.Operands.Count; i++)
                        {
                            uint varID = inst.GetOperandValue<uint>(i);
                            SpirvVariable v = context.AssignedElements[varID] as SpirvVariable;
                            ep.AddVariable(v);
                        }

                        ep.Execution.Model = inst.GetOperandValue<SpirvExecutionModel>(0);
                        context.Result.AddEntryPoint(ep);
                        context.ReplaceElement(inst, ep);
                    }
                    break;

                case SpirvOpCode.OpExecutionMode:
                    {
                        uint epID = inst.GetOperandValue<uint>(0);
                        SpirvBytecodeElement el = context.AssignedElements[epID];
                        SpirvExecutionMode mode = inst.GetOperandValue<SpirvExecutionMode>(1);

                        switch (el)
                        {
                            case SpirvEntryPoint ep:
                                ep.Execution.Mode = mode;
                                break;

                            case SpirvFunction f:
                                foreach (SpirvEntryPoint ep in context.Result.EntryPoints)
                                {
                                    if (ep.Function == f)
                                    {
                                        ep.Execution.Mode = mode;
                                        break;
                                    }
                                }
                                break;
                        }

                        context.Elements.Remove(inst);
                    }
                    break;

                default:
                    return;
            }
        }
    }
}
