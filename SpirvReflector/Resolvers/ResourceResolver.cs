using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirvReflector
{
    internal class ResourceResolver : SpirvResolver<SpirvVariable>
    {
        protected override void OnResolve(SpirvReflectContext context, SpirvVariable v)
        {
            if (TryResolveImage(context, v))
                return;

            if (TryResolveBuffer(context, v))
                return;

            if (TryResolveConstantBuffer(context, v))
                return;
        }

        private bool TryResolveConstantBuffer(SpirvReflectContext context, SpirvVariable v)
        {
            if (v.Type.Kind != SpirvTypeKind.Struct)
                return false;

            if(v.Type.Decorations.Has(SpirvDecoration.Block) || v.Type.Name == $"type.$Globals")
            {
                context.Result.AddResource(v);
                v.HlslEquivalent = SpirvHlslEquivalent.ConstantBuffer;
                return true;
            }

            return false;
        }

        private bool TryResolveBuffer(SpirvReflectContext context, SpirvVariable v)
        {
            if (v.Type.Kind == SpirvTypeKind.Struct)
                return false;

            if (v.Type.Decorations.Has(SpirvDecoration.BufferBlock) || 
                (v.Type.Name != null && v.Type.Name.StartsWith("type.StructuredBuffer")))
            {
                context.Result.AddResource(v);

                // Determine structured buffer type
                if (v.Type.Members.Count > 0)
                {
                    if (v.Type.Members[0].Decorations.Has(SpirvDecoration.NonWritable))
                        v.HlslEquivalent = SpirvHlslEquivalent.StructuredBuffer;
                    else
                        v.HlslEquivalent = SpirvHlslEquivalent.RWStructuredBuffer;
                }

                return true;
            }

            return false;
        }

        private bool TryResolveImage(SpirvReflectContext context, SpirvVariable v)
        {
            if (v.Type is SpirvImageType || v.StorageClass == SpirvStorageClass.Image)
            {
                context.Result.AddResource(v);

                SpirvImageType imgType = v.Type as SpirvImageType;
                switch (imgType.Dimension)
                {
                    case SpirvDim.Dim1D:
                        v.HlslEquivalent = SpirvHlslEquivalent.Texture1D;
                        break;

                    case SpirvDim.Dim2D:
                        v.HlslEquivalent = SpirvHlslEquivalent.Texture2D;
                        break;

                    case SpirvDim.Dim3D:
                        v.HlslEquivalent = SpirvHlslEquivalent.Texture3D;
                        break;
                }

                return true;
            }

            return false;
        }
    }
}
