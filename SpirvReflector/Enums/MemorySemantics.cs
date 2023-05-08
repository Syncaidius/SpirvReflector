namespace Molten.Graphics.Vulkan
{
  public enum MemorySemantics
   {
      Relaxed = 0x0000,
      None = 0x0000,
      Acquire = 0x0002,
      Release = 0x0004,
      AcquireRelease = 0x0008,
      SequentiallyConsistent = 0x0010,
      UniformMemory = 0x0040,
      SubgroupMemory = 0x0080,
      WorkgroupMemory = 0x0100,
      CrossWorkgroupMemory = 0x0200,
      AtomicCounterMemory = 0x0400,
      ImageMemory = 0x0800,
      OutputMemory = 0x1000,
      OutputMemoryKHR = 0x1000,
      MakeAvailable = 0x2000,
      MakeAvailableKHR = 0x2000,
      MakeVisible = 0x4000,
      MakeVisibleKHR = 0x4000,
      Volatile = 0x8000,
   }
}

