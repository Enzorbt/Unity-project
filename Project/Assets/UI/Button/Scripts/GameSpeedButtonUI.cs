using Supinfo.Project.Scripts.Events;
using Supinfo.Project.Scripts.Managers;
using UnityEngine;

namespace Supinfo.Project.UI.Button.Scripts
{
    public class GameSpeedButtonUI : MonoBehaviour
    {
        [SerializeField] private GameSpeed gameSpeed;

        [SerializeField] private GameEvent onGameSpeedChange;

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
        }
    }
}