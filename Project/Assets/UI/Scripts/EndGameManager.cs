using System;
using Supinfo.Project.Scripts.Events;
using Supinfo.Project.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Supinfo.Project.UI.Scripts
{
    public class EndGameManager : MonoBehaviour
    {
        [SerializeField] private GameObject winPanel;
        [SerializeField] private GameEvent onRestart;
        [SerializeField] private GameEvent onReturnToMainMenu;

        private TextMeshProUGUI _text;
        private Image _bannerImage;
        private Color _originalBannerColor;

        private void Awake()
        {
            GameObject banner = winPanel.transform.Find("Banner").gameObject;
            _text = banner.GetComponentInChildren<TextMeshProUGUI>();
            _bannerImage = banner.GetComponentInChildren<Image>();
            _originalBannerColor = _bannerImage.color;
        }

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

        private void EnableWinPanel(Color color, string title)
        {
            _text.text = title;
            _bannerImage.color = color;
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void ReturnToMainMenu()
        {
            SceneManager.LoadScene("Main Menu");
        }
    }
}
