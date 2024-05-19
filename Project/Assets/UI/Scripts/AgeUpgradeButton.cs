using System.Collections.Generic;
using Supinfo.Project.Scripts.Events;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Supinfo.Project.UI.Scripts
{
    public class AgeUpgradeButton : MonoBehaviour
    {

        private Image image;
        
        [SerializeField]
        private Sprite pressButtonImg;

        [SerializeField]
        private Sprite buttonImg;

        [SerializeField]
        private GameEvent onAgeUpgrade;
        
        
        private TextMeshProUGUI ageText;

        [SerializeField]
        private List<string> ages;
            
        private int currentAgeIndex = 0;
        
        private void Awake()
        {
            ageText = transform.GetComponentInChildren<TextMeshProUGUI>();
            image = transform.GetComponentInChildren<Image>();
            image.sprite = pressButtonImg;
            EnableButton(false);
            UpdateAgeText();
        }


        public void OnCanEvolve(Component sender, object data)
        {
            image.sprite = buttonImg;
            EnableButton(true);
        }
        public void OnClick()
        {
            onAgeUpgrade.Raise(this, 2);
            image.sprite = pressButtonImg;
            EnableButton(false);

            // Incrémenter l'index de l'âge actuel et vérifier les limites
            if (currentAgeIndex < ages.Count - 1)
            {
                currentAgeIndex++;
                UpdateAgeText();
            }
        }

        public void EnableButton(bool value)
        {
            var button = transform.GetComponentInChildren<UnityEngine.UI.Button>();
            if (button is null) return;
            button.enabled = value;
        }
        
        private void UpdateAgeText()
        {
            if (ageText is null) return;
            if (ageText != null && currentAgeIndex < ages.Count)
            {
                ageText.text = ages[currentAgeIndex];
            }
        }
    }
}