using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veldrid;

namespace BEngine.Engine.Graphics
{
    public class BGraphicsResource : BObject
    {
        public ResourceLayout targetLayout;
        public ResourceSet targetResourceSet;


        public void Dispose()
        {
            targetLayout?.Dispose();
            targetResourceSet?.Dispose();
        }
    }
}
