using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirvReflector
{
    public class SpirvDecorationData
    {
        Dictionary<SpirvDecoration, List<object>> _decorations = new Dictionary<SpirvDecoration, List<object>>();

        /// <summary>
        /// Adds a <see cref="SpirvDecoration"/> to the current <see cref="SpirvDecorationData"/> instance. The same <see cref="SpirvDecoration"/> cannot be added twice.
        /// </summary>
        /// <param name="decoration">The <see cref="SpirvDecoration"/>.</param>
        /// <param name="values">The list of decoration parameters.</param>
        /// <exception cref="InvalidOperationException"></exception>
        internal void Add(SpirvDecoration decoration, List<object> values)
        {
            if (_decorations.ContainsKey(decoration))
                throw new InvalidOperationException($"The same decoration ({decoration}) cannot be added twice.");

            _decorations.Add(decoration, values ?? new List<object>());
        }

        /// <summary>
        /// Checks whether the current <see cref="SpirvDecorationData"/> instance contains a specific <see cref="SpirvDecoration"/>.
        /// </summary>
        /// <param name="decoration">The <see cref="SpirvDecoration"/>.</param>
        /// <returns></returns>
        public bool Has(SpirvDecoration decoration)
        {
            return _decorations.ContainsKey(decoration);
        }

        /// <summary>
        /// Gets a list of <see cref="SpirvDecoration"/> contained in the current <see cref="SpirvDecorationData"/> instance.
        /// </summary>
        public IEnumerable<SpirvDecoration> Keys => _decorations.Keys;

        /// <summary>
        /// Retrieves the list of object parameters for a specific <see cref="SpirvDecoration"/>. Returns null if the key does not exist.
        /// </summary>
        /// <param name="key">The <see cref="SpirvDecoration"/> for which to retrieve object parameters.</param>
        /// <returns></returns>
        public IReadOnlyList<object> this[SpirvDecoration key]
        {
            get
            {
                _decorations.TryGetValue(key, out List<object> values);
                return values;
            }
        }
    }
}
