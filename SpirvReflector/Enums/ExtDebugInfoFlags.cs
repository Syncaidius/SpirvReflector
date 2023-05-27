using System;

namespace SpirvReflector
{
	[Flags]
	public enum ExtDebugInfoFlags
	{
		None = 0x0000,

		FlagIsProtected = 0x01,

		FlagIsPrivate = 0x02,

		FlagIsPublic = 0x03,

		FlagIsLocal = 0x04,

		FlagIsDefinition = 0x08,

		FlagFwdDecl = 0x10,

		FlagArtificial = 0x20,

		FlagExplicit = 0x40,

		FlagPrototyped = 0x80,

		FlagObjectPointer = 0x100,

		FlagStaticMember = 0x200,

		FlagIndirectVariable = 0x400,

		FlagLValueReference = 0x800,

		FlagRValueReference = 0x1000,

		FlagIsOptimized = 0x2000,
	}
}

