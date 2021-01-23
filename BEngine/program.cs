using BEngine.Core.Presentation;
using BEngine.Core.IO;
using BEngine.Engine.Mathematics;
using BEngine.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veldrid;
using Veldrid.Sdl2;
using BEngine.Engine.Graphics;
using BEngine.Engine.Modules;
using BEngine.Engine.Modules.FelineRenderer;

namespace BEngine
{
    class program
    {
        static int Main()
        {
            BWindow bWindow = new BWindow(new BVector2(512,512));

            Sdl2Window window = bWindow.GetWindow();

            BEngineMonitor engineMonitor = new BEngineMonitor();

            engineMonitor.RegisterModule<FrameWindowModule>();
            engineMonitor.RegisterModule<FelineRendererModule>();
            engineMonitor.RegisterModule<CrowdWorldModule>();

            RenderingModule targetRendererModule = engineMonitor.GetEngineModule<RenderingModule>();
            WindowModule targetWindowModule = engineMonitor.GetEngineModule<WindowModule>();
            WorldModule targetWorldModule = engineMonitor.GetEngineModule<WorldModule>();
            
            targetRendererModule.InitRenderingModule(bWindow.GetDevice());
            targetWindowModule.InitWindowModule(window);

            BTexture2D testTexture = BTextureFileUtility.Load2DTextureFromFile(@"D:\BEngine\Textures\BEngine_TestTexture.jpg");
            BTexture2D testTexture2 = BTextureFileUtility.Load2DTextureFromFile(@"D:\BEngine\Textures\BEngine_TestTexture2.png"); 
            BWorld world = new BWorld("My B World");

            string vertexShader = BShaderFileUtility.LoadShaderFile(@"D:\BEngine\Shaders\TestVertexShader.bvs");
            string fragmentShader = BShaderFileUtility.LoadShaderFile(@"D:\BEngine\Shaders\TestFragmentShader.bfs");

            BMesh mesh = BMeshFileUtility.LoadFileVertexes(@"D:\BEngine\Shaders\TestMesh.bmesh");

            BMaterial material = new BMaterial(vertexShader,fragmentShader);
            material.SetParameterTexture2D("myTexture", testTexture);
            material.ApplyMaterial();

            BMaterial material2 = material.CopyMaterial();
           

            mesh.BuildMesh();
            

            BEntity en = new BEntity("Halo");
            en.TargetSpatial.Position = new BVector3(1.5f,0,0);

            BSpectrumRenderer renderer = en.AddComponent<BSpectrumRenderer>();
            renderer.TargetMaterial = material;
            renderer.TargetMesh = mesh;
            
            BEntity observer = new BEntity("Observer Enttiy");
            observer.TargetSpatial.Position = new BVector3(0, 0, 5);
            observer.AddComponent<BSpectrumObserver>();

            BEntity en2 = new BEntity("Halo2");
            en2.TargetSpatial.Position = new BVector3(-1.5f, 0, 0);
            BSpectrumRenderer renderer2 = en2.AddComponent<BSpectrumRenderer>();
            renderer2.TargetMesh = mesh;
            renderer2.TargetMaterial = material2;
            material2.SetParameterTexture2D("myTexture", testTexture2);

            while (targetWindowModule.IsWindowActive)
            {
                targetWindowModule.WindowPragma();
                targetRendererModule.RenderPragma();
            }
      
            return -1;
        }
    }
}
