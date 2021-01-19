using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEngine.Engine.Mathematics
{
    public struct BVector3
    {
        public float x;
        public float y;
        public float z;

        public BVector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }
    }
}
