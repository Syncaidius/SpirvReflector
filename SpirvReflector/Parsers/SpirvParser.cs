using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpirvReflector
{
    internal abstract class SpirvParser
    {
        List<Type> _prerequisites = new List<Type>();

        protected void AddPrerequisite<T>()
            where T : SpirvParser
        {
            _prerequisites.Add(typeof(T));
        }

        /// <summary>
        /// Invoked during a 
        /// </summary>
        /// <param name="reflection"></param>
        /// <param name="result"></param>
        internal void Parse(SpirvReflection reflection, SpirvReflectionResult result)
        {
            foreach(Type pType in _prerequisites)
                result.RunParser(pType, reflection);

            List<SpirvBytecodeElement> elements = new List<SpirvBytecodeElement>(result.Elements);
            foreach (SpirvBytecodeElement el in elements)
            {
                if (el is not SpirvInstruction inst)
                    continue;

                OnParse(reflection, result, inst);
            }

            OnCompleted();
        }

        protected abstract void OnParse(SpirvReflection reflection, SpirvReflectionResult result, SpirvInstruction inst);

        protected virtual void OnCompleted() { }
    }
}
