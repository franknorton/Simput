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
        private Dictionary<string, InputAxis> axes;
        private Dictionary<string, InputVector> vectors;
        private PlayerIndex playerNumber;

        public InputMap(PlayerIndex playerNumber)
        {
            actions = new Dictionary<string, InputAction>();
            axes = new Dictionary<string, InputAxis>();
            vectors = new Dictionary<string, InputVector>();
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
        public InputAxis GetAxis(string axisName)
        {
            InputAxis axis;
            if(axes.TryGetValue(axisName, out axis))
            {
                return axis;
            }
            return null;
        }
        public void AddAxis(string axisName, InputAxis newAxis)
        {
            axes.Add(axisName, newAxis);
        }
        public InputVector GetVector(string vectorName)
        {
            InputVector vector;
            if(vectors.TryGetValue(vectorName, out vector))
            {
                return vector;
            }
            return null;
        }
        public void AddVector(string vectorName, InputVector newVector)
        {
            vectors.Add(vectorName, newVector);
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

        public float AxisValue(string axisName)
        {
            var axis = GetAxis(axisName);
            if (axis == null)
                return 0;

            switch(Mode)
            {
                case InputMode.GamePad:
                    return axis.Value(playerNumber);
                default:
                    return 0;
            }
        }

        public Vector2 VectorValue(string vectorName)
        {
            var vector = GetVector(vectorName);
            if (vector == null)
                return Vector2.Zero;
            
            switch(Mode)
            {
                case InputMode.GamePad:
                    return vector.Value(playerNumber);
                default:
                    return Vector2.Zero;
            }
        }

        public void RemoveAction(string actionName)
        {
            InputAction action;
            if(actions.TryGetValue(actionName, out action))
            {
                actions.Remove(actionName);
            }
        }
        public void RemoveVector(string vectorName)
        {
            InputVector action;
            if (vectors.TryGetValue(vectorName, out action))
            {
                vectors.Remove(vectorName);
            }
        }
        public void RemoveAxis(string axisName)
        {
            InputAxis action;
            if (axes.TryGetValue(axisName, out action))
            {
                axes.Remove(axisName);
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
