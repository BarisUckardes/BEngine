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

        public BTexture2D tex;


        public BMaterial(string vertexCode,string fragmentCode)
        {
            targetVertexShader = vertexCode;
            targetFragmentShader = fragmentCode;
        }

        public void ApplyMaterial()
        {
            TargetRenderingModule.CreateRenderingMaterial(this);
        }
            
    }
}
