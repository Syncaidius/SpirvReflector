﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirvReflector
{
    public class SpirvEntryPoint : SpirvBytecodeElement
    {
        /// <summary>
        /// Contains the execution mode and any extra parameters associated with it.
        /// </summary>
        public class ExecutionInfo
        {
            List<object> Parameters = new List<object>();

            internal void AddParameter(object p)
            {
                Parameters.Add(p);
            }

            /// <summary>
            /// Gets a list of extra parameters that were included with the execution mode.
            /// </summary>
            /// <returns></returns>
            public IReadOnlyList<object> GetParameters() => Parameters; 

            /// <summary>
            /// Gets the execution mode.
            /// </summary>
            public SpirvExecutionMode Mode { get; internal set; }

            /// <summary>
            /// Gets the execution model. This determines how the entry point is invoked. For example, as a vertex, geometry or tessellation shader.
            /// </summary>
            public SpirvExecutionModel Model { get; internal set; }
        }

        List<SpirvVariable> _inputs = new List<SpirvVariable>();

        List<SpirvVariable> _outputs = new List<SpirvVariable>();

        public override string ToString()
        {
            string o = string.Join(", ", Outputs.Select(v => v.Type.Kind));
            string i = string.Join(", ", Inputs.Select(v => v.Type.Kind));

            return $"{Execution.Model} Entry-point ({o}) = {Name}({i})";
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

        /// <summary>
        /// Gets information about how the entry point should be executed.
        /// </summary>
        public ExecutionInfo Execution { get; } = new ExecutionInfo();

        /// <summary>
        /// Gets the name of the entry point.
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// Gets the underlying function associated with the entry-point.
        /// </summary>
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
