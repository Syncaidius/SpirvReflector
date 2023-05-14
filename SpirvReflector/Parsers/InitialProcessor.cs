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
        protected override void OnProcess(SpirvReflection reflection, SpirvReflectionResult result, SpirvInstruction inst)
        {
            if (inst.Result != null)
                result.Assignments[inst.Result.Value] = inst;

            switch (inst.OpCode)
            {
                case SpirvOpCode.OpCapability:
                    SpirvWord<SpirvCapability> wCap = inst.Operands[0] as SpirvWord<SpirvCapability>;
                    result.AddCapaibility(wCap.Value);
                    break;

                case SpirvOpCode.OpExtension:
                    SpirvLiteralString wExt = inst.Operands[0] as SpirvLiteralString;
                    result.AddExtension(wExt.Text);
                    break;

                case SpirvOpCode.OpMemoryModel:
                    result.AddressingModel = inst.GetOperand<SpirvAddressingModel>();
                    result.MemoryModel = inst.GetOperand<SpirvMemoryModel>();
                    break;

                case SpirvOpCode.OpSource:
                    SpirvSource src = new SpirvSource()
                    {
                        Language = inst.GetOperand<SpirvSourceLanguage>(),
                        Version = inst.GetOperand<uint>(1),
                        Source = inst.GetOperandString(3),
                    };

                    // Filename is optional.
                    if (inst.Operands.Count >= 3)
                    {
                        uint fnID = inst.GetOperand<uint>(2);
                        SpirvInstruction fn = result.Assignments[fnID];
                        src.Filename = fn.GetOperandString(1);
                    }

                    result.AddSource(src);
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

            result.Elements.Remove(inst);
        }
    }
}
