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

        private BMesh _targetMesh;
        private BMaterial _targetMaterial;

        public BMesh TargetMesh
        {
            get
            {
                return _targetMesh;
            }
            set
            {
                _targetMesh = value;

                if(_targetMesh !=null && _targetMaterial !=null)
                {
                    TargetRenderingModule.CreateRenderingPipeline(this);
                }
            }
        }
        public BMaterial TargetMaterial
        {
            get
            {
                return _targetMaterial;
            }
            set
            {
                _targetMaterial = value;
                _targetMaterial.RegisterRendererInternal(this);

                if (_targetMesh != null && _targetMaterial != null)
                {
                    TargetRenderingModule.CreateRenderingPipeline(this);
                }
            }
        }


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
