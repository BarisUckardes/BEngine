using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEngine.Engine.Graphics
{
    using Veldrid;
    public class BUniformBuffer : BGraphicsResource
    {
        public DeviceBuffer targetDeviceBuffer;

        public BUniformBuffer(DeviceBuffer targetDeviceBuffer,ResourceLayout targetLayout,ResourceSet targetResourceSet)
        {
            this.targetDeviceBuffer = targetDeviceBuffer;
            this.targetLayout = targetLayout;
            this.targetResourceSet = targetResourceSet;
        }
    }
}
