using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirvReflector
{
    [Flags]
    public enum SpirvReflectionFlags
    {
        /// <summary>
        /// No flags.
        /// </summary>
        None = 0,

        /// <summary>
        /// Debug messages are output to the provided <see cref="IReflectionLogger"/>.
        /// </summary>
        LogDebug = 1 << 0,

        /// <summary>
        /// The bytecode instructions are output to the provided <see cref="IReflectionLogger"/>.
        /// </summary>
        LogInstructions = 1 << 1,

        /// <summary>
        /// The detected assignments are output to the provided <see cref="IReflectionLogger"/>.
        /// </summary>
        LogAssignments = 1 << 2,

        /// <summary>
        /// The final processed elements are output to the provided <see cref="IReflectionLogger"/>.
        /// </summary>
        LogResult = 1 << 3,

        /// <summary>
        /// The returned <see cref="SpirvReflectionResult"/> will be populated with the instructions that were read from the bytecode.
        /// </summary>
        Instructions = 1 << 4,
    }

    public static class SpirvReflectionFlagsExtension
    {
        public static bool Has(this SpirvReflectionFlags flags, SpirvReflectionFlags value)
        {
            return (flags & value) == value;
        }
    }
}
