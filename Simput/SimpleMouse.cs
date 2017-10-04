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
        public bool MouseTouched;
        private int virtualMouseX;
        private int virtualMouseY;
        private GameWindow window;

        public SimpleMouse(GameWindow window)
        {
            lastMouseState = Mouse.GetState();
            currentMouseState = Mouse.GetState();
            this.window = window;
            virtualMouseX = window.ClientBounds.Center.X;
            virtualMouseY = window.ClientBounds.Center.Y;
        }

        public void Update()
        {
            lastMouseState = currentMouseState;
            currentMouseState = Mouse.GetState();
            MouseTouched = false;

            MoveVirtualMouse();
            ConstrainMouse();
            CheckButtonsTouched();
        }

        private void MoveVirtualMouse()
        {
            var changeInX = currentMouseState.X - lastMouseState.X;
            var changeInY = currentMouseState.Y - lastMouseState.Y;

            virtualMouseX += changeInX;
            virtualMouseY += changeInY;

            if (virtualMouseX < 0) virtualMouseX = 0;
            if (virtualMouseX > window.ClientBounds.Width) virtualMouseX = window.ClientBounds.Width;
            if (virtualMouseY < 0) virtualMouseY = 0;
            if (virtualMouseY > window.ClientBounds.Height) virtualMouseY = window.ClientBounds.Height;

            if (changeInX != 0 || changeInY != 0) MouseTouched = true;
        }
        private void ConstrainMouse()
        {
            if (LockMouseToWindow)
                Mouse.SetPosition(window.ClientBounds.Center.X, window.ClientBounds.Center.Y);
        }
        private void CheckButtonsTouched()
        {
            if (currentMouseState != lastMouseState) MouseTouched = true;
        }

        public int X { get { return currentMouseState.X; } }
        public int Y { get { return currentMouseState.Y; } }

        public bool WasButtonPressed(MouseButtons button)
        {
            return lastMouseState.IsButtonUp(button) && currentMouseState.IsButtonDown(button);
        }
        public bool WasButtonReleased(MouseButtons button)
        {
            return lastMouseState.IsButtonDown(button) && currentMouseState.IsButtonUp(button);
        }
        public bool IsButtonDown(MouseButtons button)
        {
            return currentMouseState.IsButtonDown(button);
        }
        public bool IsButtonUp(MouseButtons button)
        {
            return currentMouseState.IsButtonUp(button);
        }
    }
}
