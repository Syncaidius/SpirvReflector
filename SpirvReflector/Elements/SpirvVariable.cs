using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirvReflector
{
    public class SpirvVariable : SpirvDecoratedElement
    {
        public override string ToString()
        {
            string decoration = base.ToString();
            string name = string.IsNullOrWhiteSpace(Name) ? "_unnamed_" : $"{Name}";

            string r = $"[ID: {ID}]";
            if(decoration.Length > 0)
                r += $"{decoration} ";

            r += $"{Pointer.Type.Kind}* {name}";
            if(DefaultValue != null)
                r += $" = {DefaultValue}";

            return r;
        }

        public string Name { get; internal set; }

        public uint ID { get; internal set; }

        public SpirvPointer Pointer { get; internal set; }

        public SpirvConstant DefaultValue { get; internal set; }

        public SpirvStorageClass StorageClass { get; internal set; }
    }
}
