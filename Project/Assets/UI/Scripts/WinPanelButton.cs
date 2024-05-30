using Supinfo.Project.Scripts.Events;
using UnityEngine;


namespace Supinfo.Project.UI.Scripts
{
    using UnityEngine.UI;
    

    public class WinPanelButton : MonoBehaviour
    {
        [SerializeField] private GameEvent onRestart;
        [SerializeField] private GameEvent onReturnToMainMenu;

        private void Start()
        {
            Button restartButton = transform.Find("RestartButton").GetComponent<Button>();
            restartButton.onClick.AddListener(() => RestartGame());

            Button mainMenuButton = transform.Find("MenuButton").GetComponent<Button>();
            mainMenuButton.onClick.AddListener(() => ReturnToMainMenu());
        }

        private void RestartGame()
        {
            onRestart.Raise(this, null);
        }

        private void ReturnToMainMenu()
        {
            onReturnToMainMenu.Raise(this, null);
        }
    }
}