using System;

namespace SpirvReflector
{
	[Flags]
	public enum SpirvFragmentShadingRate
	{
		Vertical2Pixels = 0x0001,

		Vertical4Pixels = 0x0002,

		Horizontal2Pixels = 0x0004,

		Horizontal4Pixels = 0x0008,
	}
}

