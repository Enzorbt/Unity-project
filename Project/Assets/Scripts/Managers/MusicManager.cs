using Supinfo.Project.Scripts.Events;
using Supinfo.Project.Scripts.ScriptableObjects.Music;
using UnityEngine;
using UnityEngine.Serialization;

namespace Supinfo.Project.Scripts.Managers
{
    /// <summary>
    /// Enum representing different scene names.
    /// </summary>
    public enum SceneName
    {
        mainMenu,  // Main menu scene.
        game       // Game scene.
    }

    /// <summary>
    /// Manages the music and sound settings in the game.
    /// </summary>
    public class MusicManager : MonoBehaviour
    {
        /// <summary>
        /// Scriptable object containing music list.
        /// </summary>
        [FormerlySerializedAs("musicList")] public MusicListSo musicListSo;

        /// <summary>
        /// AudioSource for playing music.
        /// </summary>
        [SerializeField] private AudioSource audioSourceMusic;
        
        /// <summary>
        /// AudioSource for playing sound effects.
        /// </summary>
        [SerializeField] private AudioSource audioSourceSound;
        
        /// <summary>
        /// Original main menu music clip.
        /// </summary>
        private AudioClip originalMainMenuMusic;
        
        /// <summary>
        /// Flag to check if Easter egg music is playing.
        /// </summary>
        private bool isEasterEggMusicPlaying = false;

        /// <summary>
        /// Current scene name.
        /// </summary>
        [SerializeField]
        private SceneName sceneName;
        

        private void Awake()
        {
            originalMainMenuMusic = musicListSo.MenuMusic;  // Set the original main menu music.
            PlayMusicForCurrentAge();  // Play music for the current age.
            audioSourceMusic.volume = PlayerPrefs.GetFloat("volume");  // Set music volume from player preferences.
            audioSourceMusic.mute = PlayerPrefs.GetInt("isMuted") > 0;  // Set music mute status from player preferences.
            audioSourceSound.volume = PlayerPrefs.GetFloat("soundVolume");  // Set sound volume from player preferences.
            audioSourceSound.mute = PlayerPrefs.GetInt("isSoundMuted") > 0;  // Set sound mute status from player preferences.
        }

        /// <summary>
        /// Upgrades the age and changes the music accordingly.
        /// </summary>
        /// <param name="sender">The component that sent the event.</param>
        /// <param name="data">The data associated with the event.</param>
        public void UpgradeAge(Component sender, object data)
        {
            musicListSo.UpgradeAge();  // Upgrade the age in the music list.
            PlayMusicForCurrentAge();  // Play music for the new age.
        }

        /// <summary>
        /// Plays the music for the current age.
        /// </summary>
        private void PlayMusicForCurrentAge()
        {
            switch (sceneName)
            {
                case SceneName.mainMenu:
                    audioSourceMusic.clip = musicListSo.MenuMusic;  // Set main menu music.
                    break;
                case SceneName.game:
                    audioSourceMusic.clip = musicListSo.GetCurrentAgeMusic();  // Set game music for current age.
                    break;
            }
            audioSourceMusic.Play();  // Play the selected music.
        }

        /// <summary>
        /// Handles the change in music volume.
        /// </summary>
        /// <param name="sender">The component that sent the event.</param>
        /// <param name="data">The data associated with the event.</param>
        public void OnMusicVolumeChange(Component sender, object data)
        {
            if (data is not float volume) return;
            audioSourceMusic.volume = volume;  // Set the new volume.
            PlayerPrefs.SetFloat("volume", volume);  // Save the new volume to player preferences.
        }

        /// <summary>
        /// Handles the mute/unmute of music.
        /// </summary>
        /// <param name="sender">The component that sent the event.</param>
        /// <param name="data">The data associated with the event.</param>
        public void OnMusicVolumeMute(Component sender, object data)
        {
            if (data is not bool isMuted) return;
            audioSourceMusic.mute = isMuted;  // Mute or unmute the music.
            PlayerPrefs.SetInt("isMuted", isMuted ? 1 : 0);  // Save the mute status to player preferences.
        }

        /// <summary>
        /// Handles the change in sound volume.
        /// </summary>
        /// <param name="sender">The component that sent the event.</param>
        /// <param name="data">The data associated with the event.</param>
        public void OnSoundVolumeChange(Component sender, object data)
        {
            if (data is not float soundVolume) return;
            audioSourceSound.volume = soundVolume;  // Set the new sound volume.
            PlayerPrefs.SetFloat("soundVolume", soundVolume);  // Save the new sound volume to player preferences.
        }
        
        /// <summary>
        /// Handles the mute/unmute of sound effects.
        /// </summary>
        /// <param name="sender">The component that sent the event.</param>
        /// <param name="data">The data associated with the event.</param>
        public void OnSoundVolumeMute(Component sender, object data)
        {
            if (data is not bool isSoundMuted) return;
            audioSourceSound.mute = isSoundMuted;  // Mute or unmute the sound effects.
            PlayerPrefs.SetInt("isSoundMuted", isSoundMuted ? 1 : 0);  // Save the mute status to player preferences.
        }
        
        /// <summary>
        /// Plays a sound when a button is clicked.
        /// </summary>
        /// <param name="sender">The component that sent the event.</param>
        /// <param name="data">The data associated with the event.</param>
        public void OnPlaySound(Component sender, object data)
        {
            if (data is AudioClip clip && audioSourceSound != null)
            {
                audioSourceSound.PlayOneShot(clip);
            }
        }
        
        /// <summary>
        /// Toggles the main menu music between the original and an Easter egg music.
        /// </summary>
        /// <param name="easterEggMusic">The Easter egg music clip.</param>
        public void ToggleMainMenuMusic(AudioClip easterEggMusic)
        {
            if (sceneName == SceneName.mainMenu && audioSourceMusic != null)
            {
                if (isEasterEggMusicPlaying)
                {
                    audioSourceMusic.clip = originalMainMenuMusic;  // Switch to original main menu music.
                }
                else
                {
                    audioSourceMusic.clip = easterEggMusic;  // Switch to Easter egg music.
                }
                isEasterEggMusicPlaying = !isEasterEggMusicPlaying;  // Toggle the flag.
                audioSourceMusic.Play();  // Play the selected music.
            }
        }
    }
}
