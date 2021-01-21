using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BEngine.Engine.Graphics
{
    public struct BMatrix4x4 : IFixedSize
    {
        public Matrix4x4 targetMatrix;

        public BMatrix4x4(Matrix4x4 targetMatrix)
        {
            this.targetMatrix = targetMatrix;
        }

        public uint FixedSize => 64;
    }
}
