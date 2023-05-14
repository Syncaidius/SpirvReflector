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
        /// Gets the dimensions of the type. Each element in the array represents the size of a dimension. 
        /// <para>The minimium dimension size is 1.</para>
        /// </summary>
        public uint[] Dimensions { get; internal set; }
    }

    public class SpirvTypeMember
    {
        internal SpirvTypeMember() { }

        /// <summary>
        /// Gets the byte offset of the member from the start of its parent structure.
        /// </summary>
        public uint ByteOffset { get; internal set; }

        /// <summary>
        /// Gets the number of bytes occupied by the member.
        /// </summary>
        public uint NumBytes { get; internal set; }

        /// <summary>
        /// Gets the name of the member.
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// Gets t type of value stored by the member.
        /// </summary>
        public SpirvType Type { get; internal set; }

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
