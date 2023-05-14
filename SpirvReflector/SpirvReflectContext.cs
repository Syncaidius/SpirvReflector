using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirvReflector
{
    internal class SpirvReflectContext
    {
        internal SpirvInstruction[] Assignments;

        internal SpirvReflectContext(SpirvReflection reflection)
        {
            Reflection = reflection;
        }

        internal void ReplaceElement(SpirvBytecodeElement element, SpirvBytecodeElement replacement)
        {
            int index = Elements.IndexOf(element);
            if (index > -1)
                Elements[index] = replacement;
        }

        internal SpirvReflection Reflection { get; }

        internal SpirvReflectionResult Result { get; } = new SpirvReflectionResult();

        internal List<SpirvInstruction> Instructions { get; } = new List<SpirvInstruction>();

        internal List<SpirvBytecodeElement> Elements { get; } = new List<SpirvBytecodeElement>();

        internal Dictionary<uint, SpirvType> OpTypes { get; } = new Dictionary<uint, SpirvType>();

        internal List<SpirvFunction> Functions { get; } = new List<SpirvFunction>();

        internal IReflectionLogger Log => Reflection.Log;
    }
}
