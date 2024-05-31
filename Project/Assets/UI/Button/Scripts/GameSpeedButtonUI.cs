using Supinfo.Project.Scripts.Events;
using Supinfo.Project.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Supinfo.Project.UI.Button.Scripts
{
    public class GameSpeedButtonUI : MonoBehaviour
    {
        [SerializeField] private GameSpeed gameSpeed;
        [SerializeField] private GameEvent onGameSpeedChange;
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

        public void OnGameSpeedChange(Component sender, object data)
        {
            if (data is not GameSpeed currentGameSpeed) return;

            SetActiveButton(gameSpeed != currentGameSpeed);
        }

        private void SetActiveButton(bool state)
        {
            gameObject.GetComponentInChildren<UnityEngine.UI.Button>().enabled = state;
            
            _image.color = state ? Color.white : Color.grey;
        }
    }
}