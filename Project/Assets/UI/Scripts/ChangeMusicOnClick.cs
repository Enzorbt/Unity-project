using UnityEngine;
using UnityEngine.UI;
using Supinfo.Project.Scripts.Managers;

namespace Supinfo.Project.Scripts
{
    /// <summary>
    /// This class is responsible for changing the music when a button is clicked.
    /// </summary>
    public class ChangeMusicOnClick : MonoBehaviour
    {
        /// <summary>
        /// The AudioClip to play when the button is clicked.
        /// </summary>
        public AudioClip easterEggMusic;

        /// <summary>
        /// The Button that triggers the music change.
        /// </summary>
        private Button button;

        /// <summary>
        /// The MusicManager that controls the music in the game.
        /// </summary>
        private MusicManager musicManager;

        /// <summary>
        /// This method is called when the script is loaded or a game is started.
        /// It initializes the button and the music manager.
        /// </summary>
        private void Start()
        {
            button = GetComponent<Button>();
            musicManager = FindObjectOfType<MusicManager>();

            if (button != null)
            {
                button.onClick.AddListener(OnButtonClick);
            }
        }

        /// <summary>
        /// This method is called when the button is clicked.
        /// It changes the music to the easterEggMusic AudioClip.
        /// </summary>
        private void OnButtonClick()
        {
            if (musicManager != null)
            {
                musicManager.ToggleMainMenuMusic(easterEggMusic);
            }
        }
    }
}
