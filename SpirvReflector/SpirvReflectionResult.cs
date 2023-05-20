using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpirvReflector
{
    public class SpirvReflectionResult
    {
        List<SpirvCapability> _capabilities;
        List<SpirvSource> _sources;
        List<SpirvEntryPoint> _entryPoints;
        List<string> _extensions;
        List<SpirvVariable> _uniforms;

        uint _bound;

        internal SpirvReflectionResult()
        {
            _capabilities = new List<SpirvCapability>();
            _sources = new List<SpirvSource>();
            _extensions = new List<string>();
            _entryPoints = new List<SpirvEntryPoint>();
            _uniforms = new List<SpirvVariable>();
        }

        internal void AddCapaibility(SpirvCapability cap)
        {
            _capabilities.Add(cap);
        }

        internal void AddExtension(string ext)
        {
            _extensions.Add(ext);
        }

        internal void AddSource(SpirvSource source)
        {
            _sources.Add(source);
        }

        internal void AddUniform(SpirvVariable uniform)
        {
            _uniforms.Add(uniform);
        }

        internal void AddEntryPoint(SpirvEntryPoint entryPoint)
        {
            _entryPoints.Add(entryPoint);
        }

        /// <summary>
        /// Gets the SPIR-V version used by the reflected bytecode.
        /// </summary>
        public SpirvVersion Version { get; internal set; }

        /// <summary>
        /// The original generator's magic number.
        /// </summary>
        public uint Generator { get; internal set; }

        /// <summary>
        /// Gets the Bound value; where all [id]s in this module are guaranteed to satisfy.
        /// <para>Bound should be small, smaller is better, with all [id] in a module being densely packed and near 0.</para>
        /// </summary>
        public uint Bound { get; internal set; }

        /// <summary>
        /// Gets the instruction schema number. This is still reserved if unused.
        /// </summary>
        public uint InstructionSchema { get; internal set; }

        /// <summary>
        /// Gets the memory addressing model used by the bytecode.
        /// </summary>
        public SpirvAddressingModel AddressingModel { get; internal set; }

        /// <summary>
        /// Gets the memory model used by the bytecode.
        /// </summary>
        public SpirvMemoryModel MemoryModel { get; internal set; }

        /// <summary>
        /// Gets a read-only list of capabilities required to execute the bytecode.
        /// </summary>
        public IReadOnlyList<SpirvCapability> Capabilities => _capabilities;

        /// <summary>
        /// Gets a read-only list of extensions required to execute the bytecode.
        /// </summary>
        public IReadOnlyList<string> Extensions => _extensions;

        /// <summary>
        /// Gets a list of sources that the bytecode was originally translated from.
        /// </summary>
        public IReadOnlyList<SpirvSource> Sources => _sources;

        /// <summary>
        /// Gets a list of entry points that were found in the bytecode.
        /// </summary>
        public IReadOnlyList<SpirvEntryPoint> EntryPoints => _entryPoints;

        /// <summary>
        /// Gets a list of uniform variables that were found in the bytecode.
        /// </summary>
        public IReadOnlyList<SpirvVariable> Uniforms => _uniforms;

        /// <summary>
        /// Gets the total number of instructions in the bytecode.
        /// </summary>
        public int InstructionCount { get; internal set; }
    }
}
