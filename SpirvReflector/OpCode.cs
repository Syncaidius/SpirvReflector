namespace SpirvReflector
{
  public enum SpirvOpCode
   {
      OpNop = 0,

      OpUndef = 1,

      OpSourceContinued = 2,

      OpSource = 3,

      OpSourceExtension = 4,

      OpName = 5,

      OpMemberName = 6,

      OpString = 7,

      OpLine = 8,

      OpExtension = 10,

      OpExtInstImport = 11,

      OpExtInst = 12,

      OpMemoryModel = 14,

      OpEntryPoint = 15,

      OpExecutionMode = 16,

      OpCapability = 17,

      OpTypeVoid = 19,

      OpTypeBool = 20,

      OpTypeInt = 21,

      OpTypeFloat = 22,

      OpTypeVector = 23,

      OpTypeMatrix = 24,

      OpTypeImage = 25,

      OpTypeSampler = 26,

      OpTypeSampledImage = 27,

      OpTypeArray = 28,

      OpTypeRuntimeArray = 29,

      OpTypeStruct = 30,

      OpTypeOpaque = 31,

      OpTypePointer = 32,

      OpTypeFunction = 33,

      OpTypeEvent = 34,

      OpTypeDeviceEvent = 35,

      OpTypeReserveId = 36,

      OpTypeQueue = 37,

      OpTypePipe = 38,

      OpTypeForwardPointer = 39,

      OpConstantTrue = 41,

      OpConstantFalse = 42,

      OpConstant = 43,

      OpConstantComposite = 44,

      OpConstantSampler = 45,

      OpConstantNull = 46,

      OpSpecConstantTrue = 48,

      OpSpecConstantFalse = 49,

      OpSpecConstant = 50,

      OpSpecConstantComposite = 51,

      OpSpecConstantOp = 52,

      OpFunction = 54,

      OpFunctionParameter = 55,

      OpFunctionEnd = 56,

      OpFunctionCall = 57,

      OpVariable = 59,

      OpImageTexelPointer = 60,

      OpLoad = 61,

      OpStore = 62,

      OpCopyMemory = 63,

      OpCopyMemorySized = 64,

      OpAccessChain = 65,

      OpInBoundsAccessChain = 66,

      OpPtrAccessChain = 67,

      OpArrayLength = 68,

      OpGenericPtrMemSemantics = 69,

      OpInBoundsPtrAccessChain = 70,

      OpDecorate = 71,

      OpMemberDecorate = 72,

      OpDecorationGroup = 73,

      OpGroupDecorate = 74,

      OpGroupMemberDecorate = 75,

      OpVectorExtractDynamic = 77,

      OpVectorInsertDynamic = 78,

      OpVectorShuffle = 79,

      OpCompositeConstruct = 80,

      OpCompositeExtract = 81,

      OpCompositeInsert = 82,

      OpCopyObject = 83,

      OpTranspose = 84,

      OpSampledImage = 86,

      OpImageSampleImplicitLod = 87,

      OpImageSampleExplicitLod = 88,

      OpImageSampleDrefImplicitLod = 89,

      OpImageSampleDrefExplicitLod = 90,

      OpImageSampleProjImplicitLod = 91,

      OpImageSampleProjExplicitLod = 92,

      OpImageSampleProjDrefImplicitLod = 93,

      OpImageSampleProjDrefExplicitLod = 94,

      OpImageFetch = 95,

      OpImageGather = 96,

      OpImageDrefGather = 97,

      OpImageRead = 98,

      OpImageWrite = 99,

      OpImage = 100,

      OpImageQueryFormat = 101,

      OpImageQueryOrder = 102,

      OpImageQuerySizeLod = 103,

      OpImageQuerySize = 104,

      OpImageQueryLod = 105,

      OpImageQueryLevels = 106,

      OpImageQuerySamples = 107,

      OpConvertFToU = 109,

      OpConvertFToS = 110,

      OpConvertSToF = 111,

      OpConvertUToF = 112,

      OpUConvert = 113,

      OpSConvert = 114,

      OpFConvert = 115,

      OpQuantizeToF16 = 116,

      OpConvertPtrToU = 117,

      OpSatConvertSToU = 118,

      OpSatConvertUToS = 119,

      OpConvertUToPtr = 120,

      OpPtrCastToGeneric = 121,

      OpGenericCastToPtr = 122,

      OpGenericCastToPtrExplicit = 123,

      OpBitcast = 124,

      OpSNegate = 126,

      OpFNegate = 127,

      OpIAdd = 128,

      OpFAdd = 129,

      OpISub = 130,

      OpFSub = 131,

      OpIMul = 132,

      OpFMul = 133,

      OpUDiv = 134,

      OpSDiv = 135,

      OpFDiv = 136,

      OpUMod = 137,

      OpSRem = 138,

      OpSMod = 139,

      OpFRem = 140,

      OpFMod = 141,

      OpVectorTimesScalar = 142,

      OpMatrixTimesScalar = 143,

      OpVectorTimesMatrix = 144,

      OpMatrixTimesVector = 145,

      OpMatrixTimesMatrix = 146,

      OpOuterProduct = 147,

      OpDot = 148,

      OpIAddCarry = 149,

      OpISubBorrow = 150,

      OpUMulExtended = 151,

      OpSMulExtended = 152,

      OpAny = 154,

      OpAll = 155,

      OpIsNan = 156,

      OpIsInf = 157,

      OpIsFinite = 158,

      OpIsNormal = 159,

      OpSignBitSet = 160,

      OpLessOrGreater = 161,

      OpOrdered = 162,

      OpUnordered = 163,

      OpLogicalEqual = 164,

      OpLogicalNotEqual = 165,

      OpLogicalOr = 166,

      OpLogicalAnd = 167,

      OpLogicalNot = 168,

      OpSelect = 169,

      OpIEqual = 170,

      OpINotEqual = 171,

      OpUGreaterThan = 172,

      OpSGreaterThan = 173,

      OpUGreaterThanEqual = 174,

      OpSGreaterThanEqual = 175,

      OpULessThan = 176,

      OpSLessThan = 177,

      OpULessThanEqual = 178,

      OpSLessThanEqual = 179,

      OpFOrdEqual = 180,

      OpFUnordEqual = 181,

      OpFOrdNotEqual = 182,

      OpFUnordNotEqual = 183,

      OpFOrdLessThan = 184,

      OpFUnordLessThan = 185,

      OpFOrdGreaterThan = 186,

      OpFUnordGreaterThan = 187,

      OpFOrdLessThanEqual = 188,

      OpFUnordLessThanEqual = 189,

      OpFOrdGreaterThanEqual = 190,

      OpFUnordGreaterThanEqual = 191,

      OpShiftRightLogical = 194,

      OpShiftRightArithmetic = 195,

      OpShiftLeftLogical = 196,

      OpBitwiseOr = 197,

      OpBitwiseXor = 198,

      OpBitwiseAnd = 199,

      OpNot = 200,

      OpBitFieldInsert = 201,

      OpBitFieldSExtract = 202,

      OpBitFieldUExtract = 203,

      OpBitReverse = 204,

      OpBitCount = 205,

      OpDPdx = 207,

      OpDPdy = 208,

      OpFwidth = 209,

      OpDPdxFine = 210,

      OpDPdyFine = 211,

      OpFwidthFine = 212,

      OpDPdxCoarse = 213,

      OpDPdyCoarse = 214,

      OpFwidthCoarse = 215,

      OpEmitVertex = 218,

      OpEndPrimitive = 219,

      OpEmitStreamVertex = 220,

      OpEndStreamPrimitive = 221,

      OpControlBarrier = 224,

      OpMemoryBarrier = 225,

      OpAtomicLoad = 227,

      OpAtomicStore = 228,

      OpAtomicExchange = 229,

      OpAtomicCompareExchange = 230,

      OpAtomicCompareExchangeWeak = 231,

      OpAtomicIIncrement = 232,

      OpAtomicIDecrement = 233,

      OpAtomicIAdd = 234,

      OpAtomicISub = 235,

      OpAtomicSMin = 236,

      OpAtomicUMin = 237,

      OpAtomicSMax = 238,

      OpAtomicUMax = 239,

      OpAtomicAnd = 240,

      OpAtomicOr = 241,

      OpAtomicXor = 242,

      OpPhi = 245,

      OpLoopMerge = 246,

      OpSelectionMerge = 247,

      OpLabel = 248,

      OpBranch = 249,

      OpBranchConditional = 250,

      OpSwitch = 251,

      OpKill = 252,

      OpReturn = 253,

      OpReturnValue = 254,

      OpUnreachable = 255,

      OpLifetimeStart = 256,

      OpLifetimeStop = 257,

      OpGroupAsyncCopy = 259,

      OpGroupWaitEvents = 260,

      OpGroupAll = 261,

      OpGroupAny = 262,

      OpGroupBroadcast = 263,

      OpGroupIAdd = 264,

      OpGroupFAdd = 265,

      OpGroupFMin = 266,

      OpGroupUMin = 267,

      OpGroupSMin = 268,

      OpGroupFMax = 269,

      OpGroupUMax = 270,

      OpGroupSMax = 271,

      OpReadPipe = 274,

      OpWritePipe = 275,

      OpReservedReadPipe = 276,

      OpReservedWritePipe = 277,

      OpReserveReadPipePackets = 278,

      OpReserveWritePipePackets = 279,

      OpCommitReadPipe = 280,

      OpCommitWritePipe = 281,

      OpIsValidReserveId = 282,

      OpGetNumPipePackets = 283,

      OpGetMaxPipePackets = 284,

      OpGroupReserveReadPipePackets = 285,

      OpGroupReserveWritePipePackets = 286,

      OpGroupCommitReadPipe = 287,

      OpGroupCommitWritePipe = 288,

      OpEnqueueMarker = 291,

      OpEnqueueKernel = 292,

      OpGetKernelNDrangeSubGroupCount = 293,

      OpGetKernelNDrangeMaxSubGroupSize = 294,

      OpGetKernelWorkGroupSize = 295,

      OpGetKernelPreferredWorkGroupSizeMultiple = 296,

      OpRetainEvent = 297,

      OpReleaseEvent = 298,

      OpCreateUserEvent = 299,

      OpIsValidEvent = 300,

      OpSetUserEventStatus = 301,

      OpCaptureEventProfilingInfo = 302,

      OpGetDefaultQueue = 303,

      OpBuildNDRange = 304,

      OpImageSparseSampleImplicitLod = 305,

      OpImageSparseSampleExplicitLod = 306,

      OpImageSparseSampleDrefImplicitLod = 307,

      OpImageSparseSampleDrefExplicitLod = 308,

      OpImageSparseSampleProjImplicitLod = 309,

      OpImageSparseSampleProjExplicitLod = 310,

      OpImageSparseSampleProjDrefImplicitLod = 311,

      OpImageSparseSampleProjDrefExplicitLod = 312,

      OpImageSparseFetch = 313,

      OpImageSparseGather = 314,

      OpImageSparseDrefGather = 315,

      OpImageSparseTexelsResident = 316,

      OpNoLine = 317,

      OpAtomicFlagTestAndSet = 318,

      OpAtomicFlagClear = 319,

      OpImageSparseRead = 320,

      OpSizeOf = 321,

      OpTypePipeStorage = 322,

      OpConstantPipeStorage = 323,

      OpCreatePipeFromPipeStorage = 324,

      OpGetKernelLocalSizeForSubgroupCount = 325,

      OpGetKernelMaxNumSubgroups = 326,

      OpTypeNamedBarrier = 327,

      OpNamedBarrierInitialize = 328,

      OpMemoryNamedBarrier = 329,

      OpModuleProcessed = 330,

      OpExecutionModeId = 331,

      OpDecorateId = 332,

      OpGroupNonUniformElect = 333,

      OpGroupNonUniformAll = 334,

      OpGroupNonUniformAny = 335,

      OpGroupNonUniformAllEqual = 336,

      OpGroupNonUniformBroadcast = 337,

      OpGroupNonUniformBroadcastFirst = 338,

      OpGroupNonUniformBallot = 339,

      OpGroupNonUniformInverseBallot = 340,

      OpGroupNonUniformBallotBitExtract = 341,

      OpGroupNonUniformBallotBitCount = 342,

      OpGroupNonUniformBallotFindLSB = 343,

      OpGroupNonUniformBallotFindMSB = 344,

      OpGroupNonUniformShuffle = 345,

      OpGroupNonUniformShuffleXor = 346,

      OpGroupNonUniformShuffleUp = 347,

      OpGroupNonUniformShuffleDown = 348,

      OpGroupNonUniformIAdd = 349,

      OpGroupNonUniformFAdd = 350,

      OpGroupNonUniformIMul = 351,

      OpGroupNonUniformFMul = 352,

      OpGroupNonUniformSMin = 353,

      OpGroupNonUniformUMin = 354,

      OpGroupNonUniformFMin = 355,

      OpGroupNonUniformSMax = 356,

      OpGroupNonUniformUMax = 357,

      OpGroupNonUniformFMax = 358,

      OpGroupNonUniformBitwiseAnd = 359,

      OpGroupNonUniformBitwiseOr = 360,

      OpGroupNonUniformBitwiseXor = 361,

      OpGroupNonUniformLogicalAnd = 362,

      OpGroupNonUniformLogicalOr = 363,

      OpGroupNonUniformLogicalXor = 364,

      OpGroupNonUniformQuadBroadcast = 365,

      OpGroupNonUniformQuadSwap = 366,

      OpCopyLogical = 400,

      OpPtrEqual = 401,

      OpPtrNotEqual = 402,

      OpPtrDiff = 403,

      OpColorAttachmentReadEXT = 4160,

      OpDepthAttachmentReadEXT = 4161,

      OpStencilAttachmentReadEXT = 4162,

      OpTerminateInvocation = 4416,

      OpSubgroupBallotKHR = 4421,

      OpSubgroupFirstInvocationKHR = 4422,

      OpSubgroupAllKHR = 4428,

      OpSubgroupAnyKHR = 4429,

      OpSubgroupAllEqualKHR = 4430,

      OpGroupNonUniformRotateKHR = 4431,

      OpSubgroupReadInvocationKHR = 4432,

      OpTraceRayKHR = 4445,

      OpExecuteCallableKHR = 4446,

      OpConvertUToAccelerationStructureKHR = 4447,

      OpIgnoreIntersectionKHR = 4448,

      OpTerminateRayKHR = 4449,

      OpSDot = 4450,

      OpSDotKHR = 4450,

      OpUDot = 4451,

      OpUDotKHR = 4451,

      OpSUDot = 4452,

      OpSUDotKHR = 4452,

      OpSDotAccSat = 4453,

      OpSDotAccSatKHR = 4453,

      OpUDotAccSat = 4454,

      OpUDotAccSatKHR = 4454,

      OpSUDotAccSat = 4455,

      OpSUDotAccSatKHR = 4455,

      OpTypeRayQueryKHR = 4472,

      OpRayQueryInitializeKHR = 4473,

      OpRayQueryTerminateKHR = 4474,

      OpRayQueryGenerateIntersectionKHR = 4475,

      OpRayQueryConfirmIntersectionKHR = 4476,

      OpRayQueryProceedKHR = 4477,

      OpRayQueryGetIntersectionTypeKHR = 4479,

      OpImageSampleWeightedQCOM = 4480,

      OpImageBoxFilterQCOM = 4481,

      OpImageBlockMatchSSDQCOM = 4482,

      OpImageBlockMatchSADQCOM = 4483,

      OpGroupIAddNonUniformAMD = 5000,

      OpGroupFAddNonUniformAMD = 5001,

      OpGroupFMinNonUniformAMD = 5002,

      OpGroupUMinNonUniformAMD = 5003,

      OpGroupSMinNonUniformAMD = 5004,

      OpGroupFMaxNonUniformAMD = 5005,

      OpGroupUMaxNonUniformAMD = 5006,

      OpGroupSMaxNonUniformAMD = 5007,

      OpFragmentMaskFetchAMD = 5011,

      OpFragmentFetchAMD = 5012,

      OpReadClockKHR = 5056,

      OpHitObjectRecordHitMotionNV = 5249,

      OpHitObjectRecordHitWithIndexMotionNV = 5250,

      OpHitObjectRecordMissMotionNV = 5251,

      OpHitObjectGetWorldToObjectNV = 5252,

      OpHitObjectGetObjectToWorldNV = 5253,

      OpHitObjectGetObjectRayDirectionNV = 5254,

      OpHitObjectGetObjectRayOriginNV = 5255,

      OpHitObjectTraceRayMotionNV = 5256,

      OpHitObjectGetShaderRecordBufferHandleNV = 5257,

      OpHitObjectGetShaderBindingTableRecordIndexNV = 5258,

      OpHitObjectRecordEmptyNV = 5259,

      OpHitObjectTraceRayNV = 5260,

      OpHitObjectRecordHitNV = 5261,

      OpHitObjectRecordHitWithIndexNV = 5262,

      OpHitObjectRecordMissNV = 5263,

      OpHitObjectExecuteShaderNV = 5264,

      OpHitObjectGetCurrentTimeNV = 5265,

      OpHitObjectGetAttributesNV = 5266,

      OpHitObjectGetHitKindNV = 5267,

      OpHitObjectGetPrimitiveIndexNV = 5268,

      OpHitObjectGetGeometryIndexNV = 5269,

      OpHitObjectGetInstanceIdNV = 5270,

      OpHitObjectGetInstanceCustomIndexNV = 5271,

      OpHitObjectGetWorldRayDirectionNV = 5272,

      OpHitObjectGetWorldRayOriginNV = 5273,

      OpHitObjectGetRayTMaxNV = 5274,

      OpHitObjectGetRayTMinNV = 5275,

      OpHitObjectIsEmptyNV = 5276,

      OpHitObjectIsHitNV = 5277,

      OpHitObjectIsMissNV = 5278,

      OpReorderThreadWithHitObjectNV = 5279,

      OpReorderThreadWithHintNV = 5280,

      OpTypeHitObjectNV = 5281,

      OpImageSampleFootprintNV = 5283,

      OpEmitMeshTasksEXT = 5294,

      OpSetMeshOutputsEXT = 5295,

      OpGroupNonUniformPartitionNV = 5296,

      OpWritePackedPrimitiveIndices4x8NV = 5299,

      OpReportIntersectionNV = 5334,

      OpReportIntersectionKHR = 5334,

      OpIgnoreIntersectionNV = 5335,

      OpTerminateRayNV = 5336,

      OpTraceNV = 5337,

      OpTraceMotionNV = 5338,

      OpTraceRayMotionNV = 5339,

      OpRayQueryGetIntersectionTriangleVertexPositionsKHR = 5340,

      OpTypeAccelerationStructureNV = 5341,

      OpTypeAccelerationStructureKHR = 5341,

      OpExecuteCallableNV = 5344,

      OpTypeCooperativeMatrixNV = 5358,

      OpCooperativeMatrixLoadNV = 5359,

      OpCooperativeMatrixStoreNV = 5360,

      OpCooperativeMatrixMulAddNV = 5361,

      OpCooperativeMatrixLengthNV = 5362,

      OpBeginInvocationInterlockEXT = 5364,

      OpEndInvocationInterlockEXT = 5365,

      OpDemoteToHelperInvocation = 5380,

      OpDemoteToHelperInvocationEXT = 5380,

      OpIsHelperInvocationEXT = 5381,

      OpConvertUToImageNV = 5391,

      OpConvertUToSamplerNV = 5392,

      OpConvertImageToUNV = 5393,

      OpConvertSamplerToUNV = 5394,

      OpConvertUToSampledImageNV = 5395,

      OpConvertSampledImageToUNV = 5396,

      OpSamplerImageAddressingModeNV = 5397,

      OpSubgroupShuffleINTEL = 5571,

      OpSubgroupShuffleDownINTEL = 5572,

      OpSubgroupShuffleUpINTEL = 5573,

      OpSubgroupShuffleXorINTEL = 5574,

      OpSubgroupBlockReadINTEL = 5575,

      OpSubgroupBlockWriteINTEL = 5576,

      OpSubgroupImageBlockReadINTEL = 5577,

      OpSubgroupImageBlockWriteINTEL = 5578,

      OpSubgroupImageMediaBlockReadINTEL = 5580,

      OpSubgroupImageMediaBlockWriteINTEL = 5581,

      OpUCountLeadingZerosINTEL = 5585,

      OpUCountTrailingZerosINTEL = 5586,

      OpAbsISubINTEL = 5587,

      OpAbsUSubINTEL = 5588,

      OpIAddSatINTEL = 5589,

      OpUAddSatINTEL = 5590,

      OpIAverageINTEL = 5591,

      OpUAverageINTEL = 5592,

      OpIAverageRoundedINTEL = 5593,

      OpUAverageRoundedINTEL = 5594,

      OpISubSatINTEL = 5595,

      OpUSubSatINTEL = 5596,

      OpIMul32x16INTEL = 5597,

      OpUMul32x16INTEL = 5598,

      OpConstantFunctionPointerINTEL = 5600,

      OpFunctionPointerCallINTEL = 5601,

      OpAsmTargetINTEL = 5609,

      OpAsmINTEL = 5610,

      OpAsmCallINTEL = 5611,

      OpAtomicFMinEXT = 5614,

      OpAtomicFMaxEXT = 5615,

      OpAssumeTrueKHR = 5630,

      OpExpectKHR = 5631,

      OpDecorateString = 5632,

      OpDecorateStringGOOGLE = 5632,

      OpMemberDecorateString = 5633,

      OpMemberDecorateStringGOOGLE = 5633,

      OpVmeImageINTEL = 5699,

      OpTypeVmeImageINTEL = 5700,

      OpTypeAvcImePayloadINTEL = 5701,

      OpTypeAvcRefPayloadINTEL = 5702,

      OpTypeAvcSicPayloadINTEL = 5703,

      OpTypeAvcMcePayloadINTEL = 5704,

      OpTypeAvcMceResultINTEL = 5705,

      OpTypeAvcImeResultINTEL = 5706,

      OpTypeAvcImeResultSingleReferenceStreamoutINTEL = 5707,

      OpTypeAvcImeResultDualReferenceStreamoutINTEL = 5708,

      OpTypeAvcImeSingleReferenceStreaminINTEL = 5709,

      OpTypeAvcImeDualReferenceStreaminINTEL = 5710,

      OpTypeAvcRefResultINTEL = 5711,

      OpTypeAvcSicResultINTEL = 5712,

      OpSubgroupAvcMceGetDefaultInterBaseMultiReferencePenaltyINTEL = 5713,

      OpSubgroupAvcMceSetInterBaseMultiReferencePenaltyINTEL = 5714,

      OpSubgroupAvcMceGetDefaultInterShapePenaltyINTEL = 5715,

      OpSubgroupAvcMceSetInterShapePenaltyINTEL = 5716,

      OpSubgroupAvcMceGetDefaultInterDirectionPenaltyINTEL = 5717,

      OpSubgroupAvcMceSetInterDirectionPenaltyINTEL = 5718,

      OpSubgroupAvcMceGetDefaultIntraLumaShapePenaltyINTEL = 5719,

      OpSubgroupAvcMceGetDefaultInterMotionVectorCostTableINTEL = 5720,

      OpSubgroupAvcMceGetDefaultHighPenaltyCostTableINTEL = 5721,

      OpSubgroupAvcMceGetDefaultMediumPenaltyCostTableINTEL = 5722,

      OpSubgroupAvcMceGetDefaultLowPenaltyCostTableINTEL = 5723,

      OpSubgroupAvcMceSetMotionVectorCostFunctionINTEL = 5724,

      OpSubgroupAvcMceGetDefaultIntraLumaModePenaltyINTEL = 5725,

      OpSubgroupAvcMceGetDefaultNonDcLumaIntraPenaltyINTEL = 5726,

      OpSubgroupAvcMceGetDefaultIntraChromaModeBasePenaltyINTEL = 5727,

      OpSubgroupAvcMceSetAcOnlyHaarINTEL = 5728,

      OpSubgroupAvcMceSetSourceInterlacedFieldPolarityINTEL = 5729,

      OpSubgroupAvcMceSetSingleReferenceInterlacedFieldPolarityINTEL = 5730,

      OpSubgroupAvcMceSetDualReferenceInterlacedFieldPolaritiesINTEL = 5731,

      OpSubgroupAvcMceConvertToImePayloadINTEL = 5732,

      OpSubgroupAvcMceConvertToImeResultINTEL = 5733,

      OpSubgroupAvcMceConvertToRefPayloadINTEL = 5734,

      OpSubgroupAvcMceConvertToRefResultINTEL = 5735,

      OpSubgroupAvcMceConvertToSicPayloadINTEL = 5736,

      OpSubgroupAvcMceConvertToSicResultINTEL = 5737,

      OpSubgroupAvcMceGetMotionVectorsINTEL = 5738,

      OpSubgroupAvcMceGetInterDistortionsINTEL = 5739,

      OpSubgroupAvcMceGetBestInterDistortionsINTEL = 5740,

      OpSubgroupAvcMceGetInterMajorShapeINTEL = 5741,

      OpSubgroupAvcMceGetInterMinorShapeINTEL = 5742,

      OpSubgroupAvcMceGetInterDirectionsINTEL = 5743,

      OpSubgroupAvcMceGetInterMotionVectorCountINTEL = 5744,

      OpSubgroupAvcMceGetInterReferenceIdsINTEL = 5745,

      OpSubgroupAvcMceGetInterReferenceInterlacedFieldPolaritiesINTEL = 5746,

      OpSubgroupAvcImeInitializeINTEL = 5747,

      OpSubgroupAvcImeSetSingleReferenceINTEL = 5748,

      OpSubgroupAvcImeSetDualReferenceINTEL = 5749,

      OpSubgroupAvcImeRefWindowSizeINTEL = 5750,

      OpSubgroupAvcImeAdjustRefOffsetINTEL = 5751,

      OpSubgroupAvcImeConvertToMcePayloadINTEL = 5752,

      OpSubgroupAvcImeSetMaxMotionVectorCountINTEL = 5753,

      OpSubgroupAvcImeSetUnidirectionalMixDisableINTEL = 5754,

      OpSubgroupAvcImeSetEarlySearchTerminationThresholdINTEL = 5755,

      OpSubgroupAvcImeSetWeightedSadINTEL = 5756,

      OpSubgroupAvcImeEvaluateWithSingleReferenceINTEL = 5757,

      OpSubgroupAvcImeEvaluateWithDualReferenceINTEL = 5758,

      OpSubgroupAvcImeEvaluateWithSingleReferenceStreaminINTEL = 5759,

      OpSubgroupAvcImeEvaluateWithDualReferenceStreaminINTEL = 5760,

      OpSubgroupAvcImeEvaluateWithSingleReferenceStreamoutINTEL = 5761,

      OpSubgroupAvcImeEvaluateWithDualReferenceStreamoutINTEL = 5762,

      OpSubgroupAvcImeEvaluateWithSingleReferenceStreaminoutINTEL = 5763,

      OpSubgroupAvcImeEvaluateWithDualReferenceStreaminoutINTEL = 5764,

      OpSubgroupAvcImeConvertToMceResultINTEL = 5765,

      OpSubgroupAvcImeGetSingleReferenceStreaminINTEL = 5766,

      OpSubgroupAvcImeGetDualReferenceStreaminINTEL = 5767,

      OpSubgroupAvcImeStripSingleReferenceStreamoutINTEL = 5768,

      OpSubgroupAvcImeStripDualReferenceStreamoutINTEL = 5769,

      OpSubgroupAvcImeGetStreamoutSingleReferenceMajorShapeMotionVectorsINTEL = 5770,

      OpSubgroupAvcImeGetStreamoutSingleReferenceMajorShapeDistortionsINTEL = 5771,

      OpSubgroupAvcImeGetStreamoutSingleReferenceMajorShapeReferenceIdsINTEL = 5772,

      OpSubgroupAvcImeGetStreamoutDualReferenceMajorShapeMotionVectorsINTEL = 5773,

      OpSubgroupAvcImeGetStreamoutDualReferenceMajorShapeDistortionsINTEL = 5774,

      OpSubgroupAvcImeGetStreamoutDualReferenceMajorShapeReferenceIdsINTEL = 5775,

      OpSubgroupAvcImeGetBorderReachedINTEL = 5776,

      OpSubgroupAvcImeGetTruncatedSearchIndicationINTEL = 5777,

      OpSubgroupAvcImeGetUnidirectionalEarlySearchTerminationINTEL = 5778,

      OpSubgroupAvcImeGetWeightingPatternMinimumMotionVectorINTEL = 5779,

      OpSubgroupAvcImeGetWeightingPatternMinimumDistortionINTEL = 5780,

      OpSubgroupAvcFmeInitializeINTEL = 5781,

      OpSubgroupAvcBmeInitializeINTEL = 5782,

      OpSubgroupAvcRefConvertToMcePayloadINTEL = 5783,

      OpSubgroupAvcRefSetBidirectionalMixDisableINTEL = 5784,

      OpSubgroupAvcRefSetBilinearFilterEnableINTEL = 5785,

      OpSubgroupAvcRefEvaluateWithSingleReferenceINTEL = 5786,

      OpSubgroupAvcRefEvaluateWithDualReferenceINTEL = 5787,

      OpSubgroupAvcRefEvaluateWithMultiReferenceINTEL = 5788,

      OpSubgroupAvcRefEvaluateWithMultiReferenceInterlacedINTEL = 5789,

      OpSubgroupAvcRefConvertToMceResultINTEL = 5790,

      OpSubgroupAvcSicInitializeINTEL = 5791,

      OpSubgroupAvcSicConfigureSkcINTEL = 5792,

      OpSubgroupAvcSicConfigureIpeLumaINTEL = 5793,

      OpSubgroupAvcSicConfigureIpeLumaChromaINTEL = 5794,

      OpSubgroupAvcSicGetMotionVectorMaskINTEL = 5795,

      OpSubgroupAvcSicConvertToMcePayloadINTEL = 5796,

      OpSubgroupAvcSicSetIntraLumaShapePenaltyINTEL = 5797,

      OpSubgroupAvcSicSetIntraLumaModeCostFunctionINTEL = 5798,

      OpSubgroupAvcSicSetIntraChromaModeCostFunctionINTEL = 5799,

      OpSubgroupAvcSicSetBilinearFilterEnableINTEL = 5800,

      OpSubgroupAvcSicSetSkcForwardTransformEnableINTEL = 5801,

      OpSubgroupAvcSicSetBlockBasedRawSkipSadINTEL = 5802,

      OpSubgroupAvcSicEvaluateIpeINTEL = 5803,

      OpSubgroupAvcSicEvaluateWithSingleReferenceINTEL = 5804,

      OpSubgroupAvcSicEvaluateWithDualReferenceINTEL = 5805,

      OpSubgroupAvcSicEvaluateWithMultiReferenceINTEL = 5806,

      OpSubgroupAvcSicEvaluateWithMultiReferenceInterlacedINTEL = 5807,

      OpSubgroupAvcSicConvertToMceResultINTEL = 5808,

      OpSubgroupAvcSicGetIpeLumaShapeINTEL = 5809,

      OpSubgroupAvcSicGetBestIpeLumaDistortionINTEL = 5810,

      OpSubgroupAvcSicGetBestIpeChromaDistortionINTEL = 5811,

      OpSubgroupAvcSicGetPackedIpeLumaModesINTEL = 5812,

      OpSubgroupAvcSicGetIpeChromaModeINTEL = 5813,

      OpSubgroupAvcSicGetPackedSkcLumaCountThresholdINTEL = 5814,

      OpSubgroupAvcSicGetPackedSkcLumaSumThresholdINTEL = 5815,

      OpSubgroupAvcSicGetInterRawSadsINTEL = 5816,

      OpVariableLengthArrayINTEL = 5818,

      OpSaveMemoryINTEL = 5819,

      OpRestoreMemoryINTEL = 5820,

      OpArbitraryFloatSinCosPiINTEL = 5840,

      OpArbitraryFloatCastINTEL = 5841,

      OpArbitraryFloatCastFromIntINTEL = 5842,

      OpArbitraryFloatCastToIntINTEL = 5843,

      OpArbitraryFloatAddINTEL = 5846,

      OpArbitraryFloatSubINTEL = 5847,

      OpArbitraryFloatMulINTEL = 5848,

      OpArbitraryFloatDivINTEL = 5849,

      OpArbitraryFloatGTINTEL = 5850,

      OpArbitraryFloatGEINTEL = 5851,

      OpArbitraryFloatLTINTEL = 5852,

      OpArbitraryFloatLEINTEL = 5853,

      OpArbitraryFloatEQINTEL = 5854,

      OpArbitraryFloatRecipINTEL = 5855,

      OpArbitraryFloatRSqrtINTEL = 5856,

      OpArbitraryFloatCbrtINTEL = 5857,

      OpArbitraryFloatHypotINTEL = 5858,

      OpArbitraryFloatSqrtINTEL = 5859,

      OpArbitraryFloatLogINTEL = 5860,

      OpArbitraryFloatLog2INTEL = 5861,

      OpArbitraryFloatLog10INTEL = 5862,

      OpArbitraryFloatLog1pINTEL = 5863,

      OpArbitraryFloatExpINTEL = 5864,

      OpArbitraryFloatExp2INTEL = 5865,

      OpArbitraryFloatExp10INTEL = 5866,

      OpArbitraryFloatExpm1INTEL = 5867,

      OpArbitraryFloatSinINTEL = 5868,

      OpArbitraryFloatCosINTEL = 5869,

      OpArbitraryFloatSinCosINTEL = 5870,

      OpArbitraryFloatSinPiINTEL = 5871,

      OpArbitraryFloatCosPiINTEL = 5872,

      OpArbitraryFloatASinINTEL = 5873,

      OpArbitraryFloatASinPiINTEL = 5874,

      OpArbitraryFloatACosINTEL = 5875,

      OpArbitraryFloatACosPiINTEL = 5876,

      OpArbitraryFloatATanINTEL = 5877,

      OpArbitraryFloatATanPiINTEL = 5878,

      OpArbitraryFloatATan2INTEL = 5879,

      OpArbitraryFloatPowINTEL = 5880,

      OpArbitraryFloatPowRINTEL = 5881,

      OpArbitraryFloatPowNINTEL = 5882,

      OpLoopControlINTEL = 5887,

      OpAliasDomainDeclINTEL = 5911,

      OpAliasScopeDeclINTEL = 5912,

      OpAliasScopeListDeclINTEL = 5913,

      OpFixedSqrtINTEL = 5923,

      OpFixedRecipINTEL = 5924,

      OpFixedRsqrtINTEL = 5925,

      OpFixedSinINTEL = 5926,

      OpFixedCosINTEL = 5927,

      OpFixedSinCosINTEL = 5928,

      OpFixedSinPiINTEL = 5929,

      OpFixedCosPiINTEL = 5930,

      OpFixedSinCosPiINTEL = 5931,

      OpFixedLogINTEL = 5932,

      OpFixedExpINTEL = 5933,

      OpPtrCastToCrossWorkgroupINTEL = 5934,

      OpCrossWorkgroupCastToPtrINTEL = 5938,

      OpReadPipeBlockingINTEL = 5946,

      OpWritePipeBlockingINTEL = 5947,

      OpFPGARegINTEL = 5949,

      OpRayQueryGetRayTMinKHR = 6016,

      OpRayQueryGetRayFlagsKHR = 6017,

      OpRayQueryGetIntersectionTKHR = 6018,

      OpRayQueryGetIntersectionInstanceCustomIndexKHR = 6019,

      OpRayQueryGetIntersectionInstanceIdKHR = 6020,

      OpRayQueryGetIntersectionInstanceShaderBindingTableRecordOffsetKHR = 6021,

      OpRayQueryGetIntersectionGeometryIndexKHR = 6022,

      OpRayQueryGetIntersectionPrimitiveIndexKHR = 6023,

      OpRayQueryGetIntersectionBarycentricsKHR = 6024,

      OpRayQueryGetIntersectionFrontFaceKHR = 6025,

      OpRayQueryGetIntersectionCandidateAABBOpaqueKHR = 6026,

      OpRayQueryGetIntersectionObjectRayDirectionKHR = 6027,

      OpRayQueryGetIntersectionObjectRayOriginKHR = 6028,

      OpRayQueryGetWorldRayDirectionKHR = 6029,

      OpRayQueryGetWorldRayOriginKHR = 6030,

      OpRayQueryGetIntersectionObjectToWorldKHR = 6031,

      OpRayQueryGetIntersectionWorldToObjectKHR = 6032,

      OpAtomicFAddEXT = 6035,

      OpTypeBufferSurfaceINTEL = 6086,

      OpTypeStructContinuedINTEL = 6090,

      OpConstantCompositeContinuedINTEL = 6091,

      OpSpecConstantCompositeContinuedINTEL = 6092,

      OpConvertFToBF16INTEL = 6116,

      OpConvertBF16ToFINTEL = 6117,

      OpControlBarrierArriveINTEL = 6142,

      OpControlBarrierWaitINTEL = 6143,

      OpGroupIMulKHR = 6401,

      OpGroupFMulKHR = 6402,

      OpGroupBitwiseAndKHR = 6403,

      OpGroupBitwiseOrKHR = 6404,

      OpGroupBitwiseXorKHR = 6405,

      OpGroupLogicalAndKHR = 6406,

      OpGroupLogicalOrKHR = 6407,

      OpGroupLogicalXorKHR = 6408,

   }
}

