using BEngine.Core.Presentation;
using BEngine.Engine.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veldrid.Sdl2;

namespace BEngine
{
    class program
    {
        static int Main()
        {
            BWindow bWindow = new BWindow(new BVector2(512,512));
            Sdl2Window window = bWindow.GetWindow();

            while(window.Exists)
            {
                window.PumpEvents();
            }

            return -1;
        }
    }
}
