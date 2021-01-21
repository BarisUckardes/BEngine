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
 
        private List<BWorld> registeredWorlds;
        public override string ModuleName => "Crowd World Module";

        public BWorld currentWorld;

        public CrowdWorldModule()
        {
            registeredWorlds = new List<BWorld>();
        }
        /// <summary>
        /// Registers anew world to the registry
        /// </summary>
        /// <param name="targetWorld"></param>
        public override void RegisterNewWorld(BWorld targetWorld)
        {
            if(registeredWorlds.Contains(targetWorld))
            {
                BConsoleLog.DropLog("World : " + targetWorld.Name + " is already registered!", LogType.Warning);
                return;
            }
            BConsoleLog.DropLog("World : " + targetWorld.Name + " is registered!");
            registeredWorlds.Add(targetWorld);
        }

        /// <summary>
        /// Ticks world
        /// </summary>
        /// <param name="targetWorld"></param>
        public override void WorldPragma()
        {
         
        }

      
    }
}
