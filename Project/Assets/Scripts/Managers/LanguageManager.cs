using System.Collections;
using UnityEngine;
using UnityEngine.Localization.Settings;
using System.Collections.Generic;
using TMPro;

namespace Supinfo.Project.Scripts.Managers
{
    public class LanguageManager : MonoBehaviour
    {
        public TMP_Dropdown languageDropdown;
        private bool isChangingLocale = false;
        public enum Language
        {
            English,
            French,
            Spanish,
            German
        }
        private void Start()
        {
            InitializeDropdown();
            
            int currentLanguageIndex = PlayerPrefs.GetInt("LocaleKey", 0);
            languageDropdown.value = currentLanguageIndex;
            
            ChangeLocale(currentLanguageIndex);
            
            languageDropdown.onValueChanged.AddListener(OnLanguageChanged);
        }

        private void InitializeDropdown()
        {
            List<string> options = new List<string>();

            foreach (Language language in System.Enum.GetValues(typeof(Language)))
            {
                options.Add(language.ToString());
            }

            languageDropdown.ClearOptions();
            languageDropdown.AddOptions(options);
        }

        private void OnLanguageChanged(int index)
        {
            ChangeLocale(index);
        }

        public void ChangeLocale(int localeID)
        {
            if (isChangingLocale)
                return;

            StartCoroutine(SetLocale(localeID));
        }

        private IEnumerator SetLocale(int localeID)
        {
            isChangingLocale = true;
            yield return LocalizationSettings.InitializationOperation;

            if (localeID >= 0 && localeID < LocalizationSettings.AvailableLocales.Locales.Count)
            {
                LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
                PlayerPrefs.SetInt("LocaleKey", localeID);
            }
            isChangingLocale = false;
        }
    }
}