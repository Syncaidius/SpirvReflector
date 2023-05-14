using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpirvReflector
{
    internal class InitialProcessor : SpirvProcessor
    {
        protected override void OnProcess(SpirvReflectContext context, SpirvInstruction inst)
        {
            if (inst.Result != null)
                context.Assignments[inst.Result.Value] = inst;

            switch (inst.OpCode)
            {
                case SpirvOpCode.OpCapability:
                    SpirvWord<SpirvCapability> wCap = inst.Operands[0] as SpirvWord<SpirvCapability>;
                    context.Result.AddCapaibility(wCap.Value);
                    break;

                case SpirvOpCode.OpExtension:
                    SpirvLiteralString wExt = inst.Operands[0] as SpirvLiteralString;
                    context.Result.AddExtension(wExt.Text);
                    break;

                case SpirvOpCode.OpMemoryModel:
                    context.Result.AddressingModel = inst.GetOperandValue<SpirvAddressingModel>();
                    context.Result.MemoryModel = inst.GetOperandValue<SpirvMemoryModel>();
                    break;

                case SpirvOpCode.OpSource:
                    SpirvSource src = new SpirvSource()
                    {
                        Language = inst.GetOperandValue<SpirvSourceLanguage>(),
                        Version = inst.GetOperandValue<uint>(1),
                        Source = inst.GetOperandString(3),
                    };

                    // Filename is optional.
                    if (inst.Operands.Count >= 3)
                    {
                        uint fnID = inst.GetOperandValue<uint>(2);
                        SpirvInstruction fn = context.Assignments[fnID];
                        src.Filename = fn.GetOperandString(1);
                    }

                    context.Result.AddSource(src);
                    break;

                // Dump any instructions we don't care about.
                case SpirvOpCode.OpLine:
                case SpirvOpCode.OpNoLine:
                case SpirvOpCode.OpModuleProcessed:
                    break;

                /*case SpirvOpCode.OpEntryPoint:
                    EntryPoint entry = new EntryPoint();
                    SpirvLiteralString ep = inst.GetOperandWord<SpirvLiteralString>();

                    if (ep != null)
                    {
                        entry.ExecutionModel = inst.GetOperand<SpirvExecutionModel>();
                        entry.Name = ep.Value;
                        _entryPoints.Add(entry);
                    }
                    break;*/

                default:
                    return;
            }

            context.Elements.Remove(inst);
        }
    }
}
