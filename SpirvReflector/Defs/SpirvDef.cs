using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SpirvReflector
{
    internal class SpirvDef
    {
        [JsonProperty("major_version")]
        public int MajorVersion { get; set; }

        [JsonProperty("minor_version")]
        public int MinorVersion { get; set; }

        [JsonProperty("version")]
        public int Version
        {
            get => MinorVersion;
            set => MinorVersion = value;
        }

        [JsonProperty("revision")]
        public int Revision { get; set; }

        public SpirvInstructionDef[] Instructions { get; set; } = new SpirvInstructionDef[0];

        [JsonProperty("operand_kinds")]
        public SpirvOperandKindDef[] OperandKinds { get; set; } = new SpirvOperandKindDef[0];

        internal Dictionary<string, SpirvOperandKindDef> OperandKindLookup { get; } = new Dictionary<string, SpirvOperandKindDef>();


        internal Dictionary<SpirvOpCode, SpirvInstructionDef> OpcodeLookup { get; } = new Dictionary<SpirvOpCode, SpirvInstructionDef>();

        internal void BuildLookups(SpirvReflection reflection)
        {
            // Build operand-kind lookup
            foreach (SpirvOperandKindDef okd in OperandKinds)
            {
                okd.BuildLookups();
                OperandKindLookup.Add(okd.Kind, okd);
            }

            // Build opcode lookup
            foreach (SpirvInstructionDef inst in Instructions)
                OpcodeLookup.TryAdd((SpirvOpCode)inst.Opcode, inst);
        }

        public SpirvEnumerantDef GetEnumDef(Type enumType, object value)
        {
            if(!enumType.IsEnum)
                throw new ArgumentException("Type must be an enum.", "enumType");

            string tName = enumType.Name.Replace("Spirv","");
            SpirvEnumerantDef result = null;

            if(OperandKindLookup.TryGetValue(tName, out SpirvOperandKindDef okd))
            {
                string vName = value.ToString();

                // Try removing the type prefix from the value name.
                if (!okd.EnumerantLookup.TryGetValue(vName, out result) && vName.StartsWith(tName))
                {
                    vName = vName.Replace(tName, "");
                    okd.EnumerantLookup.TryGetValue(vName, out result);
                }
            }

            return result;
        }
    }
}
