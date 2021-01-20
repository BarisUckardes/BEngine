﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veldrid;
namespace BEngine.Engine.Graphics
{
    public abstract class RenderingModule : IEngineModule
    {
        public abstract string ModuleName{get;}

        public abstract void InitRenderingModule(GraphicsDevice targetDevice);
        public abstract void RegisterSpectrumObserver(BSpectrumRenderer targetRenderer);
        public abstract void Run();

        public abstract void CreateRenderingMesh(BMesh targetMesh);

        public abstract void CreateRenderingMaterial(BMaterial targetMaterial);

        public abstract void CreateRenderingPipeline(BSpectrumRenderer targetObserver);
      
    }
}
