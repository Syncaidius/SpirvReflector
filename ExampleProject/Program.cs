using SpirvReflector;
using System;
using System.IO;

namespace ExampleProject
{
    internal class Program
    {
        static unsafe void Main(string[] args)
        {
            // Initialize a logger and SpirvReflection instance. A single instance can safely be used to reflect multiple SPIR-V bytecode files and is thread-safe.
            IReflectionLogger log = new SpirvConsoleLogger();
            SpirvReflectionFlags flags = SpirvReflectionFlags.LogInstructions |
                SpirvReflectionFlags.LogAssignments |
                SpirvReflectionFlags.LogResult |
                SpirvReflectionFlags.LogDebug |
                SpirvReflectionFlags.Instructions;

            SpirvReflection reflection = new SpirvReflection(log);

            // Load each .spirv bytecode file in the current directory and run SpirvReflection.Reflect() on it.
            string[] spirvFiles = Directory.GetFiles(".", "*.spirv");
            foreach(string filename in spirvFiles)
            {
                log.WriteLine("--------------------------------------------------");
                log.WriteLine($"{filename}");
                log.WriteLine("--------------------------------------------------");

                byte[] byteCode = null;
                using (FileStream stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
                {
                    using (BinaryReader reader = new BinaryReader(stream))
                        byteCode = reader.ReadBytes((int)stream.Length);
                }

                SpirvReflectionResult result = reflection.Reflect(byteCode, flags);
                Console.WriteLine("--------------------------------------------------");
            }

            log.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}