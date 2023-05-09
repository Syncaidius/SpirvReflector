﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirvReflector
{
    public unsafe class SpirvInstruction
    {
        uint* _ptr;
        uint _readIndex;

        internal SpirvInstruction(uint* ptr)
        {
            _ptr = ptr;
            _readIndex = 1;
            Words = new List<SpirvWord>();
        }

        public uint ReadWord()
        {
            return _ptr[_readIndex++];
        }

        public T ReadWord<T>() 
            where T : unmanaged
        {
            return *(T*)ReadWordPtr();
        }

        public uint* ReadWordPtr()
        {
            uint* ptr = _ptr + _readIndex;
            _readIndex++;
            return ptr;
        }

        /// <summary>
        /// Reads the specified number of words from the current <see cref="SpirvStream"/> and advances the stream position by the same amount.
        /// </summary>
        /// <param name="count">The number of words to read from the stream.</param>
        /// <returns></returns>
        public uint* ReadWords(uint count)
        {
            uint* ptrStart = _ptr + _readIndex;
            _readIndex += count;
            return ptrStart;
        }

        /// <summary>
        /// Reads all remaining words for the current <see cref="SpirvInstruction"/>. Outputs the number of words read to <paramref name="numWords"/>.
        /// </summary>
        /// <param name="numWords"></param>
        /// <returns></returns>
        public uint* ReadRemainingWords(out uint numWords)
        {
            numWords = WordCount - _readIndex;
            return ReadWords(numWords);
        }

        public override string ToString()
        {
            if (Words.Count > 0)
                return $"{OpCode} - {WordCount} words";
            else
                return $"{OpCode}";
        }

        /// <summary>
        /// Gets the total word count of the current <see cref="SpirvInstruction"/>, including its header word containing the word count and opcode.
        /// </summary>
        public uint WordCount => _ptr[0] >> 16;

        /// <summary>
        /// Gets <see cref="SpirvOpCode"/> of the current <see cref="SpirvInstruction"/>.
        /// </summary>
        public SpirvOpCode OpCode => (SpirvOpCode)(_ptr[0] & 0xFFFF);

        public List<SpirvWord> Words { get; }

        public SpirvIdResult Result { get; set; }

        public uint* Ptr => _ptr;
    }
}
