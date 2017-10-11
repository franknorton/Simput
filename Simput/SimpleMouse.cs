using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Simput
{
    public enum MouseButtons
    {
        LeftButton,
        MiddleButton,
        RightButton
    }

    public class SimpleMouse
    {
        private MouseState lastMouseState;
        private MouseState currentMouseState;

        public bool LockMouseToWindow = false;
        public bool UseVirtualCursor = false;
        public bool Touched;
        private int virtualMouseX;
        private int virtualMouseY;
        private Game game;

        public SimpleMouse(Game game)
        {
            lastMouseState = Mouse.GetState();
            currentMouseState = Mouse.GetState();
            this.game = game;
            virtualMouseX = game.Window.ClientBounds.Center.X;
            virtualMouseY = game.Window.ClientBounds.Center.Y;
        }

        public void Update()
        {
            if (game.IsActive)
            {
                lastMouseState = currentMouseState;
                currentMouseState = Mouse.GetState();
                Touched = false;

                MoveVirtualMouse();
                ConstrainMouse();
                CheckTouched();
            }
        }

        private void MoveVirtualMouse()
        {
            var changeInX = currentMouseState.X - lastMouseState.X;
            var changeInY = currentMouseState.Y - lastMouseState.Y;
            if (changeInX != 0 || changeInY != 0) Touched = true;

            if (UseVirtualCursor)
            {
                virtualMouseX += changeInX;
                virtualMouseY += changeInY;

                if (virtualMouseX < 0) virtualMouseX = 0;
                if (virtualMouseX > game.Window.ClientBounds.Width) virtualMouseX = game.Window.ClientBounds.Width;
                if (virtualMouseY < 0) virtualMouseY = 0;
                if (virtualMouseY > game.Window.ClientBounds.Height) virtualMouseY = game.Window.ClientBounds.Height;
            }
        }
        private void ConstrainMouse()
        {
            if (LockMouseToWindow && game.IsActive)
                Mouse.SetPosition(game.Window.ClientBounds.Center.X, game.Window.ClientBounds.Center.Y);
        }
        private void CheckTouched()
        {
            if (currentMouseState != lastMouseState) Touched = true;
        }

        public int X
        {
            get
            {
                if (LockMouseToWindow || UseVirtualCursor)
                    return virtualMouseX;
                else
                    return currentMouseState.X;
            }
        }
        public int Y
        {
            get
            {
                if (LockMouseToWindow || UseVirtualCursor)
                    return virtualMouseY;
                else return currentMouseState.Y;
            }
        }

        public bool WasButtonPressed(MouseButtons button)
        {
            if (!game.IsActive)
                return false;

            return lastMouseState.IsButtonUp(button) && currentMouseState.IsButtonDown(button);
        }
        public bool WasButtonReleased(MouseButtons button)
        {
            if (!game.IsActive)
                return false;

            return lastMouseState.IsButtonDown(button) && currentMouseState.IsButtonUp(button);
        }
        public bool IsButtonDown(MouseButtons button)
        {
            if (!game.IsActive)
                return false;

            return currentMouseState.IsButtonDown(button);
        }
        public bool IsButtonUp(MouseButtons button)
        {
            if (!game.IsActive)
                return false;

            return currentMouseState.IsButtonUp(button);
        }
    }
}
