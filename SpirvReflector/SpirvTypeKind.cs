using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirvReflector
{
    /// <summary>
    /// Represents the type-kind of a <see cref="SpirvType"/>.
    /// </summary>
    public enum SpirvTypeKind
    {
        /// <summary>
        /// The type is invalid.
        /// </summary>
        Invalid,

        /// <summary>
        /// Void type.
        /// </summary>
        Void,

        /// <summary>
        /// Boolean type.
        /// </summary>
        Bool,

        /// <summary>
        /// Signed integer type.
        /// </summary>
        Int,

        /// <summary>
        /// Unsigned integer type.
        /// </summary>
        UInt,

        /// <summary>
        /// Floating-point type.
        /// </summary>
        Float,

        /// <summary>
        /// A vector type.
        /// </summary>
        Vector,

        /// <summary>
        /// A matrix type.
        /// </summary>
        Matrix,

        /// <summary>
        /// An image type. Usually part of a <see cref="SpirvImageType"/>.
        /// </summary>
        Image,

        /// <summary>
        /// A sampler type.
        /// </summary>
        Sampler,
        /// <summary>
        /// A sampled image type.
        /// </summary>
        SampledImage,

        /// <summary>
        /// An array type where the dimensions are known at compile-time.
        /// </summary>
        Array,

        /// <summary>
        /// A array type where the dimensions are defined at runtime.
        /// </summary>
        RuntimeArray,

        /// <summary>
        /// A structured type with members made up of variables, each with their own <see cref="SpirvType"/>.
        /// </summary>
        Struct,

        /// <summary>
        /// An opaque type.
        /// </summary>
        Opaque,

        /// <summary>
        /// A pointer type.
        /// </summary>
        Pointer,

        /// <summary>
        /// A function.
        /// </summary>
        Function,

        /// <summary>
        /// An event.
        /// </summary>
        Event,

        /// <summary>
        /// A device event.
        /// </summary>
        DeviceEvent,

        /// <summary>
        /// Represents a reserved ID.
        /// </summary>
        ReserveId,

        /// <summary>
        /// A queue type.
        /// </summary>
        Queue,

        /// <summary>
        /// A pipe type.
        /// </summary>
        Pipe,

        /// <summary>
        /// A forward-declared pointer type.
        /// </summary>
        ForwardPointer,

        /// <summary>
        /// A pipe storage type.
        /// </summary>
        PipeStorage,

        /// <summary>
        /// A named barrier type.
        /// </summary>
        NamedBarrier,

        /// <summary>
        /// An acceleration structure type.
        /// </summary>
        AccelerationStructure,

        /// <summary>
        /// A ray query type.
        /// </summary>
        RayQuery,
    }
}
