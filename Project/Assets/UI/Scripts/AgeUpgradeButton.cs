using Supinfo.Project.Scripts.Events;
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
        
        private void Awake()
        {
            image = transform.GetComponentInChildren<Image>();
            image.sprite = pressButtonImg;
            EnableButton(false);
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
        }

        public void EnableButton(bool value)
        {
            var button = transform.GetComponentInChildren<UnityEngine.UI.Button>();
            if (button is null) return;
            button.enabled = value;
        }
    }
}