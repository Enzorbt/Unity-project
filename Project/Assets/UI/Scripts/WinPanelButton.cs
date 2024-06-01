using Supinfo.Project.Scripts.Events;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// This class manages the buttons in the win panel of the game.
/// </summary>
public class WinPanelButton : MonoBehaviour
{
    /// <summary>
    /// The event that is triggered when the player clicks on the "Restart" button in the win panel.
    /// </summary>
    [SerializeField] private GameEvent onRestart;

    /// <summary>
    /// The event that is triggered when the player clicks on the "Main Menu" button in the win panel.
    /// </summary>
    [SerializeField] private GameEvent onReturnToMainMenu;

    /// <summary>
    /// This method is called when the script instance is being loaded.
    /// It adds listeners to the "Restart" and "Main Menu" buttons in the win panel.
    /// </summary>
    private void Start()
    {
        // Get the "Restart" button in the win panel and add a listener to it
        Button restartButton = transform.Find("RestartButton").GetComponent<Button>();
        restartButton.onClick.AddListener(() => RestartGame());

        // Get the "Main Menu" button in the win panel and add a listener to it
        Button mainMenuButton = transform.Find("MenuButton").GetComponent<Button>();
        mainMenuButton.onClick.AddListener(() => ReturnToMainMenu());
    }

    /// <summary>
    /// This method is called when the player clicks on the "Restart" button in the win panel.
    /// It triggers the onRestart event.
    /// </summary>
    private void RestartGame()
    {
        // Trigger the onRestart event
        onRestart.Raise(this, null);
    }

    /// <summary>
    /// This method is called when the player clicks on the "Main Menu" button in the win panel.
    /// It triggers the onReturnToMainMenu event.
    /// </summary>
    private void ReturnToMainMenu()
    {
        // Trigger the onReturnToMainMenu event
        onReturnToMainMenu.Raise(this, null);
    }
}
