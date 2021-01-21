using BEngine.Engine.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veldrid;

namespace BEngine.Engine
{
    public class BMesh
    {
        /// <summary>
        /// Mesh GPU variables
        /// </summary>
        internal DeviceBuffer targetVertexBuffer;
        internal DeviceBuffer targetIndexBuffer;
        internal VertexLayoutDescription targetVertexLayout;

        /// <summary>
        /// Mesh local variables
        /// </summary>
        public BVertex[] Vertexes;
        public uint[] Indexes;
    }
}
