using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEngine.Engine.Graphics
{
    public class BUniformBufferData<T>  where T: struct, IFixedSize
    {
        private T targetData;
        private uint DataSizeInBytes;
        private string targetBufferName;

        public BUniformBufferData(T targetData,string targetBufferName)
        {
            this.targetData = targetData;
            DataSizeInBytes = targetData.FixedSize;
            this.targetBufferName = targetBufferName;
        }
    }
}
