using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Simput
{
    public static class Input
    {
        private static SimpleKeyboard keyboard;
        public static SimpleKeyboard Keyboard { get { return keyboard; } }

        private static SimpleMouse mouse;
        public static SimpleMouse Mouse { get { return mouse; } }

        private static SimpleGamepad[] gamepads;
        public static SimpleGamepad GamePad(PlayerIndex playerNumber) { return gamepads[(int)playerNumber]; }

        public static GamePadDeadZone GamePadDeadZoneMode;

        static Input() { }

        public static void Initialize(Game game)
        {
            keyboard = new SimpleKeyboard(game.Window);
            mouse = new SimpleMouse(game.Window);
            gamepads = new SimpleGamepad[4] { new SimpleGamepad(PlayerIndex.One), new SimpleGamepad(PlayerIndex.Two), new SimpleGamepad(PlayerIndex.Three), new SimpleGamepad(PlayerIndex.Four) };
            GamePadDeadZoneMode = GamePadDeadZone.IndependentAxes;
        }

        public static bool AnyGamePadTouched()
        {
            foreach(var gamepad in gamepads)
            {
                if (gamepad.GamePadTouched)
                    return true;
            }

            return false;
        }
        public static bool MouseTouched()
        {
            return Mouse.MouseTouched;
        }
        public static bool KeyboardTouched()
        {
            return Keyboard.KeyboardTouched;
        }

        public static void Update()
        {
            keyboard.Update();
            mouse.Update();
            foreach (var gamepad in gamepads)
                gamepad.Update(GamePadDeadZoneMode);
        }
    }
}
