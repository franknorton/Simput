using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Simput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simput
{
    public enum InputAxisType
    {
        LeftStickX,
        LeftStickY,
        RightStickX,
        RightStickY,
        LeftTrigger,
        RightTrigger,
        DPadX,
        DPadY
    }
    public class InputAxis
    {
        private List<InputAxisBase> axes;

        public InputAxis()
        {
            axes = new List<InputAxisBase>();
        }

        public InputAxis Add(InputAxisType type)
        {
            axes.Add(new InputAxisOfType(type));
            return this;
        }
        public InputAxis AddKeys(Keys positiveKey, Keys negativeKey)
        {
            axes.Add(new InputAxisKeys(positiveKey, negativeKey));
            return this;
        }
        public InputAxis AddMouseButtons(MouseButtons positiveButton, MouseButtons negativeButton)
        {
            axes.Add(new InputAxisMouseButtons(positiveButton, negativeButton));
            return this;
        }
        public InputAxis AddGamePadButtons(Buttons positiveButton, Buttons negativeButton)
        {
            axes.Add(new InputAxisButtons(positiveButton, negativeButton));
            return this;
        }

        public InputAxis Set(InputAxisType type)
        {
            axes.Clear();
            Add(type);
            return this;
        }
        public InputAxis Set(Keys positiveKey, Keys negativeKey)
        {
            axes.Clear();
            AddKeys(positiveKey, negativeKey);
            return this;
        }
        public InputAxis Set(MouseButtons positiveButton, MouseButtons negativeButton)
        {
            axes.Clear();
            AddMouseButtons(positiveButton, negativeButton);
            return this;
        }
        public InputAxis Set(Buttons positiveButton, Buttons negativeButton)
        {
            axes.Clear();
            AddGamePadButtons(positiveButton, negativeButton);
            return this;
        }

        public InputAxis RemoveAxis(InputAxisType type)
        {
            var a = Get(type);
            if (a != null)
                axes.Remove(a);

            return this;
        }
        public InputAxis RemoveKeys(Keys positiveKey, Keys negativeKey)
        {
            var a = Get(positiveKey, negativeKey);
            if (a != null)
                axes.Remove(a);

            return this;
        }
        public InputAxis RemoveMouseButtons(MouseButtons positiveButton, MouseButtons negativeButton)
        {
            var a = Get(positiveButton, negativeButton);
            if (a != null)
                axes.Remove(a);

            return this;
        }
        public InputAxis RemoveGamePadButtons(Buttons positiveButton, Buttons negativeButton)
        {
            var a = Get(positiveButton, negativeButton);
            if (a != null)
                axes.Remove(a);

            return this;
        }


        public bool ContainsAxis(InputAxisType type)
        {
            return Get(type) != null;
        }
        public bool ContainsAxis(Keys positiveKey, Keys negativeKey)
        {
            return Get(positiveKey, negativeKey) != null;
        }
        public bool ContainsAxis(Buttons positiveButton, Buttons negativeButton)
        {
            return Get(positiveButton, negativeButton) != null;
        }
        public bool ContainsAxis(MouseButtons positiveButton, MouseButtons negativeButton)
        {
            return Get(positiveButton, negativeButton) != null;
        }

        internal InputAxisBase Get(InputAxisType type)
        {
            foreach(var axis in axes)
            {
                if(axis.GetType() == typeof(InputAxisOfType))
                {
                    var t = axis as InputAxisOfType;
                    if (t != null)
                        if (t.type == type)
                            return t;
                }
            }
            return null;
        }
        internal InputAxisBase Get(Keys positiveKey, Keys negativeKey)
        {
            foreach(var axis in axes)
            {
                if(axis.GetType() == typeof(InputAxisKeys))
                {
                    var t = axis as InputAxisKeys;
                    if (t != null)
                        if (t.negativeKey == negativeKey && t.positiveKey == positiveKey)
                            return t;
                }
            }
            return null;
        }
        internal InputAxisBase Get(MouseButtons positiveButton, MouseButtons negativeButton)
        {
            foreach (var axis in axes)
            {
                if (axis.GetType() == typeof(InputAxisMouseButtons))
                {
                    var t = axis as InputAxisMouseButtons;
                    if (t != null)
                        if (t.positiveButton == positiveButton && t.negativeButton == negativeButton)
                            return t;
                }
            }
            return null;
        }
        internal InputAxisBase Get(Buttons positiveButton, Buttons negativeButton)
        {
            foreach (var axis in axes)
            {
                if (axis.GetType() == typeof(InputAxisButtons))
                {
                    var t = axis as InputAxisButtons;
                    if (t != null)
                        if (t.positiveButton == positiveButton && t.negativeButton == negativeButton)
                            return t;
                }
            }
            return null;
        }

        public float Value(PlayerIndex playerNumber)
        {
            foreach(var axis in axes)
            {
                var value = axis.GetValue(playerNumber);
                if (value != 0)
                    return value;
            }
            return 0;
        }

        private float GetValueFromAxisType(PlayerIndex playerNumber, InputAxisType type)
        {
            switch(type)
            {
                case InputAxisType.LeftStickX:
                    return Input.GamePad(playerNumber).LeftThumbstickX;
                case InputAxisType.LeftStickY:
                    return Input.GamePad(playerNumber).LeftThumbstickY;
                case InputAxisType.RightStickX:
                    return Input.GamePad(playerNumber).RightThumbstickX;
                case InputAxisType.RightStickY:
                    return Input.GamePad(playerNumber).RightThumbstickY;
                case InputAxisType.LeftTrigger:
                    return Input.GamePad(playerNumber).LeftTrigger;
                case InputAxisType.RightTrigger:
                    return Input.GamePad(playerNumber).RightTrigger;
                default:
                    return 0;
            }
        }
    }

    internal abstract class InputAxisBase
    {
        public abstract float GetValue(PlayerIndex player);
    }
    internal class InputAxisOfType : InputAxisBase
    {
        public InputAxisType type;
        public InputAxisOfType(InputAxisType type)
        {
            this.type = type;
        }
        public override float GetValue(PlayerIndex player)
        {
            return GetValueFromAxisType(player, type);
        }

        private float GetValueFromAxisType(PlayerIndex playerNumber, InputAxisType type)
        {
            switch (type)
            {
                case InputAxisType.LeftStickX:
                    return Input.GamePad(playerNumber).LeftThumbstickX;
                case InputAxisType.LeftStickY:
                    return Input.GamePad(playerNumber).LeftThumbstickY;
                case InputAxisType.RightStickX:
                    return Input.GamePad(playerNumber).RightThumbstickX;
                case InputAxisType.RightStickY:
                    return Input.GamePad(playerNumber).RightThumbstickY;
                case InputAxisType.LeftTrigger:
                    return Input.GamePad(playerNumber).LeftTrigger;
                case InputAxisType.RightTrigger:
                    return Input.GamePad(playerNumber).RightTrigger;
                default:
                    return 0;
            }
        }
    }
    internal class InputAxisKeys : InputAxisBase
    {
        public Keys positiveKey, negativeKey;

        public InputAxisKeys(Keys positiveKey, Keys negativeKey)
        {
            this.positiveKey = positiveKey;
            this.negativeKey = negativeKey;
        }

        public override float GetValue(PlayerIndex player)
        {
            var positiveValue = Input.Keyboard.IsKeyDown(positiveKey) ? 1 : 0;
            var negativeValue = Input.Keyboard.IsKeyDown(negativeKey) ? 1 : 0;
            return positiveValue - negativeValue;
        }
    }
    internal class InputAxisButtons : InputAxisBase
    {
        public Buttons positiveButton, negativeButton;

        public InputAxisButtons(Buttons positiveButton, Buttons negativeButton)
        {
            this.positiveButton = positiveButton;
            this.negativeButton = negativeButton;
        }

        public override float GetValue(PlayerIndex player)
        {
            var positiveValue = Input.GamePad(player).IsButtonDown(positiveButton) ? 1 : 0;
            var negativeValue = Input.GamePad(player).IsButtonDown(negativeButton) ? 1 : 0;
            return positiveValue - negativeValue;
        }
    }
    internal class InputAxisMouseButtons : InputAxisBase
    {
        public MouseButtons positiveButton, negativeButton;

        public InputAxisMouseButtons(MouseButtons positiveButton, MouseButtons negativeButton)
        {
            this.positiveButton = positiveButton;
            this.negativeButton = negativeButton;
        }

        public override float GetValue(PlayerIndex player)
        {
            var positiveValue = Input.Mouse.IsButtonDown(positiveButton) ? 1 : 0;
            var negativeValue = Input.Mouse.IsButtonDown(negativeButton) ? 1 : 0;
            return positiveValue - negativeValue;
        }
    }
}
