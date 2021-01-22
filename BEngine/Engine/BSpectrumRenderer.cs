using BEngine.Engine.Graphics;
using BEngine.Engine.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veldrid;
namespace BEngine.Engine
{
    public class BSpectrumRenderer : BComponent
    {
        internal static RenderingModule TargetRenderingModule { get; set; }
        internal Pipeline targetPipeline;
        internal BUniformBuffer targetMVPBuffer;
      

        public BMesh targetMesh;
        public BMaterial targetMaterial;

        public BSpectrumRenderer()
        {
            TargetRenderingModule.RegisterSpectrumRenderer(this);
        }
        public void ApplyRenderer()
        {
            TargetRenderingModule.CreateRenderingPipeline(this);
        }
    }
}
