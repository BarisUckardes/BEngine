using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veldrid.Sdl2;

namespace BEngine.Core.Presentation.FrameWindow
{
    /// <summary>
    /// Window module, controls window events and inputs
    /// </summary>
    internal class FrameWindow : WindowModule
    {
        /// <summary>
        /// Target Native SDL window
        /// </summary>
        private Sdl2Window targetWindow;
        public override string ModuleName => "Frame Window Module";
        public override bool IsWindowActive => targetWindow.Exists;

        /// <summary>
        /// Inits FrameWindow
        /// </summary>
        /// <param name="targetWindow">Target native SDL window</param>
        public override void InitWindowModule(Sdl2Window targetWindow)
        {
            this.targetWindow = targetWindow;
        }

        /// <summary>
        /// Runs FrameWindow module Loop
        /// </summary>
        public override  void Run()
        {
            targetWindow.PumpEvents();
        }
    }
}
