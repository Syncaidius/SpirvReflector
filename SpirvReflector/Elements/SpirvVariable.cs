using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirvReflector
{
    public class SpirvVariable : SpirvBytecodeElement
    {
        public override string ToString()
        {
            string name = string.IsNullOrWhiteSpace(Name) ? " _unnamed_" : $" {Name}";
            return $"{Pointer.Type.Kind}*{name} ({ID})";
        }

        public string Name { get; internal set; }

        public uint ID { get; internal set; }

        public SpirvPointer Pointer { get; internal set; }

        public SpirvConstant DefaultValue { get; internal set; }

        public SpirvStorageClass StorageClass { get; internal set; }
    }
}
