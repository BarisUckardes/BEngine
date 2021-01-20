using BEngine.Core.Presentation;
using BEngine.Core.IO;
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
using BEngine.Core.ConsoleDebug;
using Veldrid;

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

            RenderingModule targetRendererModule = engineMonitor.GetEngineModule<RenderingModule>();
            WindowModule targetWindowModule = engineMonitor.GetEngineModule<WindowModule>();

            targetRendererModule.InitRenderingModule(bWindow.GetDevice());
            targetWindowModule.InitWindowModule(window);

            string vertexShader = BShaderFileUtility.LoadShaderFile(@"D:\BEngine\Shaders\TestVertexShader.bvs");
            string fragmentShader = BShaderFileUtility.LoadShaderFile(@"D:\BEngine\Shaders\TestFragmentShader.bfs");

            BMesh mesh = BMeshFileUtility.LoadFileVertexes(@"D:\BEngine\Shaders\TestMesh.bmesh");

            BMaterial material = new BMaterial(vertexShader,fragmentShader);

            BSpectrumRenderer renderer = new BSpectrumRenderer();

            renderer.targetMaterial = material;
            renderer.targetMesh = mesh;

            targetRendererModule.CreateRenderingMesh(mesh);
            targetRendererModule.CreateRenderingMaterial(material);
            
            targetRendererModule.CreateRenderingPipeline(renderer);

            targetRendererModule.RegisterSpectrumObserver(renderer);

            while (targetWindowModule.IsWindowActive)
            {
                targetWindowModule.Run();
                targetRendererModule.Run();
            }
          

            return -1;
        }
    }
}
