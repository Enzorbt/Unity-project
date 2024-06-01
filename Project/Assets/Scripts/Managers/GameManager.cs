using System;
using UnityEngine;

namespace Supinfo.Project.Scripts.Managers
{
    /// <summary>
    /// Identifiers for different bases in the game.
    /// </summary>
    public enum BaseIdentifier
    {
        BaseAllies,  // Identifier for allied base.
        BaseEnemies  // Identifier for enemy base.
    }

    /// <summary>
    /// Enum representing different game speeds.
    /// </summary>
    public enum GameSpeed
    {
        Stop,   // Game is stopped.
        Slow,   // Game is running at slow speed.
        Play,   // Game is running at normal speed.
        Fast,   // Game is running at fast speed.
        Faster  // Game is running at faster speed.
    }
    
    /// <summary>
    /// Manages the game state and game speed.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        /// <summary>
        /// Handles the game speed change event.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="data">Event data.</param>
        public void OnGameSpeedChange(Component sender, object data)
        {
            if (data is not GameSpeed gameSpeed) return;  // Validate data type.
            Time.timeScale = gameSpeed switch
            {
                GameSpeed.Stop => 0,    // Set timescale to 0 for Stop.
                GameSpeed.Slow => 0.5f, // Set timescale to 0.5 for Slow.
                GameSpeed.Play => 1,    // Set timescale to 1 for Play.
                GameSpeed.Fast => 2,    // Set timescale to 2 for Fast.
                GameSpeed.Faster => 3,  // Set timescale to 3 for Faster.
                _ => throw new ArgumentOutOfRangeException()  // Throw exception for out of range values.
            };
        }
    }
}