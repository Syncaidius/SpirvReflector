using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpirvReflector
{
    public class SpirvLiteralString : SpirvWord
    {
        public string Value;

        public override unsafe void Read(SpirvInstruction instruction)
        {
            Value = "";

            while (instruction.UnreadWordCount > 0)
            {
                // Read only until we hit a null terminator.
                uint* ptr = instruction.ReadWordPtr();

                string result = Encoding.UTF8.GetString((byte*)ptr, sizeof(uint));
                int nullIndex = result.IndexOf('\0');

                if (nullIndex > -1)
                {
                    Value += result.Substring(0, nullIndex);
                    break;
                }
                else
                {
                    Value += result;
                }
            }
        }

        public override string ToString()
        {
            return $"'{Value}'";
        }
    }
}
