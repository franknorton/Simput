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
    public enum InputVectorType
    {
        LeftStick,
        RightStick
    }
    public class InputVector
    {
        private List<InputVectorBase> vectors;

        public InputVector()
        {
            vectors = new List<InputVectorBase>();
        }

        public InputVector Add(InputVectorType type)
        {
            vectors.Add(new InputVectorOfType(type));
            return this;
        }
        public InputVector AddKeys(Keys upKey, Keys downKey, Keys leftKey, Keys rightKey)
        {
            vectors.Add(new InputVectorKeys(upKey, downKey, leftKey, rightKey));
            return this;
        }
        public InputVector AddGamePadButtons(Buttons upButton, Buttons downButton, Buttons leftButton, Buttons rightButton)
        {
            vectors.Add(new InputVectorButtons(upButton, downButton, leftButton, rightButton));
            return this;
        }
        public InputVector AddMouseButtons(MouseButtons upButton, MouseButtons downButton, MouseButtons leftButton, MouseButtons rightButton)
        {
            vectors.Add(new InputVectorMouseButtons(upButton, downButton, leftButton, rightButton));
            return this;
        }

        public InputVector Set(InputVectorType type)
        {
            vectors.Clear();
            Add(type);
            return this;
        }
        public InputVector Set(Keys upKey, Keys downKey, Keys leftKey, Keys rightKey)
        {
            vectors.Clear();
            AddKeys(upKey, downKey, leftKey, rightKey);
            return this;
        }
        public InputVector Set(Buttons upButton, Buttons downButton, Buttons leftButton, Buttons rightButton)
        {
            vectors.Clear();
            AddGamePadButtons(upButton, downButton, leftButton, rightButton);
            return this;
        }
        public InputVector Set(MouseButtons upButton, MouseButtons downButton, MouseButtons leftButton, MouseButtons rightButton)
        {
            vectors.Clear();
            AddMouseButtons(upButton, downButton, leftButton, rightButton);
            return this;
        }

        public InputVector Remove(InputVectorType type)
        {
            var a = Get(type);
            if (a != null)
                vectors.Remove(a);

            return this;
        }
        public InputVector RemoveKeys(Keys upKey, Keys downKey, Keys leftKey, Keys rightKey)
        {
            var a = Get(upKey, downKey, leftKey, rightKey);
            if (a != null)
                vectors.Remove(a);

            return this;
        }
        public InputVector RemoveButtons(Buttons upButton, Buttons downButton, Buttons leftButton, Buttons rightButton)
        {
            var a = Get(upButton, downButton, leftButton, rightButton);
            if (a != null)
                vectors.Remove(a);

            return this;
        }
        public InputVector RemoveMouseButtons(MouseButtons upButton, MouseButtons downButton, MouseButtons leftButton, MouseButtons rightButton)
        {
            var a = Get(upButton, downButton, leftButton, rightButton);
            if (a != null)
                vectors.Remove(a);

            return this;
        }

        public bool ContainsVector(InputVectorType type)
        {
            return Get(type) != null;
        }
        public bool ContainsVector(Keys upKey, Keys downKey, Keys leftKey, Keys rightKey)
        {
            return Get(upKey, downKey, leftKey, rightKey) != null;
        }
        public bool ContainsVector(Buttons upButton, Buttons downButton, Buttons leftButton, Buttons rightButton)
        {
            return Get(upButton, downButton, leftButton, rightButton) != null;
        }
        public bool ContainsVector(MouseButtons upButton, MouseButtons downButton, MouseButtons leftButton, MouseButtons rightButton)
        {
            return Get(upButton, downButton, leftButton, rightButton) != null;
        }

        internal InputVectorBase Get(InputVectorType type)
        {
            foreach (var vector in vectors)
            {
                if (vector.GetType() == typeof(InputVectorOfType))
                {
                    var t = vector as InputVectorOfType;
                    if (t != null)
                        if (t.type == type)
                            return t;
                }
            }
            return null;
        }
        internal InputVectorBase Get(Keys upKey, Keys downKey, Keys leftKey, Keys rightKey)
        {
            foreach (var vector in vectors)
            {
                if (vector.GetType() == typeof(InputVectorKeys))
                {
                    var t = vector as InputVectorKeys;
                    if (t != null)
                        if (t.upKey == upKey && t.downKey == downKey && t.leftKey == leftKey && t.rightKey == rightKey)
                            return t;
                }
            }
            return null;
        }
        internal InputVectorBase Get(Buttons upButton, Buttons downButton, Buttons leftButton, Buttons rightButton)
        {
            foreach (var vector in vectors)
            {
                if (vector.GetType() == typeof(InputVectorButtons))
                {
                    var t = vector as InputVectorButtons;
                    if (t != null)
                        if (t.upButton == upButton && t.downButton == downButton && t.leftButton == leftButton && t.rightButton == rightButton)
                            return t;
                }
            }
            return null;
        }
        internal InputVectorBase Get(MouseButtons upButton, MouseButtons downButton, MouseButtons leftButton, MouseButtons rightButton)
        {
            foreach (var vector in vectors)
            {
                if (vector.GetType() == typeof(InputVectorMouseButtons))
                {
                    var t = vector as InputVectorMouseButtons;
                    if (t != null)
                        if (t.upButton == upButton && t.downButton == downButton && t.leftButton == leftButton && t.rightButton == rightButton)
                            return t;
                }
            }
            return null;
        }

        public Vector2 Value(PlayerIndex playerNumber)
        {
            foreach(var vector in vectors)
            {
                var value = vector.GetValue(playerNumber);
                if (value != Vector2.Zero)
                    return value;
            }
            return Vector2.Zero;
        }
    }

    public abstract class InputVectorBase
    {
        public abstract Vector2 GetValue(PlayerIndex player);
    }

    public class InputVectorOfType : InputVectorBase
    {
        public InputVectorType type;
        public InputVectorOfType(InputVectorType type)
        {
            this.type = type;
        }

        public override Vector2 GetValue(PlayerIndex player)
        {
            return GetValueFromVectorType(player, type);
        }

        private Vector2 GetValueFromVectorType(PlayerIndex playerNumber, InputVectorType type)
        {
            switch (type)
            {
                case InputVectorType.LeftStick:
                    return Input.GamePad(playerNumber).LeftThumbStick;
                case InputVectorType.RightStick:
                    return Input.GamePad(playerNumber).RightThumbStick;
                default:
                    return Vector2.Zero;
            }
        }
    }

    public class InputVectorButtons : InputVectorBase
    {
        public Buttons upButton, leftButton, downButton, rightButton;

        public InputVectorButtons(Buttons upButton, Buttons downButton, Buttons leftButton, Buttons rightButton)
        {
            this.upButton = upButton;
            this.downButton = downButton;
            this.leftButton = leftButton;
            this.rightButton = rightButton;
        }

        public override Vector2 GetValue(PlayerIndex player)
        {
            var upValue = Input.GamePad(player).IsButtonDown(upButton) ? 1 : 0;
            var downValue = Input.GamePad(player).IsButtonDown(downButton) ? 1 : 0;
            var leftValue = Input.GamePad(player).IsButtonDown(leftButton) ? 1 : 0;
            var rightValue = Input.GamePad(player).IsButtonDown(rightButton) ? 1 : 0;
            return new Vector2(rightValue - leftValue, downValue - upValue);
        }
    }

    public class InputVectorMouseButtons : InputVectorBase
    {
        public MouseButtons upButton, leftButton, downButton, rightButton;

        public InputVectorMouseButtons(MouseButtons upButton, MouseButtons downButton, MouseButtons leftButton, MouseButtons rightButton)
        {
            this.upButton = upButton;
            this.downButton = downButton;
            this.leftButton = leftButton;
            this.rightButton = rightButton;
        }

        public override Vector2 GetValue(PlayerIndex player)
        {
            var upValue = Input.Mouse.IsButtonDown(upButton) ? 1 : 0;
            var downValue = Input.Mouse.IsButtonDown(downButton) ? 1 : 0;
            var leftValue = Input.Mouse.IsButtonDown(leftButton) ? 1 : 0;
            var rightValue = Input.Mouse.IsButtonDown(rightButton) ? 1 : 0;
            return new Vector2(rightValue - leftValue, downValue - upValue);
        }
    }

    public class InputVectorKeys : InputVectorBase
    {
        public Keys upKey, leftKey, downKey, rightKey;

        public InputVectorKeys(Keys upKey, Keys downKey, Keys leftKey, Keys rightKey)
        {
            this.upKey = upKey;
            this.downKey = downKey;
            this.leftKey = leftKey;
            this.rightKey = rightKey;
        }

        public override Vector2 GetValue(PlayerIndex player)
        {
            var upValue = Input.Keyboard.IsKeyDown(upKey) ? 1: 0;
            var downValue = Input.Keyboard.IsKeyDown(downKey) ? 1 : 0;
            var leftValue = Input.Keyboard.IsKeyDown(leftKey) ? 1 : 0;
            var rightValue = Input.Keyboard.IsKeyDown(rightKey) ? 1 : 0;
            return new Vector2(rightValue - leftValue, downValue - upValue);
        }
    }

}
