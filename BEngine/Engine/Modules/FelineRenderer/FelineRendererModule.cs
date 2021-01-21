using BEngine.Core.ConsoleDebug;
using BEngine.Engine.Graphics;
using BEngine.Engine.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Veldrid;
using Veldrid.SPIRV;

namespace BEngine.Engine.Modules.FelineRenderer
{
    internal class FelineRendererModule : RenderingModule
    {
        
        /// <summary>
        /// Renderer variables
        /// </summary>
        private GraphicsDevice targetDevice;
        private ResourceFactory targetResourceFactory;
        private CommandList targetCommandList;
        public override string ModuleName => "Feline Renderer v0.0.0";

        List<BSpectrumRenderer> registeredRenderers;

        /// <summary>
        /// Inits FelineRenderer
        /// </summary>
        /// <param name="targetDevice">target DX11 device</param>
        public override void InitRenderingModule(GraphicsDevice targetDevice)
        {
            this.targetDevice = targetDevice;
            this.targetResourceFactory = this.targetDevice.ResourceFactory;
            this.targetCommandList = this.targetResourceFactory.CreateCommandList();

            registeredRenderers = new List<BSpectrumRenderer>();
        }

        /// <summary>
        /// Renders the target world
        /// </summary>
        float b = 0;
        public override void RenderPragma()
        {
            targetCommandList.Begin();
            targetCommandList.SetFramebuffer(targetDevice.SwapchainFramebuffer);
            targetCommandList.ClearColorTarget(0, RgbaFloat.Blue);

            /*
             * Loop through every registered renderers
             */
            for(int i=0;i< registeredRenderers.Count;i++)
            {
                BSpectrumRenderer targetRenderer = registeredRenderers[i];
                BSpatial targetSpatial = targetRenderer.TargetSpatial;
                targetSpatial.Rotation = new BVector3(0,b,0);
                b+=0.001f;
                /*
                 * Check if this renderable needs his spatial buffer to be updated
                 */
                if (targetSpatial.IsDirty)
                {
                    
                    targetSpatial.ModelMatrix =
                        Matrix4x4.CreateRotationX(targetSpatial.Rotation.x)*
                        Matrix4x4.CreateRotationY(targetSpatial.Rotation.y)*
                        Matrix4x4.CreateRotationZ(targetSpatial.Rotation.z)*
                        Matrix4x4.CreateScale(targetSpatial.Scale.x,targetSpatial.Scale.y,targetSpatial.Scale.z)*
                        Matrix4x4.CreateTranslation(targetSpatial.Position.x,targetSpatial.Position.y,targetSpatial.Position.z);

                    Matrix4x4 projectionMatrix =
                    Matrix4x4.CreatePerspectiveFieldOfView(
                      1.0f,
                      1.0f,
                      0.001f,
                      100000.0f
                     );
                    Matrix4x4 viewMatrix =
                        Matrix4x4.CreateLookAt(
                            new Vector3(0, 0, 5),
                            new Vector3(0, 0, 0),
                            new Vector3(0, 1, 0)
                    );

                    Matrix4x4 MVP = targetSpatial.ModelMatrix * viewMatrix * projectionMatrix;
                    targetDevice.UpdateBuffer(targetRenderer.targetMVPBuffer.targetDeviceBuffer, 0, MVP);

                    targetSpatial.IsDirty = false;
                }

                
                targetCommandList.SetPipeline(targetRenderer.targetPipeline);

                targetCommandList.SetGraphicsResourceSet(0, targetRenderer.targetMVPBuffer.targetResourceSet);

   
                targetCommandList.SetVertexBuffer(0, targetRenderer.targetMesh.targetVertexBuffer);
                targetCommandList.SetIndexBuffer(targetRenderer.targetMesh.targetIndexBuffer,IndexFormat.UInt32);
                targetCommandList.SetPipeline(targetRenderer.targetPipeline);
                targetCommandList.DrawIndexed(
                    indexCount : (uint)targetRenderer.targetMesh.Indexes.Length,
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
                cullMode : FaceCullMode.None,
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

            gpDescription.ResourceLayouts = new ResourceLayout[] { targetObserver.targetMVPBuffer.targetLayout};
            
            Pipeline pipeline = targetResourceFactory.CreateGraphicsPipeline(gpDescription);

            targetObserver.targetPipeline = pipeline;

        }

        /// <summary>
        /// Registers renderable for the first time
        /// </summary>
        /// <param name="targetRenderer">target renderable object</param>
        public override void RegisterSpectrumRenderer(BSpectrumRenderer targetRenderer)
        {
            /*
             * Create MVP buffer (DeviceBuffer,ResourceLayout,ResourceSet)
             */
            BufferDescription mvpBufferDescription = new BufferDescription()
            {
                Usage = BufferUsage.UniformBuffer,
                SizeInBytes = 64
            };

            DeviceBuffer mvpDeviceBuffer = targetResourceFactory.CreateBuffer(mvpBufferDescription);

            ResourceLayoutElementDescription mvpLayout = new ResourceLayoutElementDescription()
            {
                Kind = ResourceKind.UniformBuffer,
                Stages = ShaderStages.Vertex,
                Name = "spatial_mvp"
            };

            ResourceLayoutElementDescription[] ar = new ResourceLayoutElementDescription[1];
            ar[0] = mvpLayout;

            ResourceLayoutDescription resourceLayout = new ResourceLayoutDescription()
            {
                Elements = ar
            };

            ResourceLayout rLayout = targetResourceFactory.CreateResourceLayout(resourceLayout);
            ResourceSet rSet = targetResourceFactory.CreateResourceSet(new ResourceSetDescription(rLayout, mvpDeviceBuffer));
            rLayout.Name = "MVP Buffer";
            rSet.Name = "MVP Buffer";

            BUniformBuffer mvpBuffer = new BUniformBuffer(mvpDeviceBuffer, rLayout,rSet);

            targetRenderer.targetMVPBuffer = mvpBuffer;

            registeredRenderers.Add(targetRenderer);
        }
     
       
    }
}
