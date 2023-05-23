using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirvReflector
{
    public class SpirvImageType : SpirvType
    {
        public override string ToString()
        {
            return $"[ID: {ID}] Image {Dimension} -- SpirvImageFormat.{Format}";
        }
        /// <summary>
        /// Gets the dimensionality of the image.
        /// </summary>
        public SpirvDim Dimension { get; internal set; }

        /// <summary>
        /// Gets the depth mode of the image.
        /// </summary>
        public SpirvImageDepthMode Depth { get; internal set; }

        /// <summary>
        /// Gets whether or not the image type is an arrayed image.
        /// </summary>
        public bool IsArrayed { get; internal set; }

        /// <summary>
        /// Gets whether or not the image is multi-sampled.
        /// </summary>
        public bool IsMultisampled { get; internal set; }

        /// <summary>
        /// Gets the multisample mode of the image.
        /// </summary>
        public SpirvImageSampleMode SampleMode { get; internal set; }

        /// <summary>
        /// Gets the format of the image.
        /// </summary>
        public SpirvImageFormat Format { get; internal set; }

        /// <summary>
        /// Gets the optional access qualifier of the image.
        /// </summary>
        public SpirvAccessQualifier AccessQualifier { get; internal set; }

        internal SpirvImageType()
        {
            Kind = SpirvTypeKind.Image;
        }
    }

    public enum SpirvImageSampleMode
    {
        /// <summary>
        /// Indicates this is only known at run time, not at compile time
        /// </summary>
        Runtime = 0,

        /// <summary>
        /// Indicates an image compatible with sampling operations
        /// </summary>
        Sampled = 1,

        /// <summary>
        /// Indicates an image compatible with read/write operations (a storage or subpass data image).
        /// </summary>
        Storage = 2,
    }

    public enum SpirvImageDepthMode
    {
        /// <summary>
        /// Indicates that the image is not a depth image.
        /// </summary>
        NoDepth = 0,

        /// <summary>
        /// Indidcates the image is a depth image.
        /// </summary>
        Depth = 1,

        /// <summary>
        /// Means no indication as to whether this is a depth or non-depth image
        /// </summary>
        Unknown = 2,
    }
}
