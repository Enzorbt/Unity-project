using UnityEngine;
using UnityEngine.UI;

namespace Supinfo.Project.Castle.Scripts
{
    public class BaseHealthBar: MonoBehaviour
    {
        /// <summary>
        /// The UI Slider component that represents the health bar.
        /// </summary>
        private Slider _healthBarSlider;
        
        /// <summary>
        /// Awake is called before the first frame update.
        /// It fetch the Slider component of the Children object.
        /// </summary>
        private void Awake()
        {
            _healthBarSlider = GetComponentInChildren<Slider>();
        }
        
        /// <summary>
        /// Sets the current health ratio value on the health bar.
        /// </summary>
        /// <param name="value">The current value to set on the health bar.</param>
        public void SetHealthSliderValue(Component sender, object data)
        {
            if (data is not float value) return;
            _healthBarSlider.value = value;
        }
    }
}