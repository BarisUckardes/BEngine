using BEngine.Engine.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veldrid;
using Veldrid.Sdl2;
using Veldrid.StartupUtilities;
namespace BEngine.Core.Presentation
{
    public sealed class BWindow
    {
        private Sdl2Window targetWindow;
        private GraphicsDevice targetDevice;
        public BWindow(BVector2 windowSize,string windowName = "Default Window Name")
        {
            /*
             * Build window properties
             */
            WindowCreateInfo windowInfo = new WindowCreateInfo()
            {
                X = 100,
                Y = 100,
                WindowWidth = (int)windowSize.x,
                WindowHeight = (int)windowSize.y,
                WindowTitle = windowName
            };

            /*
             * Create sdl window and graphics device
             */
            targetWindow = VeldridStartup.CreateWindow(windowInfo);
            targetDevice = VeldridStartup.CreateGraphicsDevice(targetWindow);
            
        }
        public Sdl2Window GetWindow()
        {
            return targetWindow;
        }
        public GraphicsDevice GetDevice()
        {
            return targetDevice;
        }
    }
}
