using SpirvReflector;
using System.IO;

namespace ExampleProject
{
    internal class Program
    {
        static void Main(string[] args)
        {
            byte[] byteCode = null;
            using(FileStream stream = new FileStream("test_sprite_mfx.spirv", FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                    byteCode = reader.ReadBytes(stream.Lengt);
            }
        }
    }
}