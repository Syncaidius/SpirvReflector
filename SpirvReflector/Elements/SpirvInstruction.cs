using System;
using System.Collections.Generic;
using System.Linq;

namespace SpirvReflector
{
    public unsafe class SpirvInstruction : SpirvBytecodeElement
    {
        uint* _ptr;
        uint _readIndex;

        internal SpirvInstruction(uint* ptr)
        {
            _ptr = ptr;
            _readIndex = 1;
            Operands = new List<SpirvWord>();
        }

        public T GetOperand<T>()
            where T : SpirvWord
        {
            foreach(SpirvWord w in Operands)
            {
                if (w is T wt)
                    return wt;
                else
                    throw new InvalidOperationException($"The current instruction does not contain an operand of type '{typeof(T).Name}'");
            }

            return null;
        }

        public T GetOperand<T>(int index)
            where T : SpirvWord
        {
            if (index >= Operands.Count)
                return null;

            SpirvWord word = Operands[index];

            if (word is T wt)
                return wt;
            else
                throw new InvalidOperationException($"The word at index {index} is not of the type '{typeof(T).Name}'");
        }

        /// <summary>
        /// Attempts to retrieve an operand value of the specified type, if present. Returns the default value of <typeparamref name="T"/> if no operand of the specified type is present.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetOperandValue<T>()
            where T : unmanaged
        {
            foreach (SpirvWord w in Operands)
            {
                if (w is SpirvWord<T> wt)
                    return wt.Value;
            }

            return default;
        }

        public T GetOperandValue<T>(int index)
            where T : unmanaged
        {
            return (Operands[index] as SpirvWord<T>).Value;
        }

        public string GetOperandString(int index)
        {
            if (index >= Operands.Count)
                return null;

            SpirvLiteralString str = Operands[index] as SpirvLiteralString;
            return str?.Text;
        }

        public uint ReadWord()
        {
            return _ptr[_readIndex++];
        }

        public T ReadValue<T>() 
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

        public override string ToString()
        {
            string opResult = Result != null ? $"{Result} = " : "";
            string operands = GetOperandString();
            return $"{opResult}{OpCode}{operands}";
        }

        internal string GetOperandString()
        {
            string op = string.Join(", ", Operands.Where(o => o is not SpirvIdResult));
            if(op.Length > 0)
                op = " -- " + op;

            return op;
        }

        /// <summary>
        /// Gets the total word count of the current <see cref="SpirvInstruction"/>, including its header word containing the word count and opcode.
        /// </summary>
        public uint WordCount => _ptr[0] >> 16;

        /// <summary>
        /// Gets <see cref="SpirvOpCode"/> of the current <see cref="SpirvInstruction"/>.
        /// </summary>
        public SpirvOpCode OpCode => (SpirvOpCode)(_ptr[0] & 0xFFFF);

        /// <summary>
        /// Gets a list of all detected operands in the current <see cref="SpirvInstruction"/>.
        /// </summary>
        public List<SpirvWord> Operands { get; }

        /// <summary>
        /// Gets the assignment ID of the current <see cref="SpirvInstruction"/>, if present.
        /// </summary>
        public SpirvIdResult Result { get; set; }

        /// <summary>
        /// Gets the number of remaining unread words in the current <see cref="SpirvInstruction"/>.
        /// </summary>
        public uint UnreadWordCount => WordCount - _readIndex;
    }
}
