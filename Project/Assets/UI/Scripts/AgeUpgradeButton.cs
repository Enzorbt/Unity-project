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
    /// Handles the behavior of an age upgrade button in the UI.
    /// </summary>
    public class AgeUpgradeButton : MonoBehaviour
    {
        /// <summary>
        /// The image component of the button.
        /// </summary>
        private Image image;

        /// <summary>
        /// The sprite displayed when the button is pressed.
        /// </summary>
        [SerializeField] private Sprite pressButtonImg;

        /// <summary>
        /// The sprite displayed when the button is not pressed.
        /// </summary>
        [SerializeField] private Sprite buttonImg;

        /// <summary>
        /// Event raised when the button is clicked to upgrade the age.
        /// </summary>
        [SerializeField] private GameEvent onAgeUpgrade;

        /// <summary>
        /// The text component displaying the current age.
        /// </summary>
        private TextMeshProUGUI ageText;

        /// <summary>
        /// The list of available ages.
        /// </summary>
        [SerializeField] private List<string> ages;

        /// <summary>
        /// The index of the current age in the list.
        /// </summary>
        private int currentAgeIndex = 0;

        /// <summary>
        /// Flag indicating whether the age can be upgraded.
        /// </summary>
        private bool _canEvolve;

        /// <summary>
        /// Initializes the button and text.
        /// </summary>
        private void Awake()
        {
            ageText = transform.GetComponentInChildren<TextMeshProUGUI>(); // Get the text component.
            image = transform.GetComponentInChildren<Image>(); // Get the image component.
            image.sprite = pressButtonImg; // Set the initial sprite.
            EnableButton(false); // Deactivate the button.

            LanguageManager.onLanguageChanged += UpdateAgeTexts; // Subscribe to the language change event
        }

        private void OnDestroy()
        {
            LanguageManager.onLanguageChanged -= UpdateAgeTexts;  // Unsubscribe from language change event
        }

        /// <summary>
        /// Handles the event indicating whether the age can be upgraded.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="data">The data associated with the event (boolean).</param>
        public void OnCanEvolve(Component sender, object data)
        {
            if (data is not bool value) return;
            _canEvolve = value;
            EnableButton(_canEvolve);
        }

        /// <summary>
        /// Handles the button click event.
        /// </summary>
        public void OnClick()
        {
            onAgeUpgrade.Raise(this, 2); // Raise the age upgrade event.

            EnableButton(false); // Deactivate the button.

            // Increment the current age index and check for limits.
            if (currentAgeIndex < ages.Count - 1)
            {
                currentAgeIndex++;
                UpdateAgeText();
            }
        }

        /// <summary>
        /// Enables or disables the button based on the specified value.
        /// </summary>
        /// <param name="value">The activation state of the button.</param>
        private void EnableButton(bool value)
        {
            if(currentAgeIndex <= 5)
            {
                var button = transform.GetComponentInChildren<UnityEngine.UI.Button>(); // Get the button component.
                if (button is null) return;
                button.enabled = value; // Set the button's activation state.
                image.sprite = value ? buttonImg : pressButtonImg; // Set the sprite based on the activation state.                
            }
        }

        /// <summary>
        /// Updates the text displaying the current age.
        /// </summary>
        private void UpdateAgeText()
        {
            if (ageText is null) return;
            ageText.text = ages[currentAgeIndex]; // Set the text to the current age.
        }

        private void UpdateAgeTexts(int localeID)
        {
            StartCoroutine(UpdateAgesFromLocalization());
        }

        /// <summary>
        /// Here the coroutine to update ageText with localization Tables.
        /// </summary>
        private IEnumerator UpdateAgesFromLocalization()
        {
            yield return LocalizationSettings.InitializationOperation;
            ages.Clear();
            for (var i = 0; i < 7; i++)
            {
                var tableName = $"AgeNames_{i + 1}";
                var table = LocalizationSettings.StringDatabase.GetTable(tableName);
                
                var localizedText = table.GetEntry(tableName)?.GetLocalizedString();
                if (localizedText is not null)
                {
                    ages.Add(localizedText);
                }
            }
            UpdateAgeText();
        }

        /// <summary>
        /// Handles changes in the game speed.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="data">The data associated with the event (GameSpeed).</param>
        public void OnGameSpeedChange(Component sender, object data)
        {
            if (data is not GameSpeed gameSpeed) return;

            EnableButton(gameSpeed == GameSpeed.Stop ? false : _canEvolve); // Enable or disable the button based on the game speed.
        }
    }
}
