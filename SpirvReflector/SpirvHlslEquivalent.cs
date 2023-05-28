using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirvReflector
{
    /// <summary>
    /// The equivalent HLSL type of a resource.
    /// </summary>
    public enum SpirvHlslEquivalent
    {
        None = 0,

        StructuredBuffer = 1,

        RWStructuredBuffer = 2,

        ConstantBuffer = 3,

        Texture1D = 4,

        Texture2D = 5,

        Texture3D = 6,

        RWTexture1D = 7,

        RWTexture2D = 8,

        RWTexture3D = 9,

        Texture1DArray = 10,

        Texture2DArray = 11,

        RWTexture1DArray = 12,

        RWTexture2DArray = 13,

        TextureCube = 14,

        TextureCubeArray = 15,

        SamplerState = 16,

        SamplerComparisonState = 17,

        ByteAddressBuffer = 18,

        RWByteAddressBuffer = 19,

        Buffer = 20,

        RWBuffer = 21,

        AppendStructuredBuffer = 22,

        ConsumeStructuredBuffer = 23,
    }
}
