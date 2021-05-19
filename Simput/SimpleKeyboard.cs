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
        private KeyboardState _lastKeyboardState;
        private KeyboardState _currentKeyboardState;
        private readonly Game _game;

        public IKeyboardSubscriber CharInputSubscriber;
        public bool Touched;

        public SimpleKeyboard(Game game)
        {
            this._game = game;
            _lastKeyboardState = Keyboard.GetState();
            _currentKeyboardState = Keyboard.GetState();
            game.Window.TextInput += Window_TextInput;
        }
        
        public void Update()
        {
            if (_game.IsActive)
            {
                _lastKeyboardState = _currentKeyboardState;
                _currentKeyboardState = Keyboard.GetState();               
            }

            Touched = false;
            if (_currentKeyboardState.GetPressedKeys().Length > 0)
                Touched = true;
        }

        public bool WasKeyPressed(Keys key)
        {
            if(!_game.IsActive)
                return false;

            return _lastKeyboardState.IsKeyUp(key) && _currentKeyboardState.IsKeyDown(key);
        }
        public bool WasKeyReleased(Keys key)
        {
            if (!_game.IsActive)
                return false;

            return _lastKeyboardState.IsKeyDown(key) && _currentKeyboardState.IsKeyUp(key);
        }
        public bool IsKeyDown(Keys key)
        {
            if (!_game.IsActive)
                return false;

            return _currentKeyboardState.IsKeyDown(key);
        }
        public bool IsKeyUp(Keys key)
        {
            if (!_game.IsActive)
                return false;

            return _currentKeyboardState.IsKeyUp(key);
        }
        private void Window_TextInput(object sender, TextInputEventArgs e)
        {
            if (!_game.IsActive)
                return;

            if (Char.IsControl(e.Character))
            {
                try
                {
                    var control = ControlCodeParser.Parse(e.Character);
                    if (CharInputSubscriber != null)
                        CharInputSubscriber.OnControlEntered(control);
                }
                catch { }
            }
            else
            {
                if(CharInputSubscriber != null)
                    CharInputSubscriber.OnCharEntered(e.Character, e.Key);
            }
        }
    }
}
