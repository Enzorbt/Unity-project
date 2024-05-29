using UnityEngine;
using UnityEngine.UI;
using Supinfo.Project.Scripts.Managers;

namespace Supinfo.Project.Scripts
{
    public class ChangeMusicOnClick : MonoBehaviour
    {
        public AudioClip easterEggMusic;
        private Button button;
        private MusicManager musicManager;

        private void Start()
        {
            button = GetComponent<Button>();
            musicManager = FindObjectOfType<MusicManager>();

            if (button != null)
            {
                button.onClick.AddListener(OnButtonClick);
            }
        }

        private void OnButtonClick()
        {
            if (musicManager != null)
            {
                musicManager.ToggleMainMenuMusic(easterEggMusic);
            }
        }
    }
}