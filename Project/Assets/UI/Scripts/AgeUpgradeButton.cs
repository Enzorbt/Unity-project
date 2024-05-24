using System.Collections.Generic;
using Supinfo.Project.Scripts.Events;
using Supinfo.Project.Scripts.Managers;
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

        private bool _canEvolve;
        
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
            if (data is not bool value) return;
            _canEvolve = value;
            EnableButton(_canEvolve);
        }
        public void OnClick()
        {
            onAgeUpgrade.Raise(this, 2);
            EnableButton(false);

            // Incrémenter l'index de l'âge actuel et vérifier les limites
            if (currentAgeIndex < ages.Count - 1)
            {
                currentAgeIndex++;
                UpdateAgeText();
            }
        }

        private void EnableButton(bool value)
        {
            var button = transform.GetComponentInChildren<UnityEngine.UI.Button>();
            if (button is null) return;
            button.enabled = value;
            image.sprite = value ? buttonImg : pressButtonImg;
        }
        
        private void UpdateAgeText()
        {
            if (ageText is null) return;
            if (ageText != null && currentAgeIndex < ages.Count)
            {
                ageText.text = ages[currentAgeIndex];
            }
        }
        
        public void OnGameSpeedChange(Component sender, object data)
        {
            if (data is not GameSpeed gameSpeed) return;
            
            EnableButton(gameSpeed == GameSpeed.Stop ? false : _canEvolve);
        }
    }
}