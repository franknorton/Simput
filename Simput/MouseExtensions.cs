using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace Simput
{
    public static class MouseExtensions
    {
        public static bool IsButtonDown(this MouseState state, MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.LeftButton:
                    return state.LeftButton == ButtonState.Pressed;
                case MouseButtons.MiddleButton:
                    return state.MiddleButton == ButtonState.Pressed;
                case MouseButtons.RightButton:
                    return state.RightButton == ButtonState.Pressed;
                default:
                    return false;
            }
        }
        public static bool IsButtonUp(this MouseState state, MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.LeftButton:
                    return state.LeftButton == ButtonState.Released;
                case MouseButtons.MiddleButton:
                    return state.MiddleButton == ButtonState.Released;
                case MouseButtons.RightButton:
                    return state.RightButton == ButtonState.Released;
                default:
                    return true;
            }
        }
    }
}
