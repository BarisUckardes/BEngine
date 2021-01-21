using BEngine.Engine.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEngine.Engine
{
    public class BWorld : BObject
    {
        internal static WorldModule TargetWorldModule { get; set; }
        private List<BEntity> registeredEntities;
        private List<BComponent> registeredComponents;
        public BWorld(string name)
        {
            Name = name;
            registeredEntities = new List<BEntity>();
            registeredComponents = new List<BComponent>();

            TargetWorldModule.RegisterNewWorld(this);
        }
        internal void InternalRegisterEntity(BEntity targetEntity)
        {
            registeredEntities.Add(targetEntity);
        }
        internal void InternalRegisterComponent(BComponent targetComponent)
        {
            registeredComponents.Add(targetComponent);
        }
        internal List<BComponent> InternalGetComponents()
        {
            return registeredComponents;
        }
        internal List<BEntity> InternalGetEntities()
        {
            return registeredEntities;
        }

    }
}
