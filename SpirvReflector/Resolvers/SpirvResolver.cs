using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpirvReflector
{
    /// <summary>
    /// Resolves instruction into usable reflection elements.
    /// </summary>
    internal abstract class SpirvResolver
    {
        /// <summary>
        /// Invoked when the resolver should iterate over the bytecode elements to resolve.
        /// </summary>
        /// <param name="context"></param>
        internal abstract void Resolve(SpirvReflectContext context);

        protected virtual void OnCompleted() { }
    }

    internal abstract class SpirvResolver<T> : SpirvResolver
        where T : SpirvBytecodeElement
    {
        internal override sealed void Resolve(SpirvReflectContext context)
        {
            List<SpirvBytecodeElement> elements = new List<SpirvBytecodeElement>(context.Elements);
            foreach (SpirvBytecodeElement el in elements)
            {
                if (el is not T obj)
                    continue;

                OnResolve(context, obj);
            }

            OnCompleted();
        }

        protected abstract void OnResolve(SpirvReflectContext context, T o);
    }
}
