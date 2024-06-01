using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Supinfo.Project.Castle.Scripts
{
    /// <summary>
    /// Manages the health bar of the base.
    /// </summary>
    public class BaseHealthBar : MonoBehaviour
    {
        /// <summary>
        /// The UI Slider component that represents the health bar.
        /// </summary>
        private Slider _healthBarSlider;

        /// <summary>
        /// The Image component of the health bar fill area.
        /// </summary>
        private Image _fillImage;

        /// <summary>
        /// The target health value the slider should move towards.
        /// </summary>
        private float _targetHealthValue;

        /// <summary>
        /// The speed at which the health bar moves to the target value.
        /// </summary>
        public float interpolationSpeed = 5f;

        /// <summary>
        /// The color of the health bar when at full health.
        /// </summary>
        public Color fullHealthColor = Color.green;

        /// <summary>
        /// The color of the health bar when at zero health.
        /// </summary>
        public Color zeroHealthColor = Color.red;

        /// <summary>
        /// Awake is called before the first frame update.
        /// It fetch the Slider component of the Children object.
        /// </summary>
        private void Awake()
        {
            _healthBarSlider = GetComponentInChildren<Slider>();
            _fillImage = _healthBarSlider.fillRect.GetComponentInChildren<Image>();
        }

        /// <summary>
        /// Sets the current health ratio value on the health bar.
        /// </summary>
        /// <param name="value">The current value to set on the health bar.</param>
        public void SetHealthSliderValue(Component sender, object data)
        {
            if (data is not float value) return;

            // Stop any ongoing interpolation
            StopAllCoroutines();
            
            // Start a new interpolation to the new target value
            StartCoroutine(AnimateHealthBar(_healthBarSlider.value, value));
        }

        /// <summary>
        /// Coroutine to smoothly animate the health bar value.
        /// </summary>
        /// <param name="startValue">The starting value of the animation.</param>
        /// <param name="endValue">The target value of the animation.</param>
        /// <returns></returns>
        private IEnumerator AnimateHealthBar(float startValue, float endValue)
        {
            float elapsedTime = 0f;

            while (elapsedTime < 1f)
            {
                elapsedTime += Time.deltaTime * interpolationSpeed;
                float currentValue = Mathf.Lerp(startValue, endValue, elapsedTime);
                _healthBarSlider.value = currentValue;
                UpdateHealthBarColor(currentValue);
                yield return null;
            }

            _healthBarSlider.value = endValue;
            UpdateHealthBarColor(endValue);
        }

        /// <summary>
        /// Updates the color of the health bar based on the current health value.
        /// </summary>
        /// <param name="healthValue">The current health value of the health bar.</param>
        private void UpdateHealthBarColor(float healthValue)
        {
            float normalizedValue = healthValue / _healthBarSlider.maxValue;
            _fillImage.color = Color.Lerp(zeroHealthColor, fullHealthColor, normalizedValue);
        }
    }
}
