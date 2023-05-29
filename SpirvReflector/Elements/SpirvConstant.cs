using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirvReflector
{
    public class SpirvConstant : SpirvBytecodeElement
    {
        public override string ToString()
        {
            string sValues = Values != null ? string.Join(", ", Values) : string.Empty;
            return $"[ID: {ID}] Constant {Type.Kind} = {sValues}";
        }

        /// <summary>
        /// Gets the assignment ID of the current <see cref="SpirvConstant"/>.
        /// </summary>
        public uint ID { get; internal set; }

        /// <summary>
        /// Gets the underlying SPIR-V type of the current <see cref="SpirvConstant"/>.
        /// </summary>
        public SpirvType Type { get; internal set; }

        /// <summary>
        /// Gets a list of component values which are used to initialize a value matching <see cref="Type"/>.
        /// </summary>
        public object[] Values { get; internal set; }
    }
}
