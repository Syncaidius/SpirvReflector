using System;
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

    /// <summary>
    /// Reads a single word from the provided pointer as a value of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of value to read from the word.</typeparam>
    public class SpirvWord<T> : SpirvWord
        where T : unmanaged
    {
        bool _isFlagEnum;
        bool _isEnum;

        public SpirvWord()
        {
            _isEnum = typeof(T).IsEnum;
            _isFlagEnum = _isEnum && typeof(T).GetCustomAttributes(typeof(FlagsAttribute), false).Length > 0;
        }

        public override unsafe void Read(SpirvInstruction instruction)
        {
            Value = instruction.ReadWord<T>();
        }

        public override string ToString()
        {
            if(_isEnum)
                return $"{typeof(T).Name}." + (_isFlagEnum ? $"[{Value}]" : Value.ToString());
            else
                return Value.ToString();
        }

        public T Value;
    }
}
