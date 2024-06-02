using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;
using System.Collections.Generic;
using TMPro;

namespace Supinfo.Project.Scripts.Managers
{
    /// <summary>
    /// Manages the language settings of the game.
    /// </summary>
    public class LanguageManager : MonoBehaviour
    {
        /// <summary>
        /// Dropdown for selecting language.
        /// </summary>
        public TMP_Dropdown languageDropdown;
        
        /// <summary>
        /// Flag to indicate if the locale is being changed.
        /// </summary>
        private bool isChangingLocale = false;
        
        public static event Action<int> onLanguageChanged;

        /// <summary>
        /// Enum representing available languages.
        /// </summary>
        public enum Language
        {
            English,  // English language.
            French,   // French language.
            Spanish,  // Spanish language.
            German    // German language.
        }
        
        private void Start()
        {
            InitializeDropdown();  // Initialize the language dropdown.

            int currentLanguageIndex = PlayerPrefs.GetInt("LocaleKey", 0);  // Get saved language index or default to 0.
            languageDropdown.value = currentLanguageIndex;  // Set dropdown to current language.

            ChangeLocale(currentLanguageIndex);  // Change the locale to the current language.

            languageDropdown.onValueChanged.AddListener(OnLanguageChanged);  // Add listener for language change.
            onLanguageChanged?.Invoke(currentLanguageIndex);
            
        }

        /// <summary>
        /// Initializes the language dropdown with available languages.
        /// </summary>
        private void InitializeDropdown()
        {
            List<string> options = new List<string>();

            foreach (Language language in System.Enum.GetValues(typeof(Language)))
            {
                options.Add(language.ToString());  // Add each language to the dropdown options.
            }

            languageDropdown.ClearOptions();  // Clear existing options.
            languageDropdown.AddOptions(options);  // Add new options.
        }

        /// <summary>
        /// Callback for when the language is changed in the dropdown.
        /// </summary>
        /// <param name="index">The index of the selected language.</param>
        private void OnLanguageChanged(int index)
        {
            ChangeLocale(index);  // Change the locale based on the selected index.
            onLanguageChanged?.Invoke(index);
        }

        /// <summary>
        /// Changes the locale to the specified language ID.
        /// </summary>
        /// <param name="localeID">The ID of the locale to change to.</param>
        public void ChangeLocale(int localeID)
        {
            if (isChangingLocale)
                return;  // Return if already changing locale.

            StartCoroutine(SetLocale(localeID));  // Start coroutine to set locale.
        }

        /// <summary>
        /// Coroutine to set the locale.
        /// </summary>
        /// <param name="localeID">The ID of the locale to change to.</param>
        /// <returns>IEnumerator for the coroutine.</returns>
        private IEnumerator SetLocale(int localeID)
        {
            isChangingLocale = true;  // Set flag to indicate locale is being changed.
            yield return LocalizationSettings.InitializationOperation;  // Wait for localization settings to initialize.

            if (localeID >= 0 && localeID < LocalizationSettings.AvailableLocales.Locales.Count)
            {
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];  // Set the selected locale.
                PlayerPrefs.SetInt("LocaleKey", localeID);  // Save the selected locale.
            }
            isChangingLocale = false;  // Reset flag after changing locale.
        }
    }
}
