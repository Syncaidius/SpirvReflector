using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirvReflector
{
    /// <summary>
    /// Represents a source language and text that the bytecode was translated from.
    /// </summary>
    public class SpirvSource : SpirvBytecodeElement
    {
        internal SpirvSource() { }

        public SpirvSourceLanguage Language { get; internal set; }

        public uint Version { get; internal set; }

        public string Filename { get; internal set; }

        public string Source { get; internal set; }
    }
}
