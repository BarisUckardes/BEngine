using BEngine.Core.ConsoleDebug;
using BEngine.Engine.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEngine.Engine
{
    public class BComponent : BObject
    {
        internal static WorldModule TargetWorldModule { get; set; }
        public BSpatial TargetSpatial { get; internal set; }
        public BEntity TargetEntity { get; internal set; }
        public BComponent()
        {
            TargetWorldModule.RegisterComponent(this);
        }
        public virtual void Setup() { }
        public virtual void LogicUpdate() { }
    }
}
