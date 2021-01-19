using System;
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

        public abstract void Run();
      
    }
}
