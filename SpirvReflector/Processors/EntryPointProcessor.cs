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
                        if(!context.TryGetAssignedElement(funcID, out SpirvFunction func))
                        {
                            context.Log.Warning($"Failed to resolve entry point function {funcID}.");
                            return;
                        }

                        SpirvEntryPoint ep = new SpirvEntryPoint()
                        {
                            Function = func,
                            Name = inst.GetOperandString(2),
                        };

                        // Retrieve and map entry point input/output variables.
                        for (int i = 3; i < inst.Operands.Count; i++)
                        {
                            uint varID = inst.GetOperandValue<uint>(i);
                            if(context.TryGetAssignedElement(varID, out SpirvVariable v))
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
                        if (!context.TryGetAssignedElement(epID, out SpirvBytecodeElement el))
                            context.Log.Warning($"Failed to retrieve target '{epID}' for execution mode.");

                        SpirvExecutionMode mode = inst.GetOperandValue<SpirvExecutionMode>(1);

                        switch (el)
                        {
                            case SpirvEntryPoint ep:
                                ep.Execution.AddMode(mode, new List<object>());
                                break;

                            case SpirvFunction f:
                                // Find the entry point that references the function, since OpExecutionMode can only be applied to entry points.
                                foreach (SpirvEntryPoint ep in context.Result.EntryPoints)
                                {
                                    if (ep.Function == f)
                                    {
                                        List<object> pList = new List<object>();
                                        if(inst.Operands.Count > 2)
                                        {
                                            for (int i = 2; i < inst.Operands.Count; i++)
                                            {
                                                SpirvWord o = inst.Operands[i];
                                                pList.Add(o.GetValue());
                                            }
                                        }

                                        ep.Execution.AddMode(mode, pList);
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
