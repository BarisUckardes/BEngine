using BEngine.Core.ConsoleDebug;
using BEngine.Engine.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEngine.Engine
{
    sealed public class BEntity : BObject
    {
        internal static WorldModule TargetWorldModule { get; set; }

        private List<BComponent> components;
        public BSpatial TargetSpatial { get; private set; }
        public BEntity()
        {
            TargetWorldModule.RegisterEntity(this);
            components = new List<BComponent>();
            TargetSpatial = AddComponent<BSpatial>();
            TargetSpatial.TargetSpatial = TargetSpatial;
        }

        public BEntity(string targetName) : this()
        {
            Name = targetName;
        }

        public T AddComponent<T>() where T : BComponent,new()
        {
            T newComponent = new T();

            newComponent.TargetSpatial = TargetSpatial;
            newComponent.TargetEntity = this;
            components.Add(newComponent);

            return newComponent;
        }

        public T GetComponent<T>() where T: BComponent,new()
        {
            for(int i=0;i<components.Count;i++)
            {
                T component = components[i] as T;
                if (component != null)
                    return component;
            }

            return null;
        }
            
    }
}
