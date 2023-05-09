﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirvReflector
{
    /// <summary>
    /// A custom stream for reading SPIR-V bytecode.
    /// </summary>
    public unsafe class SpirvStream
    {
        const uint MAGIC_NUMBER = 0x07230203;

        uint* _ptrStart;
        uint* _ptrEnd;
        uint* _ptr;
        ulong _numWords;

        internal SpirvStream(void* byteCode, ulong numBytes)
        {
            if (numBytes % 4 != 0)
                throw new ArgumentException("Bytecode size must be a multiple of 4.", nameof(numBytes));

            _ptrEnd = (uint*)((byte*)byteCode + numBytes);
            _ptrStart = (uint*)byteCode;
            _ptr = _ptrStart;
            _numWords = numBytes / 4U;

            // First op is always the magic number.
            if (ReadWord() != MAGIC_NUMBER)
                throw new ArgumentException("Invalid SPIR-V bytecode.", nameof(byteCode));
        }

        /// <summary>
        /// Reads a single SPIR-V word (<see cref="uint"/>) from the stream and advances the stream position.
        /// </summary>
        /// <returns></returns>
        public uint ReadWord()
        {
            uint val = *_ptr;
            _ptr++;
            return val;
        }

        /// <summary>
        /// Reads <see cref="SpirvInstruction"/> from the stream and advances the stream position by the number of words that make up the instruction.
        /// </summary>
        /// <returns></returns>
        public SpirvInstruction ReadInstruction()
        {
            SpirvInstruction inst = new SpirvInstruction(_ptr);
            _ptr += inst.WordCount;
            return inst;
        }

        public uint Position => (uint)(_ptr - _ptrStart) / 4U;

        public bool IsEndOfStream => _ptr >= _ptrEnd;

        public ulong WordCount => _numWords;
    }
}
