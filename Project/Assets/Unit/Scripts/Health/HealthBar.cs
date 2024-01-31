using UnityEngine;
using UnityEngine.UI;

namespace Supinfo.Project.Unit.Scripts.Health
{
    /// <summary>
    /// The HealthBar class manages the UI representation of a unit's health.
    /// It interacts with the UnitHealth class to update the health bar based on the unit's current health.
    /// </summary>
    public class HealthBar : MonoBehaviour
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
        public void SetHealthSliderValue(float value)
        {
            _healthBarSlider.value = value;
        }
    }
}
