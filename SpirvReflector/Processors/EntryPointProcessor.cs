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
            if (inst.OpCode != SpirvOpCode.OpEntryPoint)
                return;

            uint funcID = inst.GetOperandValue<uint>(1);
            SpirvEntryPoint e = new SpirvEntryPoint()
            {
                Function = context.AssignedElements[funcID] as SpirvFunction,
                ExecutionModel = inst.GetOperandValue<SpirvExecutionModel>(0),
                Name = inst.GetOperandString(2),
            };

            for(int i = 3; i < inst.Operands.Count; i++)
            {
                uint varID = inst.GetOperandValue<uint>(i);
                SpirvVariable v = context.AssignedElements[varID] as SpirvVariable;
                e.AddVariable(v);
            }

            context.Result.AddEntryPoint(e);
            context.ReplaceElement(inst, e);
        }
    }
}
