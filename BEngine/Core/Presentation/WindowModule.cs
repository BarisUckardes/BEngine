using BEngine.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veldrid.Sdl2;

namespace BEngine.Core.Presentation
{
    public abstract class WindowModule : IEngineModule
    {
        public abstract bool IsWindowActive { get; }

        public abstract string ModuleName { get; }

        public abstract void InitWindowModule(Sdl2Window targetWindow);

        public abstract void Run();
       
    }
}
