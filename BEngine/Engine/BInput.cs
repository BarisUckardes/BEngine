using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veldrid;
namespace BEngine.Engine
{
    public static class BInput
    {
        private static BKeyState[] keyStates = new BKeyState[132];
        internal static void SetKeyStateInternal(Key targetKey,BKeyState state)
        {
            keyStates[(int)targetKey] = state;
        }
        internal static void FlushKeyStateInternal()
        {
            for(int i=0;i<keyStates.Length;i++)
            {
                keyStates[i] = BKeyState.None;
            }
        }

        public static bool IsKeyHold(Key targetKey)
        {
            if(keyStates[(int)targetKey] == BKeyState.Hold)
            {
                return true;
            }

            return false;
        }
        public static bool IsKeyDown(Key targetKey)
        { 
            if(keyStates[(int)targetKey] == BKeyState.Pressed)
            {
                return true;
            }
            return false;
        }

        public static bool IsKeyUp(Key targetKey)
        {
            if (keyStates[(int)targetKey] == BKeyState.Up)
            {
                return true;
            }
            return false;
        }
    }
}
