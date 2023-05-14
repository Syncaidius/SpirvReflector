using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirvReflector
{
    public interface IReflectionLogger
    {
        void Write(string value, ConsoleColor color = ConsoleColor.White);

        void WriteLine(string value, ConsoleColor color = ConsoleColor.White);

        void WriteLabeled(string label, string text, ConsoleColor labelColor = ConsoleColor.Yellow, ConsoleColor valueColor = ConsoleColor.White);

        void Error(string text);

        void Warning(string text);
    }
}
