using Supinfo.Project.Scripts.Events; 
using UnityEngine;

namespace Supinfo.Project.UI.Scripts
{
    /// <summary>
    /// Handles playing button click sounds.
    /// </summary>
    public class ButtonSound : MonoBehaviour
    {
        /// <summary>
        /// The audio clip to be played.
        /// </summary>
        [SerializeField]
        private AudioClip buttonClip;

        /// <summary>
        /// Event raised when a button click sound is played.
        /// </summary>
        [SerializeField] private GameEvent onPlayButtonClickSound;

        /// <summary>
        /// Plays the button click sound.
        /// </summary>
        public void PlayButtonSound()
        {
            onPlayButtonClickSound.Raise(this, buttonClip); // Raise the event to play the button click sound.
        }
    }
}