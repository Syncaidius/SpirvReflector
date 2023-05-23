using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirvReflector
{
    public class SpirvEntryPoint : SpirvBytecodeElement
    {
        List<SpirvVariable> _inputs = new List<SpirvVariable>();

        List<SpirvVariable> _outputs = new List<SpirvVariable>();

        public override string ToString()
        {
            string o = string.Join(", ", Outputs.Select(v => v.Type.Kind));
            string i = string.Join(", ", Inputs.Select(v => v.Type.Kind));

            return $"{ExecutionModel} Entry-point ({o}) = {Name}({i})";
        }

        internal void AddVariable(SpirvVariable v)
        {
            if (v.StorageClass == SpirvStorageClass.Input)
                _inputs.Add(v);
            else if(v.StorageClass == SpirvStorageClass.Output)
                _outputs.Add(v);
            else
                throw new InvalidOperationException($"Cannot add non-input/output variables to entry point. Variable '{v.Name}' has storage class {v.StorageClass}.");
        }

        public SpirvExecutionModel ExecutionModel { get; internal set; }

        public string Name { get; internal set; }

        public SpirvFunction Function { get; internal set; }

        /// <summary>
        /// Gets a list of input variables for the current entry point.
        /// </summary>
        public IReadOnlyList<SpirvVariable> Inputs => _inputs;

        /// <summary>
        /// Gets a list of output variables for the current entry point.
        /// </summary>
        public IReadOnlyList<SpirvVariable> Outputs => _outputs;
    }
}
