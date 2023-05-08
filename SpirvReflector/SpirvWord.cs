﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirvReflector
{
    public abstract class SpirvWord
    {
        public virtual unsafe void Read(SpirvInstruction instruction)
        {
            instruction.ReadWord();
        }

        public string Name { get; internal set; }
    }
}
