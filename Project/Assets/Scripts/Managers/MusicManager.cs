using Supinfo.Project.Scripts.ScriptableObjects.Music;
using UnityEngine;
using UnityEngine.Serialization;

namespace Supinfo.Project.Scripts.Managers
{
    public enum SceneName
    {
        mainMenu,
        game
    }

    public class MusicManager : MonoBehaviour
    {
        [FormerlySerializedAs("musicList")] public MusicListSo musicListSo;
        private AudioSource _audioSource;
        private int currentAge = 0;

        [SerializeField]
        private SceneName sceneName;

        private void Awake()
        {
            _audioSource = GetComponentInChildren<AudioSource>();
            PlayMusicForCurrentAge();
            _audioSource.volume = PlayerPrefs.GetFloat("volume");
            _audioSource.mute = PlayerPrefs.GetInt("isMuted") > 0;
            _audioSource.volume = PlayerPrefs.GetFloat("soundVolume");
            _audioSource.mute = PlayerPrefs.GetInt("isSoundMuted") > 0;
        }

        public void UpgradeAge(Component sender, object data)
        {
            currentAge++;
            PlayMusicForCurrentAge();
        }

        private void PlayMusicForCurrentAge()
        {
            switch (sceneName)
            {
                case SceneName.mainMenu:
                    _audioSource.clip = musicListSo.MenuMusic;
                    break;
                case SceneName.game:
                    _audioSource.clip = musicListSo.AgeMusics;
                    break;
            }
            _audioSource.Play();
        }
        
        public void onMusicVolumeChange(Component sender, object data)
        {
            if (data is not float volume) return;
            _audioSource.volume = volume;
            PlayerPrefs.SetFloat("volume", volume);
        }

        public void onMusicVolumeMute(Component sender, object data)
        {
            if (data is not bool isMuted) return;
            _audioSource.mute = isMuted;
            PlayerPrefs.SetInt("isMuted", isMuted ? 1 : 0);
        }
        
        public void onSoundVolumeChange(Component sender, object data)
        {
            if (data is not float soundVolume) return;
            _audioSource.volume = soundVolume;
            PlayerPrefs.SetFloat("soundVolume", soundVolume);
        }

        public void onSoundVolumeMute(Component sender, object data)
        {
            if (data is not bool isSoundMuted) return;
            _audioSource.mute = isSoundMuted;
            PlayerPrefs.SetInt("isSoundMuted", isSoundMuted ? 1 : 0);
        }
    }
}
