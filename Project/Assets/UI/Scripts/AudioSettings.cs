using Supinfo.Project.Scripts.Events; 
using UnityEngine;
using UnityEngine.UI;

namespace Supinfo.Project.UI.Scripts
{
    /// <summary>
    /// Handles the audio settings in the UI.
    /// </summary>
    public class AudioSettings : MonoBehaviour
    {
        /// <summary>
        /// Slider for controlling music volume.
        /// </summary>
        public Slider musicVolumeSlider;

        /// <summary>
        /// Toggle for muting/unmuting music.
        /// </summary>
        public Toggle musicMuteToggle;

        /// <summary>
        /// Slider for controlling sound effects volume.
        /// </summary>
        public Slider soundVolumeSlider;

        /// <summary>
        /// Toggle for muting/unmuting sound effects.
        /// </summary>
        public Toggle soundMuteToggle;

        /// <summary>
        /// Event raised when music volume changes.
        /// </summary>
        [SerializeField] private GameEvent onMusicVolumeChange;

        /// <summary>
        /// Event raised when music mute state changes.
        /// </summary>
        [SerializeField] private GameEvent onMusicVolumeMute;

        /// <summary>
        /// Event raised when sound volume changes.
        /// </summary>
        [SerializeField] private GameEvent onSoundVolumeChange;

        /// <summary>
        /// Event raised when sound mute state changes.
        /// </summary>
        [SerializeField] private GameEvent onSoundVolumeMute;

        void Start()
        {
            musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume); // Add listener for music volume changes.
            musicMuteToggle.onValueChanged.AddListener(ToggleMusicMute); // Add listener for music mute toggle changes.
            musicVolumeSlider.value = PlayerPrefs.GetFloat("volume"); // Set music volume slider value from player prefs.
            musicMuteToggle.isOn = PlayerPrefs.GetInt("isMuted") > 0 ? true : false ; // Set music mute toggle state from player prefs.

            soundVolumeSlider.onValueChanged.AddListener(SetSoundVolume); // Add listener for sound volume changes.
            soundMuteToggle.onValueChanged.AddListener(ToggleSoundMute); // Add listener for sound mute toggle changes.
            soundVolumeSlider.value = PlayerPrefs.GetFloat("soundVolume"); // Set sound volume slider value from player prefs.
            soundMuteToggle.isOn = PlayerPrefs.GetInt("isSoundMuted") > 0 ? true : false ; // Set sound mute toggle state from player prefs.
        }
        
        /// <summary>
        /// Sets the music volume.
        /// </summary>
        /// <param name="volume">The new music volume.</param>
        public void SetMusicVolume(float volume)
        {
            onMusicVolumeChange.Raise(this, volume); // Raise music volume change event.
        }

        /// <summary>
        /// Sets the sound effects volume.
        /// </summary>
        /// <param name="soundVolume">The new sound effects volume.</param>
        public void SetSoundVolume(float soundVolume)
        {
            onSoundVolumeChange.Raise(this, soundVolume); // Raise sound volume change event.
        }

        /// <summary>
        /// Toggles the music mute state.
        /// </summary>
        /// <param name="isMuted">The new mute state.</param>
        public void ToggleMusicMute(bool isMuted)
        {
            onMusicVolumeMute.Raise(this, isMuted); // Raise music mute state change event.
        }

        /// <summary>
        /// Toggles the sound effects mute state.
        /// </summary>
        /// <param name="isSoundMuted">The new mute state.</param>
        public void ToggleSoundMute(bool isSoundMuted)
        {
            onSoundVolumeMute.Raise(this, isSoundMuted); // Raise sound mute state change event.
        }
    }
}
