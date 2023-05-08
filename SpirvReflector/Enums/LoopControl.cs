namespace SpirvReflector
{
  public enum LoopControl
   {
      None = 0x0000,
      Unroll = 0x0001,
      DontUnroll = 0x0002,
      DependencyInfinite = 0x0004,
      DependencyLength = 0x0008,
      MinIterations = 0x0010,
      MaxIterations = 0x0020,
      IterationMultiple = 0x0040,
      PeelCount = 0x0080,
      PartialCount = 0x0100,
      InitiationIntervalINTEL = 0x10000,
      MaxConcurrencyINTEL = 0x20000,
      DependencyArrayINTEL = 0x40000,
      PipelineEnableINTEL = 0x80000,
      LoopCoalesceINTEL = 0x100000,
      MaxInterleavingINTEL = 0x200000,
      SpeculatedIterationsINTEL = 0x400000,
      NoFusionINTEL = 0x800000,
      LoopCountINTEL = 0x1000000,
      MaxReinvocationDelayINTEL = 0x2000000,
   }
}

