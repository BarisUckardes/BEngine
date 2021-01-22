using BEngine.Core.ConsoleDebug;
using BEngine.Engine;
using BEngine.Engine.Graphics;
using BEngine.Engine.Mathematics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEngine.Core.IO
{
    public static class BMeshFileUtility
    {
        public static readonly string[] ValidMeshExtension = { "bmesh" };

        public static BMesh LoadFileVertexes(string path)
        {
            int extensionStartIndex = path.IndexOf(".");

            if (extensionStartIndex == -1)//has no extension
            {
                BConsoleLog.DropLog("There is no mesh extension in this path : " + path, LogType.Error);

                return null;
            }

            extensionStartIndex++;

            int remainingLength = path.Length - extensionStartIndex;

            string extension = path.Substring(extensionStartIndex, remainingLength);

            if (!ValidMeshExtension.Contains(extension)) //has no valid extension
            {
                BConsoleLog.DropLog("There is no valid mesh extension in this path : " + path, LogType.Error);
                return null;
            }

            BMesh mesh = new BMesh();

            /*
             * Map the file
             */
            string[] lines = File.ReadAllLines(path);
            int vertexIndex = Array.IndexOf(lines,"vertexes")+1;
            int indexIndex = Array.IndexOf(lines, "indexes")+1;

            /*
             * Load vertexes
             */
            if (vertexIndex != -1)
            {
                int vertexCount = indexIndex-vertexIndex;

                BVertex[] vertexes = new BVertex[vertexCount];

                int vertIndex = 0;
                for (int i = vertexIndex; i < vertexCount; i++)
                {
                    string[] fragments = lines[i].Split(' ');
                    vertexes[vertIndex] = new BVertex(

                        new BVector3(
                        float.Parse(fragments[0]),
                        float.Parse(fragments[1]),
                        float.Parse(fragments[2])),

                        new BVector2(
                        float.Parse(fragments[3]),
                        float.Parse(fragments[4])
                        ));

                    vertIndex++;
                }

                mesh.Vertexes = vertexes;
            }

           /*
            * Load Indexes
            */
            if(indexIndex!=-1)
            {
                int indexCount = lines.Length - indexIndex;

                
                uint[] indexes = new uint[indexCount*3];

                int triangle = 0;
                for(int i= indexIndex; i< indexIndex + indexCount;i++)
                {
                    string[] fragments = lines[i].Split(' ');

                    indexes[triangle] = Convert.ToUInt32(fragments[0]);
                    indexes[triangle + 1] = Convert.ToUInt32(fragments[1]);
                    indexes[triangle+2] = Convert.ToUInt32(fragments[2]);
                    triangle +=3;
                }

                mesh.Indexes = indexes;
            }


            return mesh;
        }
    }
}
