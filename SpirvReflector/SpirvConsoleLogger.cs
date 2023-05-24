using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirvReflector
{
    /// <summary>
    /// A basic implementation of <see cref="IReflectionLogger"/> that writes to the console.
    /// </summary>
    public class SpirvConsoleLogger : IReflectionLogger
    {
        public SpirvConsoleLogger()
        {
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void Error(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
        }

        public void Warning(string text)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(text);
        }

        public void Write(string text, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
        }

        public void WriteLabeled(string label, string text, ConsoleColor labelColor = ConsoleColor.Yellow, ConsoleColor valueColor = ConsoleColor.White)
        {
            Write($"{label}: ", labelColor);
            WriteLine(text, valueColor);
        }

        public void WriteLine(string text, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(text);
        }
    }
}
