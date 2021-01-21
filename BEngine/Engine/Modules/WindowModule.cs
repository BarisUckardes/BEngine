using BEngine.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veldrid.Sdl2;

namespace BEngine.Engine.Modules
{
    public abstract class WindowModule : IEngineModule
    {
        public abstract string ModuleName { get; }
        public abstract bool IsWindowActive { get; }
        public abstract void WindowPragma();
       

        public abstract void InitWindowModule(Sdl2Window targetWindow);

       
    }
}
