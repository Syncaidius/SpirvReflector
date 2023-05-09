using SpirvReflector;
using System;
using System.IO;

namespace ExampleProject
{
    internal class Program
    {
        static unsafe void Main(string[] args)
        {
            byte[] byteCode = null;
            using(FileStream stream = new FileStream("test_sprite_mfx.spirv", FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                    byteCode = reader.ReadBytes((int)stream.Length);
            }

            SpirvReflection.Load();
            IReflectionLogger logger = new SpirvConsoleLogger();

            fixed (byte* ptrByteCode = byteCode)
            {
                SpirvReflection reflect = new SpirvReflection(ptrByteCode, (nuint)byteCode.LongLength, logger);
            }

            SpirvReflection.Unload();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}