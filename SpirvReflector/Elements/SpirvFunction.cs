using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SpirvReflector
{
    public class SpirvFunction : SpirvBytecodeElement
    {
        public override string ToString()
        {
            string returnType = "";
            if (ReturnType != null)
                returnType = $"{(ReturnType.Name ?? ReturnType.Kind.ToString())} ";

            // TODO fetch function parameter definition
            SpirvFunctionControl funcControl = Start.GetOperandValue<SpirvFunctionControl>();
            SpirvIdRef defID = Start.GetOperand<SpirvIdRef>(3);

            string result = $"[FunctionControl.{funcControl}]";
            result += $"\n{returnType}Function()";
            result += $"\n{{";

            foreach (SpirvInstruction inst in Instructions)
                result += $"\n    {inst}";

            result += $"\n}}";
            return result;

        }
        internal List<SpirvInstruction> Instructions { get; } = new List<SpirvInstruction>();

        internal SpirvInstruction Start { get; set; }

        internal SpirvInstruction End { get; set; }

        public SpirvType ReturnType { get; internal set; }

        public SpirvFunctionControl Control { get; internal set; }

        public int InstructionCount => Instructions.Count;

        public List<SpirvWord> Parameters { get; } = new List<SpirvWord>();
    }
}
