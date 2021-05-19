using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Simput
{
    public class SimpleGamePad
    {
        private GamePadState _lastGamePadState;
        private GamePadState _currentGamePadState;
        public PlayerIndex PlayerNumber;
        private Game _game;

        private bool _vibrating = false;
        private float _vibratingTime = 0;
        private float _vibrationDuration = 0;

        public bool Touched = false;
        public event GamePadConnectionChange OnConnectionChange;

        public SimpleGamePad(PlayerIndex playerNumber, Game game)
        {
            this._game = game;
            this.PlayerNumber = playerNumber;
            _lastGamePadState = GamePad.GetState(playerNumber);
            _currentGamePadState = GamePad.GetState(playerNumber);
        }

        public void Update(GameTime gameTime, GamePadDeadZone deadZoneMode)
        {
            _lastGamePadState = _currentGamePadState;
            _currentGamePadState = GamePad.GetState(PlayerNumber, deadZoneMode);
            UpdateVibrating(gameTime);
            CheckGamePadTouched();
            
            if(Connected)
                OnConnectionChange?.Invoke(true);
            
            if(Disconnected)
                OnConnectionChange?.Invoke(false);
        }

        private void UpdateVibrating(GameTime gameTime)
        {
            if(_vibrating)
            {
                _vibratingTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if(_vibratingTime >= _vibrationDuration)
                {
                    if (IsConnected)
                        GamePad.SetVibration(PlayerNumber, 0, 0);

                    _vibratingTime = 0;
                    _vibrationDuration = 0;
                    _vibrating = false;
                }
            }
        }
        private void CheckGamePadTouched()
        {
            Touched = false;

            if (IsConnected)
            {
                Touched = _currentGamePadState.PacketNumber != _lastGamePadState.PacketNumber;
            }
        }

        public bool IsConnected => _currentGamePadState.IsConnected;
        public bool Connected => _currentGamePadState.IsConnected && !_lastGamePadState.IsConnected;
        public bool Disconnected => !_currentGamePadState.IsConnected && _lastGamePadState.IsConnected;
        
        public Vector2 LeftThumbStick { get { if(!IsConnected) { return Vector2.Zero; } return new Vector2(LeftThumbstickX, LeftThumbstickY); } }
        public float LeftThumbstickX { get { if (!IsConnected) { return 0; } return _currentGamePadState.ThumbSticks.Left.X; } }
        public float LeftThumbstickY { get { if (!IsConnected) { return 0; } return -_currentGamePadState.ThumbSticks.Left.Y; } }
        public Vector2 RightThumbStick { get { if (!IsConnected) { return Vector2.Zero; } return new Vector2(RightThumbstickX, RightThumbstickY); } }
        public float RightThumbstickX { get { if (!IsConnected) { return 0; } return _currentGamePadState.ThumbSticks.Right.X; } }
        public float RightThumbstickY { get { if (!IsConnected) { return 0; } return -_currentGamePadState.ThumbSticks.Right.Y; } }
        public float LeftTrigger { get { if (!IsConnected) { return 0; } return _currentGamePadState.Triggers.Left; } }
        public float RightTrigger { get { if (!IsConnected) { return 0; } return _currentGamePadState.Triggers.Right; } }
        

        public bool WasButtonPressed(Buttons button)
        {
            if (!IsConnected) return false;
            return _lastGamePadState.IsButtonUp(button) && _currentGamePadState.IsButtonDown(button);
        }
        public bool WasButtonReleased(Buttons button)
        {
            if (!IsConnected) { return false; }
            return _lastGamePadState.IsButtonDown(button) && _currentGamePadState.IsButtonUp(button);
        }
        public bool IsButtonDown(Buttons button)
        {
            if (!IsConnected) return false;
            return _currentGamePadState.IsButtonDown(button);
        }
        public bool IsButtonUp(Buttons button)
        {
            if (!IsConnected) return false;
            return _currentGamePadState.IsButtonUp(button);
        }

        public void Vibrate(float leftAmount, float rightAmount)
        {
            Vibrate(leftAmount, rightAmount, 0);
        }
        public void Vibrate(float leftAmount, float rightAmount, float durationInMilliseconds)
        {
            _vibrationDuration = durationInMilliseconds;
            _vibratingTime = 0;
            leftAmount = MathHelper.Clamp(leftAmount, 0, 1);
            rightAmount = MathHelper.Clamp(rightAmount, 0, 1);
            GamePad.SetVibration(PlayerNumber, leftAmount, rightAmount);
        }
        public void StopVibrating()
        {
            GamePad.SetVibration(PlayerNumber, 0, 0);
            _vibrationDuration = 0;
            _vibratingTime = 0;
        }
    }

    public delegate void GamePadConnectionChange(bool connected);
}
