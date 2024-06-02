using System;
using System.Collections;
using System.Collections.Generic;
using Supinfo.Project.Scripts.Events;
using Supinfo.Project.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
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
        /// The TextMeshProUGUI component that displays the timer.
        /// </summary>
        [SerializeField] private TextMeshProUGUI timerText;

        /// <summary>
        /// The elapsed time since the game started.
        /// </summary>
        private float elapsedTime;

        /// <summary>
        /// Boolean to track if the game is still running.
        /// </summary>
        private bool isRunning;

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
            elapsedTime = 0f;
            isRunning = true;

            LanguageManager.onLanguageChanged += UpdateEndTexts; // Subscribe to the language change event
        }

        /// <summary>
        /// Called when the script instance is being destroyed.
        /// Unsubscribes from the language change event.
        /// </summary>
        private void OnDestroy()
        {
            LanguageManager.onLanguageChanged -= UpdateEndTexts;  // Unsubscribe from language change event
        }

        /// <summary>
        /// Called once per frame.
        /// Updates the elapsed time and displays it on the timer text if the game is running.
        /// </summary>
        private void Update()
        {
            if (isRunning)
            {
                elapsedTime += Time.deltaTime;
                timerText.text = FormatTime(elapsedTime);
            }
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
            isRunning = false;
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
        
        /// <summary>
        /// Starts the coroutine to update the end texts from localization settings.
        /// </summary>
        /// <param name="localeID">The locale ID indicating the language to update to.</param>
        private void UpdateEndTexts(int localeID)
        {
            StartCoroutine(UpdateEndFromLocalization());
        }

        /// <summary>
        /// Coroutine that updates the end texts (e.g., "Victory", "Defeat") from the localization database.
        /// </summary>
        /// <returns>Returns an IEnumerator for the coroutine.</returns>
        private IEnumerator UpdateEndFromLocalization()
        {
            yield return LocalizationSettings.InitializationOperation;
            _endTexts.Clear();
            var tableName = "EndText";
            var table = LocalizationSettings.StringDatabase.GetTable(tableName);
            
            _endTexts.Add(table.GetEntry("Victory")?.GetLocalizedString());
            _endTexts.Add(table.GetEntry("Defeat")?.GetLocalizedString());
        }

        /// <summary>
        /// Formats the elapsed time as a string.
        /// </summary>
        /// <param name="time">The elapsed time in seconds.</param>
        /// <returns>A formatted time string.</returns>
        private string FormatTime(float time)
        {
            int minutes = Mathf.FloorToInt(time / 60F);
            int seconds = Mathf.FloorToInt(time - minutes * 60);
            return string.Format("{0:0}:{1:00}", minutes, seconds);
        }
    }
}
