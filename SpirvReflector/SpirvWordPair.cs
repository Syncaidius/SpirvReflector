using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirvReflector.Literals
{
    public abstract class SpirvWordPair<A, B> : SpirvWord
        where A : SpirvWord, new()
        where B : SpirvWord, new()
    {
        public A First = new A();

        public B Second = new B();

        public override void Read(SpirvInstruction instruction)
        {
            First.Read(instruction);
            Second.Read(instruction);
        }
    }
}
