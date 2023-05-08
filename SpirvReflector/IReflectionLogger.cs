using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirvReflector
{
    public interface IReflectionLogger
    {
        void WriteLine(string value);

        void Error(string text);

        void Warning(string text);
    }
}
