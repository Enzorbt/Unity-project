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
        [FormerlySerializedAs("musicList")] 
        public MusicListSo musicListSo;

        [SerializeField]
        private AudioSource audioSourceMusic;

        [SerializeField]
        private AudioSource audioSourceSound;

        private int currentAge = 0;
        private AudioClip originalMainMenuMusic;
        private bool isEasterEggMusicPlaying = false;

        [SerializeField]
        private SceneName sceneName;

        private void Awake()
        {
            originalMainMenuMusic = musicListSo.MenuMusic;
            PlayMusicForCurrentAge();
            audioSourceMusic.volume = PlayerPrefs.GetFloat("volume");
            audioSourceMusic.mute = PlayerPrefs.GetInt("isMuted") > 0;
            audioSourceSound.volume = PlayerPrefs.GetFloat("soundVolume");
            audioSourceSound.mute = PlayerPrefs.GetInt("isSoundMuted") > 0;
        }

        public void UpgradeAge(Component sender, object data)
        {
            musicListSo.UpgradeAge();
            PlayMusicForCurrentAge();
        }

        private void PlayMusicForCurrentAge()
        {
            switch (sceneName)
            {
                case SceneName.mainMenu:
                    audioSourceMusic.clip = musicListSo.MenuMusic;
                    break;
                case SceneName.game:
                    audioSourceMusic.clip = musicListSo.GetCurrentAgeMusic();
                    break;
            }
            audioSourceMusic.Play();
        }

        public void onMusicVolumeChange(Component sender, object data)
        {
            if (data is not float volume) return;
            audioSourceMusic.volume = volume;
            PlayerPrefs.SetFloat("volume", volume);
        }

        public void onMusicVolumeMute(Component sender, object data)
        {
            if (data is not bool isMuted) return;
            audioSourceMusic.mute = isMuted;
            PlayerPrefs.SetInt("isMuted", isMuted ? 1 : 0);
        }

        public void onSoundVolumeChange(Component sender, object data)
        {
            if (data is not float soundVolume) return;
            audioSourceSound.volume = soundVolume;
            PlayerPrefs.SetFloat("soundVolume", soundVolume);
        }

        public void onSoundVolumeMute(Component sender, object data)
        {
            if (data is not bool isSoundMuted) return;
            audioSourceSound.mute = isSoundMuted;
            PlayerPrefs.SetInt("isSoundMuted", isSoundMuted ? 1 : 0);
        }

        public void onPlayButtonClickSound(Component sender, object data)
        {
            if (data is AudioClip buttonClip && audioSourceSound != null)
            {
                audioSourceSound.PlayOneShot(buttonClip);
            }
        }
        
        public void ToggleMainMenuMusic(AudioClip easterEggMusic)
        {
            if (sceneName == SceneName.mainMenu && audioSourceMusic != null)
            {
                if (isEasterEggMusicPlaying)
                {
                    audioSourceMusic.clip = originalMainMenuMusic;
                }
                else
                {
                    audioSourceMusic.clip = easterEggMusic;
                }
                isEasterEggMusicPlaying = !isEasterEggMusicPlaying;
                audioSourceMusic.Play();
            }
        }
    }
}
