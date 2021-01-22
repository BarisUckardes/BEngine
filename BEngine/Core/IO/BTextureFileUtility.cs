using BEngine.Engine.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veldrid.ImageSharp;
using Veldrid;
using BEngine.Engine.Modules;

namespace BEngine.Core.IO
{
    public static class BTextureFileUtility
    {
        internal static RenderingModule TargetRenderingModule { get; set; }
        public static BTexture2D Load2DTextureFromFile(string path)
        {
            ImageSharpTexture isTexture = new ImageSharpTexture(path);
           
            return TargetRenderingModule.CreateTexture2D(isTexture);
        }
    }
}
