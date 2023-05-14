using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpirvReflector
{
    internal abstract class SpirvProcessor
    {
        /// <summary>
        /// Invoked during a 
        /// </summary>
        /// <param name="reflection"></param>
        /// <param name="result"></param>
        internal void Process(SpirvReflection reflection, SpirvReflectionResult result)
        {
            List<SpirvBytecodeElement> elements = new List<SpirvBytecodeElement>(result.Elements);
            foreach (SpirvBytecodeElement el in elements)
            {
                if (el is not SpirvInstruction inst)
                    continue;

                OnProcess(reflection, result, inst);
            }

            OnCompleted();
        }

        protected abstract void OnProcess(SpirvReflection reflection, SpirvReflectionResult result, SpirvInstruction inst);

        protected virtual void OnCompleted() { }
    }
}
