using BEngine.Engine.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEngine.Engine.Graphics
{
    public struct BVertex
    {
        public BVector3 Position;
        public BVector2 UV;

        public BVertex(BVector3 position, BVector2 uv)
        {
            Position = position;
            UV = uv;
        }
    }
}
