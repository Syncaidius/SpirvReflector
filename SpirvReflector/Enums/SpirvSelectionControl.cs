using System;

namespace SpirvReflector
{
	[Flags]
	public enum SpirvSelectionControl
	{
		None = 0x0000,

		Flatten = 0x0001,

		DontFlatten = 0x0002,
	}
}

