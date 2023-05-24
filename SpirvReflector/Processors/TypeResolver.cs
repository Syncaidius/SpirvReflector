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
        protected override void OnProcess(SpirvReflectContext context, SpirvInstruction inst)
        {
            if (inst.Result == null || !inst.OpCode.ToString().StartsWith("OpType"))
                return;

            ResolveType(inst.Result.Value, context);
        }

        private SpirvType ResolveType(uint refID, SpirvReflectContext context)
        {
            if (!context.TryGetAssignedElement(refID, out SpirvBytecodeElement e, true))
            {
                SpirvInstruction inst = context.Assignments[refID];
                SpirvType t = null;

                switch (inst.OpCode)
                {
                    case SpirvOpCode.OpTypeImage:
                        uint sampledTypeID = inst.GetOperandValue<uint>(1);
                        context.TryGetAssignedElement(sampledTypeID, out SpirvType sampledType);

                        SpirvImageType it = new SpirvImageType()
                        {
                            ElementType = sampledType,
                            Dimension = inst.GetOperandValue<SpirvDim>(2),
                            Depth = (SpirvImageDepthMode)inst.GetOperandValue<uint>(3),
                            IsArrayed = inst.GetOperandValue<uint>(4) == 1,
                            IsMultisampled = inst.GetOperandValue<uint>(5) == 1,
                            SampleMode = (SpirvImageSampleMode)inst.GetOperandValue<uint>(6),
                            Format = inst.GetOperandValue<SpirvImageFormat>(7),
                        };

                        t = it;

                        // Access qualifier is an optional operand.
                        if (inst.Operands.Count > 8)
                            it.AccessQualifier = inst.GetOperandValue<SpirvAccessQualifier>(7);

                        break;
                }

                if (t == null)
                {
                    t = new SpirvType();
                    t.Length = 1;

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
                            t.NumBytes = 1;
                            break;

                        case SpirvOpCode.OpTypeFloat:
                            t.Kind = SpirvTypeKind.Float;
                            t.NumBytes = inst.GetOperandValue<uint>(1) / 8;
                            break;

                        case SpirvOpCode.OpTypeInt:
                            uint signed = inst.GetOperandValue<uint>(2);
                            t.Kind = signed == 1 ? SpirvTypeKind.Int : SpirvTypeKind.UInt;
                            t.NumBytes = inst.GetOperandValue<uint>(1) / 8;
                            break;

                        case SpirvOpCode.OpTypeMatrix:
                            t.Kind = SpirvTypeKind.Matrix;
                            uint elTypeID = inst.GetOperandValue<uint>(1);
                            t.ElementType = ResolveType(elTypeID, context);
                            t.Length = inst.GetOperandValue<uint>(2);
                            t.NumBytes = t.ElementType.NumBytes * t.Length;
                            break;

                        case SpirvOpCode.OpTypeVector:
                            t.Kind = SpirvTypeKind.Vector;
                            uint comTypeID = inst.GetOperandValue<uint>(1);
                            t.ElementType = ResolveType(comTypeID, context);
                            t.Length = inst.GetOperandValue<uint>(2);
                            t.NumBytes = t.ElementType.NumBytes * t.Length;
                            break;

                        case SpirvOpCode.OpTypeVoid:
                            t.Kind = SpirvTypeKind.Void;
                            break;

                        case SpirvOpCode.OpTypeStruct:
                            t.Kind = SpirvTypeKind.Struct;
                            for (int i = 1; i < inst.Operands.Count; i++)
                            {
                                uint memberTypeID = inst.GetOperandValue<uint>(i);
                                t.AddMember(new SpirvTypeMember()
                                {
                                    Type = ResolveType(memberTypeID, context),
                                });
                            }
                            break;

                        case SpirvOpCode.OpTypeSampler:
                            t.Kind = SpirvTypeKind.Sampler;
                            break;

                        case SpirvOpCode.OpTypeSampledImage:
                            t.Kind = SpirvTypeKind.SampledImage;
                            break;

                        default:
                            return t;
                    }
                }

                e = t;
                t.ID = refID;
                context.SetAssignedElement(refID, t);
                context.ReplaceElement(inst, t);
            }

            return e as SpirvType;
        }
    }
}
