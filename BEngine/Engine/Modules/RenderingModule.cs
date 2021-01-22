using BEngine.Engine.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veldrid;
using Veldrid.ImageSharp;

namespace BEngine.Engine.Modules
{
    public abstract class RenderingModule : IEngineModule
    {
        public abstract string ModuleName{get;}

        public abstract void InitRenderingModule(GraphicsDevice targetDevice);
        public abstract void RenderPragma();
        public abstract void RegisterSpectrumRenderer(BSpectrumRenderer targetRenderer);
        public abstract void RegisterSpectrumObserver(BSpectrumObserver targetObserver);
        public abstract void CreateRenderingMesh(BMesh targetMesh);
        public abstract void CreateRenderingMaterial(BMaterial targetMaterial);
        public abstract void CreateRenderingPipeline(BSpectrumRenderer targetObserver);

        public abstract BTexture2D CreateTexture2D(ImageSharpTexture isTexture);

      
    }
}
