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
        private GamePadState lastGamePadState;
        private GamePadState currentGamePadState;
        private PlayerIndex playerNumber;
        private Game game;

        private bool vibrating = false;
        private float vibratingTime = 0;
        private float vibrationDuration = 0;

        public bool Touched = false;

        public SimpleGamePad(PlayerIndex playerNumber, Game game)
        {
            this.game = game;
            this.playerNumber = playerNumber;
            lastGamePadState = GamePad.GetState(playerNumber);
            currentGamePadState = GamePad.GetState(playerNumber);
        }

        public void Update(GameTime gameTime, GamePadDeadZone deadZoneMode)
        {
            if (game.IsActive)
            {
                lastGamePadState = currentGamePadState;
                currentGamePadState = GamePad.GetState(playerNumber, deadZoneMode);
                UpdateVibrating(gameTime);
                CheckGamePadTouched();
            }
        }
        private void UpdateVibrating(GameTime gameTime)
        {
            if(vibrating)
            {
                vibratingTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
                if(vibratingTime >= vibrationDuration)
                {
                    if (IsConnected)
                        GamePad.SetVibration(playerNumber, 0, 0);

                    vibratingTime = 0;
                    vibrationDuration = 0;
                    vibrating = false;
                }
            }
        }
        private void CheckGamePadTouched()
        {
            Touched = false;

            if (IsConnected)
            {
                Touched = currentGamePadState.PacketNumber != lastGamePadState.PacketNumber;
            }
        }

        public bool IsConnected { get { return currentGamePadState.IsConnected; } }

        public Vector2 LeftThumbStick { get { if(!IsConnected) { return Vector2.Zero; } return currentGamePadState.ThumbSticks.Left; } }
        public float LeftThumbstickX { get { if (!IsConnected) { return 0; } return currentGamePadState.ThumbSticks.Left.X; } }
        public float LeftThumbstickY { get { if (!IsConnected) { return 0; } return currentGamePadState.ThumbSticks.Left.Y; } }
        public Vector2 RightThumbStick { get { if (!IsConnected) { return Vector2.Zero; } return currentGamePadState.ThumbSticks.Right; } }
        public float RightThumbstickX { get { if (!IsConnected) { return 0; } return currentGamePadState.ThumbSticks.Right.X; } }
        public float RightThumbstickY { get { if (!IsConnected) { return 0; } return currentGamePadState.ThumbSticks.Right.Y; } }
        public float LeftTrigger { get { if (!IsConnected) { return 0; } return currentGamePadState.Triggers.Left; } }
        public float RightTrigger { get { if (!IsConnected) { return 0; } return currentGamePadState.Triggers.Right; } }
        

        public bool WasButtonPressed(Buttons button)
        {
            if (!IsConnected) return false;
            return lastGamePadState.IsButtonUp(button) && currentGamePadState.IsButtonDown(button);
        }
        public bool WasButtonReleased(Buttons button)
        {
            if (!IsConnected) { return false; }
            return lastGamePadState.IsButtonDown(button) && currentGamePadState.IsButtonUp(button);
        }
        public bool IsButtonDown(Buttons button)
        {
            if (!IsConnected) return false;
            return currentGamePadState.IsButtonDown(button);
        }
        public bool IsButtonUp(Buttons button)
        {
            if (!IsConnected) return false;
            return currentGamePadState.IsButtonUp(button);
        }

        public void Vibrate(float leftAmount, float rightAmount)
        {
            Vibrate(leftAmount, rightAmount, 0);
        }
        public void Vibrate(float leftAmount, float rightAmount, float durationInMilliseconds)
        {
            vibrationDuration = durationInMilliseconds;
            vibratingTime = 0;
            leftAmount = MathHelper.Clamp(leftAmount, 0, 1);
            rightAmount = MathHelper.Clamp(rightAmount, 0, 1);
            GamePad.SetVibration(playerNumber, leftAmount, rightAmount);
        }
        public void StopVibrating()
        {
            GamePad.SetVibration(playerNumber, 0, 0);
            vibrationDuration = 0;
            vibratingTime = 0;
        }
    }
}
