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

namespace PlatformerGameLibrary
{
    public class InputManager
    {
        public const int MaxInputs = 4;

        public readonly KeyboardState[] CurrentKeyboardStates;
        public readonly KeyboardState[] LastKeyboardStates;

        public InputManager()
        {
            CurrentKeyboardStates = new KeyboardState[MaxInputs];
            LastKeyboardStates = new KeyboardState[MaxInputs];
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < MaxInputs; i++)
            {
                LastKeyboardStates[i] = CurrentKeyboardStates[i];

                CurrentKeyboardStates[i] = Keyboard.GetState((PlayerIndex)i);
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
        /// Checks for a "Right" input action.
        /// </summary>
        /// <param name="controllingPlayer"></param>
        /// <returns></returns>
        public bool IsRight(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return (IsKeyDown(Keys.Right, controllingPlayer, out playerIndex) ||
                    IsKeyDown(Keys.D, controllingPlayer, out playerIndex));
        }

        /// <summary>
        /// Checks for a "Left" input action.
        /// </summary>
        /// <param name="controllingPlayer"></param>
        /// <returns></returns>
        public bool IsLeft(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return (IsKeyDown(Keys.Left, controllingPlayer, out playerIndex) ||
                    IsKeyDown(Keys.A, controllingPlayer, out playerIndex));
        }

        /// <summary>
        /// Checks for a "Jump" input action.
        /// </summary>
        /// <param name="controllingPlayer"></param>
        /// <returns></returns>
        public bool IsJump(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return (IsKeyDown(Keys.Up, controllingPlayer, out playerIndex) ||
                    IsKeyDown(Keys.W, controllingPlayer, out playerIndex) ||
                    IsKeyDown(Keys.Space, controllingPlayer, out playerIndex));
        }

        /// <summary>
        /// Checks for a "Crouch" input action.
        /// </summary>
        /// <param name="controllingPlayer"></param>
        /// <returns></returns>
        public bool IsCrouch(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return (IsKeyDown(Keys.Down, controllingPlayer, out playerIndex)||
                    IsKeyDown(Keys.S, controllingPlayer, out playerIndex));
        }

        /// <summary>
        /// Checks for a "Run" input action.
        /// </summary>
        /// <param name="controllingPlayer"></param>
        /// <returns></returns>
        public bool IsRun(PlayerIndex? controllingPlayer)
        {
            PlayerIndex playerIndex;

            return (IsKeyDown(Keys.LeftShift, controllingPlayer, out playerIndex));
        }
    }
}
