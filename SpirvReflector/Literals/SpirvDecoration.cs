﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirvReflector
{
    public class SpirvDecoration : SpirvLiteral
    {
        public override unsafe void Read(SpirvInstruction instruction)
        {
            DecorationType = instruction.ReadWord<SpirvDecorationType>();

            // TODO: Read decoration literals, if any.
            //       A JSON map will be needed to map decoration types to included literals (and tell us how many to read).
            //       See: https://registry.khronos.org/SPIR-V/specs/unified1/SPIRV.html#Decoration
        }

        public SpirvDecorationType DecorationType { get; private set; }
    }
}
