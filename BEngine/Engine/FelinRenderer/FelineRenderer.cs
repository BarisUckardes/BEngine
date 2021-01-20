using BEngine.Core.ConsoleDebug;
using BEngine.Engine.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veldrid;
using Veldrid.SPIRV;

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

        private List<BSpectrumRenderer> RegisteredRenderers;

        /// <summary>
        /// Inits FelineRenderer
        /// </summary>
        /// <param name="targetDevice">target DX11 device</param>
        public override void InitRenderingModule(GraphicsDevice targetDevice)
        {
            this.targetDevice = targetDevice;
            this.targetResourceFactory = this.targetDevice.ResourceFactory;
            this.targetCommandList = this.targetResourceFactory.CreateCommandList();

            RegisteredRenderers = new List<BSpectrumRenderer>();
        }

        /// <summary>
        /// FelineRenderer loop
        /// </summary>
        public override void Run()
        {
            targetCommandList.Begin();
            targetCommandList.SetFramebuffer(targetDevice.SwapchainFramebuffer);
            targetCommandList.ClearColorTarget(0, RgbaFloat.Blue);

            for(int i=0;i<RegisteredRenderers.Count;i++)
            {
                targetCommandList.SetVertexBuffer(0, RegisteredRenderers[i].targetMesh.targetVertexBuffer);
                targetCommandList.SetIndexBuffer(RegisteredRenderers[i].targetMesh.targetIndexBuffer,IndexFormat.UInt32);
                targetCommandList.SetPipeline(RegisteredRenderers[i].targetPipeline);
                targetCommandList.DrawIndexed(
                    indexCount : (uint)RegisteredRenderers[i].targetMesh.Indexes.Length,
                    instanceCount : 1,
                    indexStart : 0,
                    vertexOffset : 0,
                    instanceStart : 0);
            }
            targetCommandList.End();
            targetDevice.SubmitCommands(targetCommandList);
            targetDevice.SwapBuffers();
        }

        /// <summary>
        /// Creates a mesh on GPU side
        /// </summary>
        /// <param name="targetMesh">Target mesh which needs to be created on GPU</param>
        public override void CreateRenderingMesh(BMesh targetMesh)
        {
            DeviceBuffer vertexBuffer = targetResourceFactory.CreateBuffer(new BufferDescription((uint)targetMesh.Vertexes.Length*(24),BufferUsage.VertexBuffer));
            DeviceBuffer indexBuffer = targetResourceFactory.CreateBuffer(new BufferDescription((uint)targetMesh.Indexes.Length * sizeof(uint), BufferUsage.IndexBuffer));

            targetDevice.UpdateBuffer(vertexBuffer, 0, targetMesh.Vertexes);
            targetDevice.UpdateBuffer(indexBuffer, 0, targetMesh.Indexes);

            VertexLayoutDescription vertexLayout = new VertexLayoutDescription(
                new VertexElementDescription("Position", VertexElementSemantic.TextureCoordinate, VertexElementFormat.Float3),
                new VertexElementDescription("Color", VertexElementSemantic.TextureCoordinate, VertexElementFormat.Float3));

            targetMesh.targetVertexBuffer = vertexBuffer;
            targetMesh.targetIndexBuffer = indexBuffer;
            targetMesh.targetVertexLayout = vertexLayout;
        }

        /// <summary>
        /// Creates a material on GPU side
        /// </summary>
        /// <param name="targetMaterial">target material which needs to be created on GPU</param>
        public override void CreateRenderingMaterial(BMaterial targetMaterial)
        {
            ShaderDescription vertexShaderDescription = new ShaderDescription(
                ShaderStages.Vertex,
                Encoding.UTF8.GetBytes(targetMaterial.targetVertexShader),
                "main"
                );

            ShaderDescription fragmentShaderDescription = new ShaderDescription(
                ShaderStages.Fragment,
                Encoding.UTF8.GetBytes(targetMaterial.targetFragmentShader),
                "main"
                );

            targetMaterial.targetShaders = targetResourceFactory.CreateFromSpirv(vertexShaderDescription, fragmentShaderDescription);
        }

        /// <summary>
        /// Creates a pipeline on GPU side
        /// </summary>
        /// <param name="targetObserver">target observer which is currently rendering</param>
        public override void CreateRenderingPipeline(BSpectrumRenderer targetObserver)
        {
           

            GraphicsPipelineDescription gpDescription = new GraphicsPipelineDescription();

            gpDescription.BlendState = BlendStateDescription.SingleOverrideBlend;

            gpDescription.DepthStencilState = new DepthStencilStateDescription(
                depthTestEnabled: true,
                depthWriteEnabled: true,
                comparisonKind: ComparisonKind.LessEqual
                );

            gpDescription.RasterizerState = new RasterizerStateDescription(
                cullMode : FaceCullMode.Back,
                fillMode : PolygonFillMode.Solid,
                frontFace : FrontFace.Clockwise,
                depthClipEnabled : true,
                scissorTestEnabled : false
                );

            gpDescription.PrimitiveTopology = PrimitiveTopology.TriangleList;
            gpDescription.ResourceLayouts = System.Array.Empty<ResourceLayout>();

            gpDescription.ShaderSet = new ShaderSetDescription(
                vertexLayouts:new VertexLayoutDescription[] {targetObserver.targetMesh.targetVertexLayout},
                shaders : targetObserver.targetMaterial.targetShaders
                );

            gpDescription.Outputs = targetDevice.SwapchainFramebuffer.OutputDescription;

          
            Pipeline pipeline = targetResourceFactory.CreateGraphicsPipeline(gpDescription);

            targetObserver.targetPipeline = pipeline;

        }

        public override void RegisterSpectrumObserver(BSpectrumRenderer targetRenderer)
        {
            RegisteredRenderers.Add(targetRenderer);
        }
    }
}
