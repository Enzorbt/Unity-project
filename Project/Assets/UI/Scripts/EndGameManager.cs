using System;
using Supinfo.Project.Scripts.Events;
using Supinfo.Project.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Supinfo.Project.UI.Scripts
{
    /// <summary>
    /// This class manages the end game screen, displaying victory or defeat.
    /// </summary>
    public class EndGameManager : MonoBehaviour
    {
        /// <summary>
        /// The GameObject representing the win/lose panel.
        /// </summary>
        [SerializeField] private GameObject winPanel;

        /// <summary>
        /// The GameEvent that is triggered when the game is restarted.
        /// </summary>
        [SerializeField] private GameEvent onRestart;

        /// <summary>
        /// The GameEvent that is triggered when the main menu is returned to.
        /// </summary>
        [SerializeField] private GameEvent onReturnToMainMenu;

        /// <summary>
        /// The TextMeshProUGUI component that displays the victory/defeat text.
        /// </summary>
        private TextMeshProUGUI _text;

        /// <summary>
        /// The Image component that displays the victory/defeat banner.
        /// </summary>
        private Image _bannerImage;

        /// <summary>
        /// The original color of the victory/defeat banner.
        /// </summary>
        private Color _originalBannerColor;

        /// <summary>
        /// This method is called when the script instance is being loaded.
        /// It initializes the text and image components.
        /// </summary>
        private void Awake()
        {
            GameObject banner = winPanel.transform.Find("Banner").gameObject;
            _text = banner.GetComponentInChildren<TextMeshProUGUI>();
            _bannerImage = banner.GetComponentInChildren<Image>();
            _originalBannerColor = _bannerImage.color;
        }

        /// <summary>
        /// This method is called when the castle is destroyed.
        /// It displays the victory/defeat screen depending on the destroyed castle.
        /// </summary>
        /// <param name="sender">The component that sent the event.</param>
        /// <param name="data">The data associated with the event.</param>
        public void OnCastleDeath(Component sender, object data)
        {
            if (data is not BaseIdentifier baseId) return;
            winPanel.SetActive(true);
            switch (baseId)
            {
                case BaseIdentifier.BaseEnemies:
                    EnableWinPanel(_originalBannerColor, "Victory");
                    break;
                case BaseIdentifier.BaseAllies:
                    EnableWinPanel(Color.red, "Defeat");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// This method enables the victory/defeat screen with the specified color and title.
        /// </summary>
        /// <param name="color">The color of the victory/defeat banner.</param>
        /// <param name="title">The text to display on the victory/defeat screen.</param>
        private void EnableWinPanel(Color color, string title)
        {
            _text.text = title;
            _bannerImage.color = color;
        }

        /// <summary>
        /// This method restarts the current scene.
        /// </summary>
        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        /// <summary>
        /// This method loads the main menu scene.
        /// </summary>
        public void ReturnToMainMenu()
        {
            SceneManager.LoadScene("Main Menu");
        }
    }
}
