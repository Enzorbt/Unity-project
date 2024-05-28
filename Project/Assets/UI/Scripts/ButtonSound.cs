using Supinfo.Project.Scripts.Events;
using Supinfo.Project.Scripts.Managers;
using UnityEngine;


namespace Supinfo.Project.UI.Scripts
{
    public class ButtonSound : MonoBehaviour
    {
        [SerializeField]
        private AudioClip buttonClip;
        
        [SerializeField] private GameEvent onPlayButtonClickSound;
        
        public void PlayButtonSound()
        {
            onPlayButtonClickSound.Raise(this, buttonClip);
        }
    }
}