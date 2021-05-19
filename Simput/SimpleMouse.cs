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
        private MouseState _lastMouseState;
        private MouseState _currentMouseState;

        public bool LockMouseToWindow = false;
        public bool UseVirtualCursor = false;
        public bool Touched;
        private int _virtualMouseX;
        private int _virtualMouseY;
        private readonly Game _game;

        public SimpleMouse(Game game)
        {
            _lastMouseState = Mouse.GetState();
            _currentMouseState = Mouse.GetState();
            this._game = game;
            _virtualMouseX = game.Window.ClientBounds.Center.X;
            _virtualMouseY = game.Window.ClientBounds.Center.Y;
        }

        public void Update()
        {
            if (_game.IsActive)
            {
                _lastMouseState = _currentMouseState;
                _currentMouseState = Mouse.GetState();
                Touched = false;

                MoveVirtualMouse();
                ConstrainMouse();
                CheckTouched();
            }
        }

        private void MoveVirtualMouse()
        {
            var changeInX = _currentMouseState.X - _lastMouseState.X;
            var changeInY = _currentMouseState.Y - _lastMouseState.Y;
            if (changeInX != 0 || changeInY != 0) Touched = true;

            if (UseVirtualCursor)
            {
                _virtualMouseX += changeInX;
                _virtualMouseY += changeInY;

                if (_virtualMouseX < 0) _virtualMouseX = 0;
                if (_virtualMouseX > _game.Window.ClientBounds.Width) _virtualMouseX = _game.Window.ClientBounds.Width;
                if (_virtualMouseY < 0) _virtualMouseY = 0;
                if (_virtualMouseY > _game.Window.ClientBounds.Height) _virtualMouseY = _game.Window.ClientBounds.Height;
            }
        }
        private void ConstrainMouse()
        {
            if (LockMouseToWindow && _game.IsActive)
                Mouse.SetPosition(_game.Window.ClientBounds.Center.X, _game.Window.ClientBounds.Center.Y);
        }
        private void CheckTouched()
        {
            if (_currentMouseState != _lastMouseState) Touched = true;
        }

        public int X
        {
            get
            {
                if (LockMouseToWindow || UseVirtualCursor)
                    return _virtualMouseX;
                else
                    return _currentMouseState.X;
            }
        }
        public int Y
        {
            get
            {
                if (LockMouseToWindow || UseVirtualCursor)
                    return _virtualMouseY;
                else return _currentMouseState.Y;
            }
        }

        public bool WasButtonPressed(MouseButtons button)
        {
            if (!_game.IsActive)
                return false;

            return _lastMouseState.IsButtonUp(button) && _currentMouseState.IsButtonDown(button);
        }
        public bool WasButtonReleased(MouseButtons button)
        {
            if (!_game.IsActive)
                return false;

            return _lastMouseState.IsButtonDown(button) && _currentMouseState.IsButtonUp(button);
        }
        public bool IsButtonDown(MouseButtons button)
        {
            if (!_game.IsActive)
                return false;

            return _currentMouseState.IsButtonDown(button);
        }
        public bool IsButtonUp(MouseButtons button)
        {
            if (!_game.IsActive)
                return false;

            return _currentMouseState.IsButtonUp(button);
        }
    }
}
