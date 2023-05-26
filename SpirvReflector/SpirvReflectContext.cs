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

        Dictionary<uint, SpirvBytecodeElement> _assignedElements { get; } = new Dictionary<uint, SpirvBytecodeElement>();

        internal SpirvReflectContext(SpirvReflection reflection, SpirvReflectionFlags flags)
        {
            Reflection = reflection;
            Flags = flags;
        }

        internal void ReplaceElement(SpirvBytecodeElement element, SpirvBytecodeElement replacement)
        {
            int index = Elements.IndexOf(element);
            if (index > -1)
                Elements[index] = replacement;
        }

        internal void SetAssignedElement(uint id, SpirvBytecodeElement element)
        {
            _assignedElements[id] = element;
        }

        internal bool TryGetAssignedElement<T>(uint id, out T element, bool supressWarnings = false)
            where T : SpirvBytecodeElement
        {
            if (_assignedElements.TryGetValue(id, out SpirvBytecodeElement e))
            {
                element = e as T;
                if (element != null)
                    return true;
                else
                    Log.Warning($"Found element %{id} but it was of type '{e.GetType().Name}' not '{typeof(T)}'");
            }
            else
            {
                if (!supressWarnings)
                {
                    Log.Warning($"Unable to find target element %{id}");
                    if (Assignments[id] != null)
                        Log.Warning($"\t But found matching instruction: {Assignments[id]}");
                }
            }

            element = null;
            return false;
        }

        internal SpirvReflection Reflection { get; }

        internal SpirvReflectionResult Result { get; } = new SpirvReflectionResult();

        internal List<SpirvInstruction> Instructions { get; } = new List<SpirvInstruction>();

        internal List<SpirvBytecodeElement> Elements { get; } = new List<SpirvBytecodeElement>();

        /// <summary>
        /// Gets the flags that were passed in during the associated <see cref="SpirvReflection.Reflect(byte[], SpirvReflectionFlags)"/> or 
        /// <see cref="SpirvReflection.Reflect(void*, nuint, SpirvReflectionFlags)"/> call.
        /// </summary>
        internal SpirvReflectionFlags Flags { get; }

        internal IReflectionLogger Log => Reflection.Log;
    }
}
