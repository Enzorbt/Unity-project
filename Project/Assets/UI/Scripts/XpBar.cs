using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Supinfo.Project.UI.Scripts
{
    public class XpBar : MonoBehaviour
    {
        /// <summary>
        /// The UI Slider component that represents the health bar.
        /// </summary>
        private Slider _xpBarSlider;
        private float _maxXp;
        private TextMeshProUGUI _xpText;
        
        /// <summary>
        /// Awake is called before the first frame update.
        /// It fetch the Slider component of the Children object.
        /// </summary>
        private void Awake()
        {
            _xpBarSlider = GetComponentInChildren<Slider>();
            _xpText = GetComponentInChildren<TextMeshProUGUI>();
        }

        /// <summary>
        /// Sets the current health ratio value on the health bar.
        /// </summary>
        public void OnXpRatioChange(Component sender, object data)
        {
            if (data is not float ratio) return;
            _xpBarSlider.value = ratio;
            var xpCount = ratio * _maxXp;
            _xpText.text = (int)xpCount + "/" + _maxXp;
        }
        
        public void OnXpMaxChange(Component sender, object data)
        {
            if (data is not float max) return;
            _maxXp = max;
            _xpText.text = 0 + "/" + _maxXp;
        }
        
    }
}