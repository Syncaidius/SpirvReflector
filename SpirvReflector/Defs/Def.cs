using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SpirvReflector
{
    internal class Def
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

        public InstructionDef[] Instructions { get; set; } = new InstructionDef[0];

        [JsonProperty("operand_kinds")]
        public OperandKindDef[] OperandKinds { get; set; } = new OperandKindDef[0];
    }
}
