using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Threading;
using System.Windows;

namespace Simput
{
    public interface IKeyboardSubscriber
    {
        void OnCharEntered(char character, Keys key);
        void OnControlEntered(ControlCode code);
    }
    public class SimpleKeyboard
    {
        private KeyboardState lastKeyboardState;
        private KeyboardState currentKeyboardState;
        private Game game;

        public IKeyboardSubscriber Subscriber;
        public bool KeyboardTouched;

        public SimpleKeyboard(Game game)
        {
            this.game = game;
            lastKeyboardState = Keyboard.GetState();
            currentKeyboardState = Keyboard.GetState();
            game.Window.TextInput += Window_TextInput;
        }
        
        internal void Update()
        {
            if (game.IsActive)
            {
                lastKeyboardState = currentKeyboardState;
                currentKeyboardState = Keyboard.GetState();               
            }

            KeyboardTouched = false;
            if (currentKeyboardState.GetPressedKeys().Length > 0)
                KeyboardTouched = true;
        }

        public bool WasKeyPressed(Keys key)
        {
            if(!game.IsActive)
                return false;

            return lastKeyboardState.IsKeyUp(key) && currentKeyboardState.IsKeyDown(key);
        }
        public bool WasKeyReleased(Keys key)
        {
            if (!game.IsActive)
                return false;

            return lastKeyboardState.IsKeyDown(key) && currentKeyboardState.IsKeyUp(key);
        }
        public bool IsKeyDown(Keys key)
        {
            if (!game.IsActive)
                return false;

            return currentKeyboardState.IsKeyDown(key);
        }
        public bool IsKeyUp(Keys key)
        {
            if (!game.IsActive)
                return false;

            return currentKeyboardState.IsKeyUp(key);
        }
        private void Window_TextInput(object sender, TextInputEventArgs e)
        {
            if (!game.IsActive)
                return;

            if (Char.IsControl(e.Character))
            {
                try
                {
                    var control = ControlCodeParser.Parse(e.Character);
                    if (Subscriber != null)
                        Subscriber.OnControlEntered(control);
                }
                catch { }
            }
            else
            {
                if(Subscriber != null)
                    Subscriber.OnCharEntered(e.Character, e.Key);
            }
        }
    }
}
