using BEngine.Core.ConsoleDebug;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEngine.Engine
{
    internal class BEngineMonitor
    {
       /// <summary>
       /// Current registered engine modules
       /// </summary>
        private List<IEngineModule> RegisteredModules;

        public BEngineMonitor()
        {
            RegisteredModules = new List<IEngineModule>();
        }
        /// <summary>
        /// Registers new module to engine
        /// </summary>
        /// <typeparam name="T">Target module type</typeparam>
        public void RegisterModule<T>() where T :IEngineModule,new()
        {
            BConsoleLog.DropLog("Module Registration : [" + typeof(T).Name + "]",LogType.Verbose);
            RegisteredModules.Add(new T());
        }

        /// <summary>
        /// Try to get wanted module 
        /// </summary>
        /// <typeparam name="T">Wanted module type</typeparam>
        /// <returns>returns the found module if any, or returns null if there is none by that type</returns>
        public T GetEngineModule<T>() where T : class,IEngineModule
        {
            T module;
            for(int i=0;i<RegisteredModules.Count;i++)
            {
                module = RegisteredModules[i] as T;
                if (module != null)
                    return module;
            }

            return null;
        }

        
    }
}
