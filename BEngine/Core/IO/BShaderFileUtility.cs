using BEngine.Core.ConsoleDebug;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEngine.Core.IO
{
    public static class BShaderFileUtility
    {
        public static readonly string[] ValidShaderExtensions = { "bvs","bfs" };
        public static string LoadShaderFile(string path)
        {
            int extensionStartIndex = path.IndexOf(".");

            if(extensionStartIndex == -1) 
            {
                BConsoleLog.DropLog("There is no shader extension in this path : " + path,LogType.Error);

                return String.Empty;
            }//has no extension

            extensionStartIndex++;

            int remainingLength = path.Length - extensionStartIndex;

            string extension = path.Substring(extensionStartIndex,remainingLength);

            if(!ValidShaderExtensions.Contains(extension))
            {
                BConsoleLog.DropLog("There is no valid shader extension in this path : " + path, LogType.Error);
            }

            return File.ReadAllText(path);
        }
    }
}
