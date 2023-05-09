using SpirvReflector;
using System;
using System.IO;

namespace ExampleProject
{
    internal class Program
    {
        static unsafe void Main(string[] args)
        {
            SpirvReflection.Load();
            IReflectionLogger log = new SpirvConsoleLogger();

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

                fixed (byte* ptrByteCode = byteCode)
                {
                    SpirvReflection reflect = new SpirvReflection(ptrByteCode, (nuint)byteCode.LongLength, log);
                }

                Console.WriteLine();
            }

            SpirvReflection.Unload();
            log.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}