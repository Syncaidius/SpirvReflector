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

            r += $"{Type.Kind}* {name}";
            if(DefaultValue != null)
                r += $" = {DefaultValue}";

            return r;
        }

        /// <summary>
        /// Gets the name of the variable.
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// Gets the SPIR-V assignment ID of the variable.
        /// </summary>
        public uint ID { get; internal set; }

        /// <summary>
        /// Gets the binding slot or ID of the variable.
        /// </summary>
        public uint Binding { get; internal set; }

        /// <summary>
        /// Gets the pointer of the variable.
        /// </summary>
        public SpirvType Type { get; internal set; }

        /// <summary>
        /// Gets the default value of the variable, if any.
        /// </summary>
        public SpirvConstant DefaultValue { get; internal set; }

        /// <summary>
        /// Gets the storage class of the variable.
        /// </summary>
        public SpirvStorageClass StorageClass { get; internal set; }
    }
}
