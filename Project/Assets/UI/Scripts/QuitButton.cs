using UnityEngine;

namespace Supinfo.Project.UI.Scripts
{
    /// <summary>
    /// This class manages the quit game functionality of the game.
    /// </summary>
    public class QuitButton : MonoBehaviour
    {
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
}