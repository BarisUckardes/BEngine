using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEngine.Engine.Modules
{
    public abstract class WorldModule : IEngineModule
    {
        public abstract string ModuleName { get; }
        public abstract void WorldPragma();
        public abstract void RegisterNewWorld(BWorld world);

        public abstract void RegisterEntity(BEntity targetEntity);
        public abstract void RegisterComponent(BComponent targetComponent);

    }
}
