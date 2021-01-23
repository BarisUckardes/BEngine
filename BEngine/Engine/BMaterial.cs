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

        public BMaterial(string vertexCode, string fragmentCode)
        {
            targetVertexShader = vertexCode;
            targetFragmentShader = fragmentCode;
            ParameterMap = new Dictionary<string, BGraphicsResource>();
            RegisteredRenderers = new List<BSpectrumRenderer>();
        }

        internal void RegisterRendererInternal(BSpectrumRenderer targetRenderer)
        {
            RegisteredRenderers.Add(targetRenderer);
        }
        internal void RegisterParameterMapInteral(Dictionary<string,BGraphicsResource> targetParameterMap)
        {
            for(int i=0;i<targetParameterMap.Count;i++)
            {
                ParameterMap.Add(targetParameterMap.ElementAt(i).Key, targetParameterMap.ElementAt(i).Value);
            }
            UpdateAllRenderers();
        }
      
        public void SetParameterTexture2D(string parameterName, BTexture2D targetTexture)
        {
            if (!ParameterMap.ContainsKey(parameterName))
            {
                ParameterMap.Add(parameterName, targetTexture);
                UpdateAllRenderers();
                
            }
            else
            {
                ParameterMap[parameterName] = targetTexture;
            }
        }
       
        private void UpdateAllRenderers()
        {
            /*
            * Update all renderers
            */
            for (int i = 0; i < RegisteredRenderers.Count; i++)
            {
                TargetRenderingModule.CreateRenderingPipeline(RegisteredRenderers[i]);
            }
        }

        public void ApplyMaterial()
        {
            TargetRenderingModule.CreateRenderingMaterial(this);
        }
        public BMaterial CopyMaterial()
        {
            BMaterial bMaterial = new BMaterial(targetVertexShader,targetFragmentShader);

            bMaterial.RegisterParameterMapInteral(this.ParameterMap);
            bMaterial.targetShaders = this.targetShaders;

            return bMaterial;
        }

        

    }
}
