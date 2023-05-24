using SpirvReflector;
using System;
using System.IO;

namespace ExampleProject
{
    internal class Program
    {
        static unsafe void Main(string[] args)
        {
            IReflectionLogger log = new SpirvConsoleLogger();
            SpirvReflection reflection = new SpirvReflection(log, 
                SpirvReflectionFlags.LogInstructions | 
                SpirvReflectionFlags.LogAssignments | 
                SpirvReflectionFlags.LogResult | 
                SpirvReflectionFlags.LogDebug);

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

                // Produce a SPIR-V reflection result that we could use in our application to automatically map data to shader inputs/outputs.
                SpirvReflectionResult result = null;
                fixed (byte* ptrByteCode = byteCode)
                    result = reflection.Reflect(ptrByteCode, (nuint)byteCode.LongLength);

                Console.WriteLine();
            }

            log.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}