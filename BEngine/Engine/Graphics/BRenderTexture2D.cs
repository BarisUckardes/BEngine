using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veldrid;

namespace BEngine.Engine.Graphics
{
    public class BRenderTexture2D
    {
        internal Framebuffer targetFrameBuffer;

        internal BTexture2D targetColorTexture;
        internal BTexture2D targetDepthStencilTexture;
    }
}
