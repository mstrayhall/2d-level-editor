#region File Description
/* InputManager.cs
 * 
 * Isolates input checking to define what keys control what in-game actions.
 */
#endregion

#region Using Statements
using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
#endregion

namespace PlatformerGame
{
    class InputManager
    {
        public const int MaxInputs = 4;

        public readonly KeyboardState[] CurrentKeyboardStates;
        public readonly GamePadState[] CurrentGamePadStates;

        public readonly KeyboardState[] LastKeyboardStates;
        public readonly GamePadState[] LastGamePadStates;

        public readonly bool[] GamePadWasConnected;

        //#if !XBOX360
        //        public MouseState MouseState { get; private set; }
        //#endif

        public InputManager()
        {
            CurrentKeyboardStates = new KeyboardState[MaxInputs];
            CurrentGamePadStates = new GamePadState[MaxInputs];

            LastKeyboardStates = new KeyboardState[MaxInputs];
            LastGamePadStates = new GamePadState[MaxInputs];

            GamePadWasConnected = new bool[MaxInputs];
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < MaxInputs; i++)
            {
                LastKeyboardStates[i] = CurrentKeyboardStates[i];
                LastGamePadStates[i] = CurrentGamePadStates[i];

                CurrentKeyboardStates[i] = Keyboard.GetState((PlayerIndex)i);
                CurrentGamePadStates[i] = GamePad.GetState((PlayerIndex)i);

                // Keep track of whether a gamepad has ever been
                // connected, so we can detect if it is unplugged.
                if (CurrentGamePadStates[i].IsConnected)
                {
                    GamePadWasConnected[i] = true;
                }
            }
        }

        /// <summary>
        /// Checks if the key is currently down
        /// </summary>
        /// <param name="key"></param>
        /// <param name="controllingPlayer"></param>
        /// <param name="playerIndex"></param>
        /// <returns></returns>
        public bool IsKeyDown(Keys key, PlayerIndex? controllingPlayer, out PlayerIndex playerIndex)
        {
            if (controllingPlayer.HasValue)
            {
                playerIndex = controllingPlayer.Value;
                int i = (int)playerIndex;
                return CurrentKeyboardStates[i].IsKeyDown(key);
            }
            else
            {
                // accept input from any player
                return (IsKeyDown(key, PlayerIndex.One, out playerIndex) ||
                        IsKeyDown(key, PlayerIndex.Two, out playerIndex) ||
                        IsKeyDown(key, PlayerIndex.Three, out playerIndex) ||
                        IsKeyDown(key, PlayerIndex.Four, out playerIndex));
            }
        }

        /// <summary>
        /// Checks if the button is currently down
        /// </summary>
        /// <param name="key"></param>
        /// <param name="controllingPlayer"></param>
        /// <param name="playerIndex"></param>
        /// <returns></returns>
        public bool IsButtonDown(Buttons button, PlayerIndex? controllingPlayer, out PlayerIndex playerIndex)
        {
            if (controllingPlayer.HasValue)
            {
                playerIndex = controllingPlayer.Value;
                int i = (int)playerIndex;
                if (CurrentGamePadStates[i].IsButtonDown(button)) return true;
                else return false;
            }
            else
            {
                // accept input from any player
                return (IsButtonDown(button, PlayerIndex.One, out playerIndex) ||
                        IsButtonDown(button, PlayerIndex.Two, out playerIndex) ||
                        IsButtonDown(button, PlayerIndex.Three, out playerIndex) ||
                        IsButtonDown(button, PlayerIndex.Four, out playerIndex));
            }
        }

        /// <summary>
        /// Checks for a "Forward" input action.
        /// </summary>
        /// <param name="controllingPlayer"></param>
        /// <returns></returns>
        public bool IsForward(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return (IsKeyDown(Keys.Up, controllingPlayer, out playerIndex) || IsButtonDown(Buttons.LeftThumbstickUp, controllingPlayer, out playerIndex) ||
                    IsKeyDown(Keys.W, controllingPlayer, out playerIndex) || IsButtonDown(Buttons.DPadUp, controllingPlayer, out playerIndex));
        }

        /// <summary>
        /// Checks for a "Backward" input action.
        /// </summary>
        /// <param name="controllingPlayer"></param>
        /// <returns></returns>
        public bool IsBackward(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return (IsKeyDown(Keys.Down, controllingPlayer, out playerIndex) || IsButtonDown(Buttons.LeftThumbstickDown, controllingPlayer, out playerIndex) ||
                    IsKeyDown(Keys.S, controllingPlayer, out playerIndex) || IsButtonDown(Buttons.DPadDown, controllingPlayer, out playerIndex));
        }

        /// <summary>
        /// Checks for a "Turn Left" input action.
        /// </summary>
        /// <param name="controllingPlayer"></param>
        /// <returns></returns>
        public bool IsTurnLeft(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return (IsKeyDown(Keys.Left, controllingPlayer, out playerIndex) || IsButtonDown(Buttons.LeftThumbstickLeft, controllingPlayer, out playerIndex) ||
                    IsKeyDown(Keys.A, controllingPlayer, out playerIndex) || IsButtonDown(Buttons.DPadLeft, controllingPlayer, out playerIndex));
        }
    }
}
