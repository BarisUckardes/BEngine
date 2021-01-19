using BEngine.Core.Presentation;
using BEngine.Engine.Mathematics;
using BEngine.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veldrid.Sdl2;
using BEngine.Engine.FelinRenderer;
using BEngine.Core.Presentation.FrameWindow;
using BEngine.Engine.Graphics;

namespace BEngine
{
    class program
    {
        static int Main()
        {
            
            BWindow bWindow = new BWindow(new BVector2(512,512));

            Sdl2Window window = bWindow.GetWindow();

            BEngineMonitor engineMonitor = new BEngineMonitor();

            engineMonitor.RegisterModule<FrameWindow>();
            engineMonitor.RegisterModule<FelineRenderer>();

            RenderingModule targetRenderer = engineMonitor.GetEngineModule<RenderingModule>();
            WindowModule targetWindowModule = engineMonitor.GetEngineModule<WindowModule>();

            targetRenderer.InitRenderingModule(bWindow.GetDevice());
            targetWindowModule.InitWindowModule(window);
           

            while(targetWindowModule.IsWindowActive)
            {
                targetWindowModule.Run();
                targetRenderer.Run();
            }
          

            return -1;
        }
    }
}
