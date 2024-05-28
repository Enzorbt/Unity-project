using UnityEngine;
namespace Supinfo.Project.Scripts.Managers
{
    public class ButtonSoundManager : MonoBehaviour
    {
        public static ButtonSoundManager Instance { get; private set; }

        private AudioSource _audioSource;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                _audioSource = gameObject.AddComponent<AudioSource>();
                _audioSource.playOnAwake = false;
            }
            else
            {
                Debug.LogWarning("plusieurs instances du managers de sons trouvés!");
            }
        }

        public void PlayButtonSound(AudioClip clip)
        {
            if (_audioSource != null && clip != null)
            {
                _audioSource.PlayOneShot(clip);
            }
        }
    }
}