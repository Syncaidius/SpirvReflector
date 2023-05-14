﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SpirvReflector
{
    public class SpirvType : SpirvBytecodeElement
    {
        List<SpirvTypeMember> _members = new List<SpirvTypeMember>();

        internal void AddMember(SpirvTypeMember member)
        {
            _members.Add(member);
        }

        public override string ToString()
        {
            string elType = ElementType == null ? "" : $" -- Element: [{ElementType}]";
            return $"Type({ID}) - {Kind} -- Length: {Length} -- Bytes: {NumBytes}{elType}";
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
        public uint Length { get; internal set; }

        /// <summary>
        /// Gets the <see cref="SpirvType"/> of the elements that are part of the current type.
        /// <para>if any. If the element is not an array-based type such as a vector or matrix, then this property is null.</para>
        /// </summary>
        public SpirvType ElementType { get; internal set; }

        /// <summary>
        /// Gets the total number of bytes occupied by the type.
        /// </summary>
        public uint NumBytes { get; internal set; }

        /// <summary>
        /// Gets a read-only list of the members that are part of the current type.
        /// </summary>
        public IReadOnlyList<SpirvTypeMember> Members => _members;

        /// <summary>
        /// Gets the ID that was assigned to the type in the SPIR-V bytecode.
        /// </summary>
        public uint ID { get; internal set; }
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
        /// Gets the <see cref="SpirvType"/> of value stored by the member.
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
