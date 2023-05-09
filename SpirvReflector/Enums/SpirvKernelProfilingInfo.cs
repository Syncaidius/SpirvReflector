using System;

namespace SpirvReflector
{
	[Flags]
	public enum SpirvKernelProfilingInfo
	{
		None = 0x0000,

		CmdExecTime = 0x0001,
	}
}

