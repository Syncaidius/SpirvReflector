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
        List<Type> _prerequisites = new List<Type>();

        protected void AddPrerequisite<T>()
            where T : SpirvProcessor
        {
            _prerequisites.Add(typeof(T));
        }

        /// <summary>
        /// Invoked during a 
        /// </summary>
        /// <param name="reflection"></param>
        /// <param name="result"></param>
        internal void Process(SpirvReflection reflection, SpirvReflectionResult result)
        {
            foreach(Type pType in _prerequisites)
                result.RunParser(pType, reflection);

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
