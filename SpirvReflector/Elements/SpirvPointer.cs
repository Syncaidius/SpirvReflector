﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpirvReflector
{
    public class SpirvPointer : SpirvBytecodeElement
    {
        public override string ToString()
        {
            if(Type.Kind == SpirvTypeKind.Invalid)
                return $"[ID: {ID}] {Type.Kind}* [[{Type.Name}]]";
            else if (Type.Kind == SpirvTypeKind.Struct)
                return $"[ID: {ID}] {(Type.Name ?? Type.Kind.ToString())}*";
            else
                return $"[ID: {ID}] {Type.Kind}*";
        }
        public uint ID { get; internal set; }

        public SpirvType Type { get; internal set; }

        public SpirvStorageClass StorageClass { get; internal set; }
    }
}
