using BEngine.Engine.Graphics;
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
        internal Pipeline targetPipeline;
        internal BUniformBuffer targetMVPBuffer;
      

        public BMesh targetMesh;
        public BMaterial targetMaterial;
    }
}
