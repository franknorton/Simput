using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Simput
{
    public class SimpleGamepad
    {
        private GamePadState lastGamePadState;
        private GamePadState currentGamePadState;
        private PlayerIndex playerNumber;

        public bool GamePadTouched;

        public SimpleGamepad(PlayerIndex playerNumber)
        {
            this.playerNumber = playerNumber;
            lastGamePadState = GamePad.GetState(playerNumber);
            currentGamePadState = GamePad.GetState(playerNumber);
        }

        public void Update(GamePadDeadZone deadZoneMode)
        {
            lastGamePadState = currentGamePadState;
            currentGamePadState = GamePad.GetState(playerNumber, deadZoneMode);
            CheckGamePadTouched();
        }
        private void CheckGamePadTouched()
        {
            if(IsConnected)
            {
                GamePadTouched = currentGamePadState.PacketNumber != lastGamePadState.PacketNumber;
            }

            GamePadTouched = false;
        }

        public bool IsConnected { get { return currentGamePadState.IsConnected; } }

        public float LeftThumbstickX { get { if (IsConnected) { return 0; } return currentGamePadState.ThumbSticks.Left.X; } }
        public float LeftThumbstickY { get { if (IsConnected) { return 0; } return currentGamePadState.ThumbSticks.Left.Y; } }
        public float RightThumbstickX { get { if (IsConnected) { return 0; } return currentGamePadState.ThumbSticks.Right.X; } }
        public float RightThumbstickY { get { if (IsConnected) { return 0; } return currentGamePadState.ThumbSticks.Right.Y; } }
        public float LeftTrigger { get { if (IsConnected) { return 0; } return currentGamePadState.Triggers.Left; } }
        public float RightTrigger { get { if (IsConnected) { return 0; } return currentGamePadState.Triggers.Right; } }
        

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
    }
}
