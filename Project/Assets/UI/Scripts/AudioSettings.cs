using Supinfo.Project.Scripts.Events;
using UnityEngine;
using UnityEngine.UI;
namespace Supinfo.Project.UI.Scripts
{
    public class AudioSettings : MonoBehaviour
    {
        public Slider musicVolumeSlider;
        public Toggle musicMuteToggle;
        public Slider soundVolumeSlider;
        public Toggle soundMuteToggle;
        
        void Start()
        {
            musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
            musicMuteToggle.onValueChanged.AddListener(ToggleMusicMute);
            musicVolumeSlider.value = PlayerPrefs.GetFloat("volume");
            musicMuteToggle.isOn = PlayerPrefs.GetInt("isMuted") > 0 ? true : false ;
            
            soundVolumeSlider.onValueChanged.AddListener(SetSoundVolume);
            soundMuteToggle.onValueChanged.AddListener(ToggleSoundMute);
            soundVolumeSlider.value = PlayerPrefs.GetFloat("soundVolume");
            soundMuteToggle.isOn = PlayerPrefs.GetInt("isSoundMuted") > 0 ? true : false ;
            
            //onVolumeChange.Raise(this, volumeSlider.value );
            //onVolumeMute.Raise(this, muteToggle.isOn);
        }
        
        [SerializeField] private GameEvent onMusicVolumeChange;
        [SerializeField] private GameEvent onMusicVolumeMute;
        
        [SerializeField] private GameEvent onSoundVolumeChange;
        [SerializeField] private GameEvent onSoundVolumeMute;
        
        public void SetMusicVolume(float volume)
        {
            onMusicVolumeChange.Raise(this, volume);
        }

        public void SetSoundVolume(float soundVolume)
        {
            onSoundVolumeChange.Raise(this, soundVolume);
        }

        public void ToggleMusicMute(bool isMuted)
        {
            onMusicVolumeMute.Raise(this, isMuted);
        }

        public void ToggleSoundMute(bool isSoundMuted)
        {
            onSoundVolumeMute.Raise(this, isSoundMuted);
        }
    }
}