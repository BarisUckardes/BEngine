using BEngine.Core.ConsoleDebug;
using BEngine.Core.IO;
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
using Veldrid.ImageSharp;

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
        List<BSpectrumObserver> registeredObservers;

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
            registeredObservers = new List<BSpectrumObserver>();

            BSpectrumObserver.TargetRenderingModule = this;
            BSpectrumRenderer.TargetRenderingModule = this;
            BMesh.TargetRenderingModule = this;
            BMaterial.TargetRenderingModule = this;
            BTextureFileUtility.TargetRenderingModule = this;
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

            BSpectrumObserver targetObserver = registeredObservers[0];
            /*
             * Loop through every registered renderers
             */
            for(int i=0;i< registeredRenderers.Count;i++)
            {
                BSpectrumRenderer targetRenderer = registeredRenderers[i];
                BSpatial targetSpatial = targetRenderer.TargetSpatial;
                //targetSpatial.Rotation = new BVector3(0, b, 0);
                //b += 0.00025f;
                if(BInput.IsKeyHold(Key.D))
                {
                    targetObserver.TargetSpatial.Position = new BVector3(targetObserver.TargetSpatial.Position.x+0.001f, targetObserver.TargetSpatial.Position.y, targetObserver.TargetSpatial.Position.z);
                }
                if (BInput.IsKeyHold(Key.A))
                {
                    targetObserver.TargetSpatial.Position = new BVector3(targetObserver.TargetSpatial.Position.x - 0.001f, targetObserver.TargetSpatial.Position.y, targetObserver.TargetSpatial.Position.z);
                }
                if (BInput.IsKeyHold(Key.W))
                {
                    targetObserver.TargetSpatial.Position = new BVector3(targetObserver.TargetSpatial.Position.x, targetObserver.TargetSpatial.Position.y, targetObserver.TargetSpatial.Position.z- 0.001f);
                }
                if (BInput.IsKeyHold(Key.S))
                {
                    targetObserver.TargetSpatial.Position = new BVector3(targetObserver.TargetSpatial.Position.x, targetObserver.TargetSpatial.Position.y, targetObserver.TargetSpatial.Position.z+ 0.001f);
                }


                /*
                 * Check if this renderable needs his spatial buffer to be updated
                 */
                if (targetSpatial.IsDirty || targetObserver.IsDirty || targetObserver.TargetSpatial.IsDirty)
                {
                    if(targetObserver.IsDirty)
                    {
                        targetObserver.ProjectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(targetObserver.FieldOfView, targetObserver.AspectRatio, targetObserver.NearClipPlane, targetObserver.FarClipPlane);
                        targetObserver.IsDirty = false;
                    }

                    if(targetObserver.TargetSpatial.IsDirty)
                    {
                        targetObserver.ViewMatrix = Matrix4x4.CreateLookAt(
                    new Vector3(targetObserver.TargetSpatial.Position.x, targetObserver.TargetSpatial.Position.y, targetObserver.TargetSpatial.Position.z),
                    new Vector3(targetObserver.TargetSpatial.Position.x, targetObserver.TargetSpatial.Position.y, targetObserver.TargetSpatial.Position.z - 1),
                    new Vector3(0, 1, 0));
                        targetObserver.TargetSpatial.IsDirty = false;
                    }

                   targetSpatial.ModelMatrix = targetSpatial.ModelMatrix =
                        Matrix4x4.CreateRotationX(targetSpatial.Rotation.x)*
                        Matrix4x4.CreateRotationY(targetSpatial.Rotation.y)*
                        Matrix4x4.CreateRotationZ(targetSpatial.Rotation.z)*
                        Matrix4x4.CreateScale(targetSpatial.Scale.x,targetSpatial.Scale.y,targetSpatial.Scale.z)*
                        Matrix4x4.CreateTranslation(targetSpatial.Position.x,targetSpatial.Position.y,targetSpatial.Position.z);


                    Matrix4x4 MVP = targetSpatial.ModelMatrix * targetObserver.ViewMatrix * targetObserver.ProjectionMatrix;
                    targetDevice.UpdateBuffer(targetRenderer.targetMVPBuffer.targetDeviceBuffer, 0, MVP);

                    targetSpatial.IsDirty = targetSpatial.IsDirty ? false : false;

                }

                
                targetCommandList.SetPipeline(targetRenderer.targetPipeline);

                /*
                 * Set feline renderer's pre-determined resources
                 */
                targetCommandList.SetGraphicsResourceSet(0, targetRenderer.targetMVPBuffer.targetResourceSet);

                /*
                 * Set parameter map of the material
                 */
                for(int p=0;p<targetRenderer.TargetMaterial.ParameterMap.Count;p++)
                {
                    targetCommandList.SetGraphicsResourceSet((uint)(1+p), targetRenderer.TargetMaterial.ParameterMap.ElementAt(p).Value.targetResourceSet);
                }
               
                targetCommandList.SetVertexBuffer(0, targetRenderer.TargetMesh.targetVertexBuffer);
                targetCommandList.SetIndexBuffer(targetRenderer.TargetMesh.targetIndexBuffer,IndexFormat.UInt32);
                targetCommandList.SetPipeline(targetRenderer.targetPipeline);
                targetCommandList.DrawIndexed(
                    indexCount : (uint)targetRenderer.TargetMesh.Indexes.Length,
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
            DeviceBuffer vertexBuffer = targetResourceFactory.CreateBuffer(new BufferDescription((uint)targetMesh.Vertexes.Length*(20),BufferUsage.VertexBuffer));
            DeviceBuffer indexBuffer = targetResourceFactory.CreateBuffer(new BufferDescription((uint)targetMesh.Indexes.Length * sizeof(uint), BufferUsage.IndexBuffer));

            targetDevice.UpdateBuffer(vertexBuffer, 0, targetMesh.Vertexes);
            targetDevice.UpdateBuffer(indexBuffer, 0, targetMesh.Indexes);

            VertexLayoutDescription vertexLayout = new VertexLayoutDescription(
                new VertexElementDescription("Position", VertexElementSemantic.TextureCoordinate, VertexElementFormat.Float3),
                new VertexElementDescription("UV", VertexElementSemantic.TextureCoordinate, VertexElementFormat.Float2));

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
        public override void CreateRenderingPipeline(BSpectrumRenderer targetRenderer)
        {
           
            if(targetRenderer.targetPipeline != null)
            {
                targetRenderer.targetPipeline.Dispose();
            }
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

            gpDescription.ShaderSet = new ShaderSetDescription(
                vertexLayouts:new VertexLayoutDescription[] { targetRenderer.TargetMesh.targetVertexLayout},
                shaders : targetRenderer.TargetMaterial.targetShaders
                );

            gpDescription.Outputs = targetDevice.SwapchainFramebuffer.OutputDescription;

            List<ResourceLayout> layouts = new List<ResourceLayout>();
            layouts.Add(targetRenderer.targetMVPBuffer.targetLayout);

            for(int i=0;i<targetRenderer.TargetMaterial.ParameterMap.Count;i++)
            {
                layouts.Add(targetRenderer.TargetMaterial.ParameterMap.ElementAt(i).Value.targetLayout);
            }

            gpDescription.ResourceLayouts = layouts.ToArray();
            
            Pipeline pipeline = targetResourceFactory.CreateGraphicsPipeline(gpDescription);

            targetRenderer.targetPipeline = pipeline;

            BConsoleLog.DropLog("New Pipeline Created!");

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

        public override void RegisterSpectrumObserver(BSpectrumObserver targetObserver)
        {
            registeredObservers.Add(targetObserver);
        }

        public override BTexture2D CreateTexture2D(ImageSharpTexture isTexture)
        {
            BTexture2D texture = new BTexture2D();

            texture.targetTexture = isTexture.CreateDeviceTexture(this.targetDevice, this.targetResourceFactory);
            texture.targetView = this.targetResourceFactory.CreateTextureView(texture.targetTexture);

            ResourceLayout textureResourceLayout = this.targetResourceFactory.CreateResourceLayout(
                new ResourceLayoutDescription(
                    new ResourceLayoutElementDescription("testTexture", ResourceKind.TextureReadOnly,ShaderStages.Fragment),
                    new ResourceLayoutElementDescription("testTextureSampler", ResourceKind.Sampler,ShaderStages.Fragment)
                    )
                );
            ResourceSet textureResourceSet = this.targetResourceFactory.CreateResourceSet(new ResourceSetDescription(
                textureResourceLayout,texture.targetView,this.targetDevice.Aniso4xSampler
                )
                );

            texture.targetLayout = textureResourceLayout;
            texture.targetResourceSet = textureResourceSet;

            return texture;
        }
    }
}
