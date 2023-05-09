namespace SpirvReflector
{
	public enum SpirvFunctionControl
	{
		None = 0x0000,

		Inline = 0x0001,

		DontInline = 0x0002,

		Pure = 0x0004,

		Const = 0x0008,

		OptNoneINTEL = 0x10000,
	}
}

