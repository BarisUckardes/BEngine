using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veldrid;

namespace BEngine.Engine.Graphics
{
    public class BTexture : BGraphicsResource
    {
        internal Texture targetTexture;
        internal TextureView targetView;
        internal TextureDescription targetDescription;
    }
}
