using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class manages the main menu of the game.
/// </summary>
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// This method is called when the player clicks on the "Play" button in the main menu.
    /// It loads the first level of the game asynchronously.
    /// </summary>
    public void PlayGame()
    {
        // Load the first level of the game asynchronously
        SceneManager.LoadSceneAsync(1);
    }

    /// <summary>
    /// This method is called when the player clicks on the "Quit" button in the main menu.
    /// It quits the application.
    /// </summary>
    public void QuitGame()
    {
        // Quit the application
        Application.Quit();
    }
}
