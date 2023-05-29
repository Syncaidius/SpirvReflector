using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SpirvReflector
{
    internal unsafe class SpirvReflectContext : IDisposable
    {
        internal SpirvInstruction[] Assignments;
        byte* _byteCode;
        nuint _numBytes;

        Dictionary<uint, SpirvBytecodeElement> _assignedElements { get; } = new Dictionary<uint, SpirvBytecodeElement>();

        internal SpirvReflectContext(SpirvReflection reflection, byte* sourceByteCode, nuint numBytes, SpirvReflectionFlags flags)
        {
            Reflection = reflection;
            Result = new SpirvReflectionResult(flags);
            _numBytes = numBytes;

            if (Result.Flags.Has(SpirvReflectionFlags.NoSafeCopy))
            {
                _byteCode = sourceByteCode;
            }
            else
            {
                _byteCode = (byte*)Marshal.AllocHGlobal((int)numBytes).ToPointer();
                Buffer.MemoryCopy(sourceByteCode, _byteCode, numBytes, numBytes);
            }
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

        public void Dispose()
        {
            if (_byteCode != null && !Result.Flags.Has(SpirvReflectionFlags.NoSafeCopy))
            {
                // Don't dispose if we're outputting instructions, which store a pointer to their place within the bytecode.
                // Disposing the bytecode would invalidate those pointers, so we'll leave disposal up to SpirvReflectionResult.
                if(!Result.Flags.Has(SpirvReflectionFlags.Instructions))
                    Marshal.FreeHGlobal(new IntPtr(_byteCode));
            }
        }

        internal SpirvReflection Reflection { get; }

        internal SpirvReflectionResult Result { get; }

        internal List<SpirvInstruction> Instructions { get; } = new List<SpirvInstruction>();

        internal List<SpirvBytecodeElement> Elements { get; } = new List<SpirvBytecodeElement>();

        internal IReflectionLogger Log => Reflection.Log;

        /// <summary>
        /// Gets a pointer to the underlying bytecode data.
        /// </summary>
        internal byte* ByteCode => _byteCode;

        /// <summary>
        /// Gets the number of bytes in <see cref="ByteCode"/>.
        /// </summary>
        internal nuint NumBytes => _numBytes;
    }
}
