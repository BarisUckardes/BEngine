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
        /// <summary>
        /// Material GPU variables
        /// </summary>
        internal Shader[] targetShaders;


        /// <summary>
        /// Material local variables
        /// </summary>
        public string targetVertexShader;
        public string targetFragmentShader;


        public BMaterial(string vertexCode,string fragmentCode)
        {
            targetVertexShader = vertexCode;
            targetFragmentShader = fragmentCode;
        }
    }
}
