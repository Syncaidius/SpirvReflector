using SpirvReflector;
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

            IReflectionLogger logger = null;
            fixed (byte* ptrByteCode = byteCode)
            {
                Reflector reflect = new Reflector(ptrByteCode, (nuint)byteCode.LongLength, logger);
            }
        }
    }
}