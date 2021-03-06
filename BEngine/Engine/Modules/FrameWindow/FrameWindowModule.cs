﻿using BEngine.Core.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veldrid;
using Veldrid.Sdl2;

namespace BEngine.Engine.Modules
{
    /// <summary>
    /// Window module, controls window events and inputs
    /// </summary>
    internal class FrameWindowModule : WindowModule
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
            targetWindow.KeyDown += OnKeyDown;
            targetWindow.KeyUp += OnKeyUp;
        }

        private void OnKeyDown(KeyEvent downEvent)
        {
            BInput.SetKeyStateInternal(downEvent.Key, BKeyState.Hold);
        }
        private void OnKeyUp(KeyEvent upEvent)
        {
            BInput.SetKeyStateInternal(upEvent.Key, BKeyState.Up);
        }
        /// <summary>
        /// Runs FrameWindow module Loop
        /// </summary>
        public override void WindowPragma()
        {
            targetWindow.PumpEvents();
        }
    }
}
