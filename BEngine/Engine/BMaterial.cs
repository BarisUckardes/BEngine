using BEngine.Core.ConsoleDebug;
using BEngine.Engine.Graphics;
using BEngine.Engine.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veldrid;

namespace BEngine.Engine
{
    public class BMaterial
    {
        internal static RenderingModule TargetRenderingModule { get; set; }
        /// <summary>
        /// Material GPU variables
        /// </summary>
        internal Shader[] targetShaders;

        /// <summary>
        /// Material local variables
        /// </summary>
        public string targetVertexShader;
        public string targetFragmentShader;
        private List<BSpectrumRenderer> RegisteredRenderers;
        public Dictionary<string, BGraphicsResource> ParameterMap { get; private set; }

        internal void RegisterRendererInternal(BSpectrumRenderer targetRenderer)
        {
            RegisteredRenderers.Add(targetRenderer);
        }
        public void RegisterTexture2DParameter(string parameterName)
        {
            if(ParameterMap.ContainsKey(parameterName))
            {
                BConsoleLog.DropLog("Parameter[" + parameterName + "] is already registered!", LogType.Warning);
                return;
            }

            ParameterMap.Add(parameterName, null);

            /*
             * Update all renderers
             */
            for(int i=0;i<RegisteredRenderers.Count;i++)
            {
                TargetRenderingModule.CreateRenderingPipeline(RegisteredRenderers[i]);
            }

        }
        public void SetParameterTexture2D(string parameterName, BTexture2D targetTexture)
        {
            if (!ParameterMap.ContainsKey(parameterName))
            {
                BConsoleLog.DropLog("Parameter[" + parameterName + "] is not registered!", LogType.Warning);
                return;
            }

            ParameterMap[parameterName] = targetTexture;

        }

        public BMaterial(string vertexCode,string fragmentCode)
        {
            targetVertexShader = vertexCode;
            targetFragmentShader = fragmentCode;
            ParameterMap = new Dictionary<string, BGraphicsResource>();
            RegisteredRenderers = new List<BSpectrumRenderer>();
        }

        public void ApplyMaterial()
        {
            TargetRenderingModule.CreateRenderingMaterial(this);
        }
            
    }
}
