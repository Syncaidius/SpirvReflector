using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SpirvReflector
{
    public class SpirvType : SpirvBytecodeElement
    {
        public override string ToString()
        {
            string name = string.IsNullOrWhiteSpace(Name) ? "" : $"({Name})";
            return $"Type{name} - {Kind}";
        }

        /// <summary>
        /// Gets the kind of SPIR-V type.
        /// </summary>
        public SpirvTypeKind Kind { get; internal set; }

        /// <summary>
        /// Gets the name of the type that was provided in the SPIR-V bytecode.
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// Gets the translated <see cref="Type"/> of the SPIR-V type.
        /// </summary>
        public Type ElementType { get; internal set; }

        /// <summary>
        /// Gets the dimensions of the type. Each element in the array represents the size of a dimension.
        /// </summary>
        public uint[] Dimensions { get; internal set; }
    }

    public enum SpirvTypeKind
    {
        Void,
        Bool,
        Int,
        UInt,
        Float,
        Vector,
        Matrix,
        Image,
        Sampler,
        SampledImage,
        Array,
        RuntimeArray,
        Struct,
        Opaque,
        Pointer,
        Function,
        Event,
        DeviceEvent,
        ReserveId,
        Queue,
        Pipe,
        ForwardPointer,
        PipeStorage,
        NamedBarrier,
        AccelerationStructure,
        RayQuery,
        Max,
    }
}
