using BEngine.Core.ConsoleDebug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEngine.Engine.Modules
{
    public class CrowdWorldModule : WorldModule
    {
        public override string ModuleName => "Crowd World Module";

        public BWorld currentWorld;

        private List<BEntity> registeredEntities;
        private List<BComponent> registeredComponents;

        public CrowdWorldModule()
        {
            BEntity.TargetWorldModule = this;
            BComponent.TargetWorldModule = this;
            BWorld.TargetWorldModule = this;
        }
        /// <summary>
        /// Registers anew world to the registry
        /// </summary>
        /// <param name="targetWorld"></param>
        public override void RegisterNewWorld(BWorld targetWorld)
        {
            currentWorld = targetWorld;
        }

        /// <summary>
        /// Ticks world
        /// </summary>
        /// <param name="targetWorld"></param>
        public override void WorldPragma()
        {
         
        }

        public override void RegisterEntity(BEntity targetEntity)
        {
            currentWorld.InternalRegisterEntity(targetEntity);
        }
        public override void RegisterComponent(BComponent targetComponent)
        {
            currentWorld.InternalRegisterComponent(targetComponent);
        }
    }
}
