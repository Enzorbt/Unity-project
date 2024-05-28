using Supinfo.Project.Scripts.Managers;
using UnityEngine;


namespace Supinfo.Project.UI.Scripts
{
    public class ButtonSound : MonoBehaviour
    {
        public AudioClip buttonClip;
        
        public void PlayButtonSound()
        {
            if (ButtonSoundManager.Instance != null && buttonClip != null)
            {
                ButtonSoundManager.Instance.PlayButtonSound(buttonClip);
            }
            else
            {
                Debug.LogWarning("ButtonSoundManager instance or buttonClip is missing!");
            }
        }
    }
}