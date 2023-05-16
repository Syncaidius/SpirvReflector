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

        internal void AddDecoration(SpirvDecoration decoration, List<object> values)
        {
            if (_decorations.ContainsKey(decoration))
                throw new InvalidOperationException($"The same decoration ({decoration}) cannot be added twice.");

            _decorations.Add(decoration, values);
        }

        public bool HasDecoration(SpirvDecoration decoration)
        {
            return _decorations.ContainsKey(decoration);
        }

        public IReadOnlyList<object> GetDecorationValues(SpirvDecoration decoration)
        {
            return _decorations[decoration];
        }

        public IEnumerable<SpirvDecoration> Keys => _decorations.Keys;
    }
}
