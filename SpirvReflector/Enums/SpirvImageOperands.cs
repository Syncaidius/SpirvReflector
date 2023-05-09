using System;

namespace SpirvReflector
{
	[Flags]
	public enum SpirvImageOperands
	{
		None = 0x0000,

		Bias = 0x0001,

		Lod = 0x0002,

		Grad = 0x0004,

		ConstOffset = 0x0008,

		Offset = 0x0010,

		ConstOffsets = 0x0020,

		Sample = 0x0040,

		MinLod = 0x0080,

		MakeTexelAvailable = 0x0100,

		MakeTexelAvailableKHR = 0x0100,

		MakeTexelVisible = 0x0200,

		MakeTexelVisibleKHR = 0x0200,

		NonPrivateTexel = 0x0400,

		NonPrivateTexelKHR = 0x0400,

		VolatileTexel = 0x0800,

		VolatileTexelKHR = 0x0800,

		SignExtend = 0x1000,

		ZeroExtend = 0x2000,

		Nontemporal = 0x4000,

		Offsets = 0x10000,
	}
}

