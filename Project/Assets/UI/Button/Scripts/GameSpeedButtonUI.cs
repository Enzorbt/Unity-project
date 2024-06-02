using Supinfo.Project.Scripts.Events;
using Supinfo.Project.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Supinfo.Project.UI.Button.Scripts
{
    /// <summary>
    /// The GameSpeedButtonUI class manages the UI button for adjusting the game speed.
    /// </summary>
    public class GameSpeedButtonUI : MonoBehaviour
    {
        /// <summary>
        /// The game speed associated with this button.
        /// </summary>
        [SerializeField] private GameSpeed gameSpeed;
        
        /// <summary>
        /// Event triggered when the game speed changes.
        /// </summary>
        [SerializeField] private GameEvent onGameSpeedChange;
        
        /// <summary>
        /// The image component of the button.
        /// </summary>
        private Image _image;

        private void Awake()
        {
            _image = GetComponentInChildren<Image>();
            if (gameSpeed == GameSpeed.Play)
            {
                SetActiveButton(false);
            }
        }
        
        public void OnClick()
        {
            onGameSpeedChange.Raise(this, gameSpeed);
            SetActiveButton(false);
        }

        /// <summary>
        /// Handles the game speed change event.
        /// </summary>
        /// <param name="sender">The component that triggered the event.</param>
        /// <param name="data">The new game speed.</param>
        public void OnGameSpeedChange(Component sender, object data)
        {
            if (data is not GameSpeed currentGameSpeed) return;

            SetActiveButton(gameSpeed != currentGameSpeed);
        }

        /// <summary>
        /// Sets the active state of the button and its appearance.
        /// </summary>
        /// <param name="state">The desired state.</param>
        private void SetActiveButton(bool state)
        {
            gameObject.GetComponentInChildren<UnityEngine.UI.Button>().enabled = state;
            _image.color = state ? Color.white : Color.grey;
        }
    }
}