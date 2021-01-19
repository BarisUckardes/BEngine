using BEngine.Engine.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veldrid;

namespace BEngine.Engine.FelinRenderer
{
    internal class FelineRenderer : RenderingModule
    {
        
        /// <summary>
        /// Renderer variables
        /// </summary>
        private GraphicsDevice targetDevice;
        private ResourceFactory targetResourceFactory;
        private CommandList targetCommandList;
       
        public override string ModuleName => "Feline Renderer v0.0.0";

        /// <summary>
        /// Inits FelineRenderer
        /// </summary>
        /// <param name="targetDevice">target DX11 device</param>
        public override void InitRenderingModule(GraphicsDevice targetDevice)
        {
            this.targetDevice = targetDevice;
            this.targetResourceFactory = this.targetDevice.ResourceFactory;
            this.targetCommandList = this.targetResourceFactory.CreateCommandList();
        }

        /// <summary>
        /// FelineRenderer loop
        /// </summary>
        public override void Run()
        {
            targetCommandList.Begin();
            targetCommandList.SetFramebuffer(targetDevice.SwapchainFramebuffer);
            targetCommandList.ClearColorTarget(0, RgbaFloat.Blue);
            targetCommandList.End();
            targetDevice.SubmitCommands(targetCommandList);
            targetDevice.SwapBuffers();
        }
    }
}
