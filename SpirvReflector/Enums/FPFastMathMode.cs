namespace Molten.Graphics.Vulkan
{
  public enum FPFastMathMode
   {
      None = 0x0000,
      NotNaN = 0x0001,
      NotInf = 0x0002,
      NSZ = 0x0004,
      AllowRecip = 0x0008,
      Fast = 0x0010,
      AllowContractFastINTEL = 0x10000,
      AllowReassocINTEL = 0x20000,
   }
}

