using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SpirvReflector
{
    public class SpirvConsoleLogger : IReflectionLogger
    {
        public void Error(string text)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
        }

        public void Warning(string text)
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.WriteLine(text);
        }

        public void WriteLine(string text)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.WriteLine(text);
        }
    }
}
