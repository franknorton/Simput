using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Simput
{
    
    public class InputMap
    {
        public InputMode Mode;
        private Dictionary<string, InputAction> actions;
        private PlayerIndex playerNumber;

        public InputMap(PlayerIndex playerNumber)
        {
            actions = new Dictionary<string, InputAction>();
            Mode = InputMode.KeyboardAndMouse;
            this.playerNumber = playerNumber;
        }

        public InputAction GetAction(string actionName)
        {
            InputAction action;
            if(actions.TryGetValue(actionName, out action))
            {
                return action;
            }
            return null;
        }
        public void AddAction(string actionName, InputAction newAction)
        {
            actions.Add(actionName, newAction);
        }

        public bool WasActionPressed(string actionName)
        {
            var action = GetAction(actionName);
            if (action == null) return false;

            switch(Mode)
            {
                case InputMode.GamePad:
                    return action.WasGamePadPressed(playerNumber);
                case InputMode.KeyboardAndMouse:
                    return action.WasMouseOrKeyboardPressed();
                default:
                    return false;
            }
        }
        public bool WasActionReleased(string actionName)
        {
            var action = GetAction(actionName);
            if (action == null) return false;
            switch (Mode)
            {
                case InputMode.GamePad:
                    return action.WasGamePadReleased(playerNumber);
                case InputMode.KeyboardAndMouse:
                    return action.WasMouseOrKeyboardReleased();
                default:
                    return false;
            }
        }
        public bool IsActionDown(string actionName)
        {
            var action = GetAction(actionName);
            if (action == null) return false;
            switch (Mode)
            {
                case InputMode.GamePad:
                    return action.IsGamePadDown(playerNumber);
                case InputMode.KeyboardAndMouse:
                    return action.IsMouseOrKeyboardDown();
                default:
                    return false;
            }
        }
        public bool IsActionUp(string actionName)
        {
            var action = GetAction(actionName);
            if (action == null) return false;
            switch (Mode)
            {
                case InputMode.GamePad:
                    return action.IsGamePadUp(playerNumber);
                case InputMode.KeyboardAndMouse:
                    return action.IsMouseOrKeyboardUp();
                default:
                    return false;
            }
        }

        public bool KeyInUse(Keys key, out InputAction action)
        {
            foreach(var inputAction in actions.Values)
            {
                if(inputAction.ContainsKey(key))
                {
                    action = inputAction;
                    return true;
                }
            }

            action = null;
            return false;
        }
        public bool ButtonInUse(Buttons button, out InputAction action)
        {
            foreach(var inputAction in actions.Values)
            {
                if(inputAction.ContainsGamepadButton(button))
                {
                    action = inputAction;
                    return true;
                }
            }

            action = null;
            return false;
        }
        public bool MouseButtonInUse(MouseButtons button, out InputAction action)
        {
            foreach(var inputAction in actions.Values)
            {
                if(inputAction.ContainsMouseButton(button))
                {
                    action = inputAction;
                    return true;
                }
            }
            action = null;
            return false;
        }
    }
}
