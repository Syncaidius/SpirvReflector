namespace SpirvReflector
{
   public enum SpirvRayFlags
   {
      NoneKHR = 0x0000,

      OpaqueKHR = 0x0001,

      NoOpaqueKHR = 0x0002,

      TerminateOnFirstHitKHR = 0x0004,

      SkipClosestHitShaderKHR = 0x0008,

      CullBackFacingTrianglesKHR = 0x0010,

      CullFrontFacingTrianglesKHR = 0x0020,

      CullOpaqueKHR = 0x0040,

      CullNoOpaqueKHR = 0x0080,

      SkipTrianglesKHR = 0x0100,

      SkipAABBsKHR = 0x0200,

      ForceOpacityMicromap2StateEXT = 0x0400,
   }
}

