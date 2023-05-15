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
            SpirvIdRef defID = Start.GetOperand<SpirvIdRef>(3); // ref to OpTypeFunction 

            string result = $"[ID:{ID}] [FunctionControl.{funcControl}]";
            string name = string.IsNullOrWhiteSpace(Name) ? "" : $" {Name}";
            result += $"\n{returnType}Function{name}()";
            result += $"\n{{";

            foreach (SpirvInstruction inst in Instructions)
                result += $"\n    {inst}";

            result += $"\n}}";
            return result;
        }

        /// <summary>
        /// Gets the name of the current function.
        /// </summary>
        public string Name { get; internal set; }

        internal List<SpirvInstruction> Instructions { get; } = new List<SpirvInstruction>();

        internal SpirvInstruction Start { get; set; }

        internal SpirvInstruction End { get; set; }

        public SpirvType ReturnType { get; internal set; }

        public SpirvFunctionControl Control { get; internal set; }

        /// <summary>
        /// Gets the ID that was assigned to the type in the SPIR-V bytecode.
        /// </summary>
        public uint ID { get; internal set; }

        public int InstructionCount => Instructions.Count;

        public List<SpirvInstruction> Parameters { get; } = new List<SpirvInstruction>();
    }
}
