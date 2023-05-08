namespace Molten.Graphics.Vulkan
{
  public enum MemoryAccess
   {
      None = 0x0000,
      Volatile = 0x0001,
      Aligned = 0x0002,
      Nontemporal = 0x0004,
      MakePointerAvailable = 0x0008,
      MakePointerAvailableKHR = 0x0008,
      MakePointerVisible = 0x0010,
      MakePointerVisibleKHR = 0x0010,
      NonPrivatePointer = 0x0020,
      NonPrivatePointerKHR = 0x0020,
      AliasScopeINTELMask = 0x10000,
      NoAliasINTELMask = 0x20000,
   }
}

