using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BEngine.Engine
{
    internal class BEngineMonitor
    {
        private List<IEngineModule> RegisteredModules;

        /*
         * Registers a new module to engine
         */
        public void RegisterModule<T>() where T :IEngineModule,new()
        {
            
            RegisteredModules.Add(new T());
        }
        /*
         * Find corresponding engine module
         */
        public T GetEngineModule<T>() where T : class,IEngineModule, new()
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
