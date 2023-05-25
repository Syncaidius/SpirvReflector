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
        /// 
        /// </summary>
        /// <param name="context"></param>
        internal void Process(SpirvReflectContext context)
        {
            List<SpirvBytecodeElement> elements = new List<SpirvBytecodeElement>(context.Elements);
            foreach (SpirvBytecodeElement el in elements)
            {
                if (el is not SpirvInstruction inst)
                    continue;

                OnProcess(context, inst);
            }

            OnCompleted();
        }

        protected abstract void OnProcess(SpirvReflectContext context, SpirvInstruction inst);

        protected virtual void OnCompleted() { }
    }
}
