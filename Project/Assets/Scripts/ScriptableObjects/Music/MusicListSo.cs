using UnityEngine;

namespace Supinfo.Project.Scripts.ScriptableObjects.Music
{
    /// <summary>
    /// MusicListSo is a ScriptableObject used for managing music tracks in the game.
    /// </summary>
    [CreateAssetMenu(fileName = "MusicList", menuName = "ScriptableObjects/MusicList", order = 1)]
    public class MusicListSo : ScriptableObject
    {
        /// <summary>
        /// Array of AudioClips representing the music tracks for different ages.
        /// </summary>
        [SerializeField]
        private AudioClip[] ageMusics;

        /// <summary>
        /// AudioClip for the menu music.
        /// </summary>
        [SerializeField]
        private AudioClip menuMusic;

        /// <summary>
        /// The current age, used to select the appropriate music track.
        /// </summary>
        private int currentAge;

        /// <summary>
        /// Gets the current age music track.
        /// </summary>
        public AudioClip AgeMusics => ageMusics[currentAge];

        /// <summary>
        /// Gets the menu music track.
        /// </summary>
        public AudioClip MenuMusic => menuMusic;

        /// <summary>
        /// Initializes the current age to 0 when the scriptable object is enabled.
        /// </summary>
        private void OnEnable()
        {
            currentAge = 0;
        }

        /// <summary>
        /// Retrieves the music track for the current age.
        /// </summary>
        /// <returns>The music track for the current age.</returns>
        public AudioClip GetCurrentAgeMusic()
        {
            if (currentAge >= 0 && currentAge < ageMusics.Length)
            {
                return ageMusics[currentAge];
            }
            return null;
        }

        /// <summary>
        /// Upgrades the current age to the next age if available.
        /// </summary>
        public void UpgradeAge()
        {
            if (currentAge < ageMusics.Length - 1)
            {
                currentAge++;
            }
        }
    }
}