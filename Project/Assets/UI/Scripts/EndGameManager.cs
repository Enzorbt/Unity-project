using System;
using System.Collections;
using System.Collections.Generic;
using Supinfo.Project.Scripts.Events;
using Supinfo.Project.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
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
        /// The variable that holds the texts.
        /// </summary>
        private List<String> _endTexts;

        /// <summary>
        /// The gameSpeedEvent that changes the speed of the game
        /// </summary>
        [SerializeField] private GameEvent onGameSpeedChange;

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
            _endTexts = new List<string>();

            LanguageManager.onLanguageChanged += UpdateEndTexts; // Subscribe to the language change event
        }

        private void OnDestroy()
        {
            LanguageManager.onLanguageChanged -= UpdateEndTexts;  // Unsubscribe from language change event
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
                    EnableWinPanel(_originalBannerColor, _endTexts[0]);
                    break;
                case BaseIdentifier.BaseAllies:
                    EnableWinPanel(Color.red, _endTexts[1]);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            onGameSpeedChange.Raise(this, GameSpeed.Stop);
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
        
        private void UpdateEndTexts(int localeID)
        {
            StartCoroutine(UpdateEndFromLocalization());
        }

        private IEnumerator UpdateEndFromLocalization()
        {
            yield return LocalizationSettings.InitializationOperation;
            _endTexts.Clear();
            var tableName = "EndText";
            var table = LocalizationSettings.StringDatabase.GetTable(tableName);
            
            _endTexts.Add(table.GetEntry("Victory")?.GetLocalizedString());
            _endTexts.Add(table.GetEntry("Defeat")?.GetLocalizedString());
        }
    }
}
