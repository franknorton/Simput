using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simput
{
    public class InputAction
    {
        private List<MouseButtons> mouseButtons;
        private List<Keys> keys;
        private List<Buttons> gamepadButtons;

        public InputAction()
        {
            mouseButtons = new List<MouseButtons>();
            keys = new List<Keys>();
            gamepadButtons = new List<Buttons>();
        }

        public InputAction AddMouseButton(MouseButtons button)
        {
            mouseButtons.Add(button);
            return this;
        }
        public InputAction AddMouseButton(params MouseButtons[] buttons)
        {
            mouseButtons.AddRange(buttons);
            return this;
        }
        public InputAction AddKey(Keys key)
        {
            keys.Add(key);
            return this;
        }
        public InputAction AddKey(params Keys[] keys)
        {
            this.keys.AddRange(keys);
            return this;
        }
        public InputAction AddGamePadButton(Buttons button)
        {
            gamepadButtons.Add(button);
            return this;
        }
        public InputAction AddGamePadButton(params Buttons[] buttons)
        {
            gamepadButtons.AddRange(buttons);
            return this;
        }

        public InputAction SetMouseButton(MouseButtons button)
        {
            mouseButtons.Clear();
            mouseButtons.Add(button);
            return this;
        }
        public InputAction SetMouseButton(params MouseButtons[] buttons)
        {
            mouseButtons.Clear();
            mouseButtons.AddRange(buttons);
            return this;
        }
        public InputAction SetKey(Keys key)
        {
            keys.Clear();
            keys.Add(key);
            return this;
        }
        public InputAction SetKey(params Keys[] keys)
        {
            this.keys.Clear();
            this.keys.AddRange(keys);
            return this;
        }
        public InputAction SetGamePadButton(Buttons button)
        {
            gamepadButtons.Clear();
            gamepadButtons.Add(button);
            return this;
        }
        public InputAction SetGamePadButton(params Buttons[] buttons)
        {
            gamepadButtons.Clear();
            gamepadButtons.AddRange(buttons);
            return this;
        }

        public InputAction RemoveMouseButton(MouseButtons button)
        {
            mouseButtons.Remove(button);
            return this;
        }
        public InputAction RemoveKey(Keys key)
        {
            keys.Remove(key);
            return this;
        }
        public InputAction RemoveGamePadButton(Buttons button)
        {
            gamepadButtons.Remove(button);
            return this;
        }

        public bool ContainsMouseButton(MouseButtons button)
        {
            return mouseButtons.Contains(button);
        }
        public bool ContainsKey(Keys key)
        {
            return keys.Contains(key);
        }
        public bool ContainsGamepadButton(Buttons button)
        {
            return gamepadButtons.Contains(button);
        }

        public bool WasMouseOrKeyboardPressed()
        {
            return mouseButtons.Any(x => Input.Mouse.WasButtonPressed(x)) || keys.Any(x => Input.Keyboard.WasKeyPressed(x));
        }
        public bool WasMouseOrKeyboardReleased()
        {
            return mouseButtons.Any(x => Input.Mouse.WasButtonReleased(x)) || keys.Any(x => Input.Keyboard.WasKeyReleased(x));
        }
        public bool IsMouseOrKeyboardDown()
        {
            return mouseButtons.Any(x => Input.Mouse.IsButtonDown(x)) || keys.Any(x => Input.Keyboard.IsKeyDown(x));
        }
        public bool IsMouseOrKeyboardUp()
        {
            return mouseButtons.Any(x => Input.Mouse.IsButtonUp(x)) || keys.Any(x => Input.Keyboard.IsKeyUp(x));
        }

        public bool WasGamePadPressed(PlayerIndex playerNumber)
        {
            return gamepadButtons.Any(x => Input.GamePad(playerNumber).WasButtonPressed(x));
        }
        public bool WasGamePadReleased(PlayerIndex playerNumber)
        {
            return gamepadButtons.Any(x => Input.GamePad(playerNumber).WasButtonReleased(x));
        }
        public bool IsGamePadDown(PlayerIndex playerNumber)
        {
            return gamepadButtons.Any(x => Input.GamePad(playerNumber).IsButtonDown(x));
        }
        public bool IsGamePadUp(PlayerIndex playerNumber)
        {
            return gamepadButtons.Any(x => Input.GamePad(playerNumber).IsButtonUp(x));
        }
        
    }
}
