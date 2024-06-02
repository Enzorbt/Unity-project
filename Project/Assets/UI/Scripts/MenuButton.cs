using UnityEngine;
using UnityEngine.SceneManagement;

namespace Supinfo.Project.UI.Scripts
{
    /// <summary>
    /// The return to main menu functionality class.
    /// </summary>
    public class MenuButton : MonoBehaviour
    {
        /// <summary>
        /// The return to main menu function.
        /// </summary>
        public void ReturnToMainMenu()
        {
            // load scene 0 = Main Menu
            SceneManager.LoadSceneAsync(0);
        }
    }
}