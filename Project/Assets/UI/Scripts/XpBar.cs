using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Supinfo.Project.UI.Scripts
{
    /// <summary>
    /// This class manages the XP bar in the game UI.
    /// </summary>
    public class XpBar : MonoBehaviour
    {
        /// <summary>
        /// The UI Slider component that represents the XP bar.
        /// </summary>
        private Slider _xpBarSlider;
        private float _maxXp;
        private TextMeshProUGUI _xpText;
        private float _ratio;

        /// <summary>
        /// The speed at which the XP bar moves to the target value.
        /// </summary>
        public float interpolationSpeed = 2f;

        /// <summary>
        /// Awake is called before the first frame update.
        /// It fetches the Slider component of the Children object.
        /// </summary>
        private void Awake()
        {
            _xpBarSlider = GetComponentInChildren<Slider>();
            
            _xpText = GetComponentInChildren<TextMeshProUGUI>();
        }

        /// <summary>
        /// Sets the current XP ratio value on the XP bar.
        /// </summary>
        public void OnXpRatioChange(Component sender, object data)
        {
            if (data is not float ratio) return;

            _ratio = ratio;

            // Stop any ongoing interpolation
            StopAllCoroutines();

            // Start a new interpolation to the new target value
            StartCoroutine(AnimateXpBar(_xpBarSlider.value, _ratio));
        }

        /// <summary>
        /// Sets the maximum XP value.
        /// </summary>
        public void OnXpMaxChange(Component sender, object data)
        {
            if (data is not float max) return;
            _maxXp = max;
        }

        /// <summary>
        /// Coroutine to smoothly animate the XP bar value.
        /// </summary>
        /// <param name="startValue">The starting value of the animation.</param>
        /// <param name="endValue">The target value of the animation.</param>
        /// <returns></returns>
        private IEnumerator AnimateXpBar(float startValue, float endValue)
        {
            float elapsedTime = 0f;

            while (elapsedTime < 1f)
            {
                elapsedTime += Time.deltaTime * interpolationSpeed;
                float currentValue = Mathf.Lerp(startValue, endValue, elapsedTime);
                _xpBarSlider.value = currentValue;
                UpdateXpText(currentValue);
                yield return null;
            }

            _xpBarSlider.value = endValue;
            UpdateXpText(endValue);
        }

        /// <summary>
        /// Updates the XP text based on the current XP value.
        /// </summary>
        /// <param name="xpRatio">The current XP value of the XP bar.</param>
        private void UpdateXpText(float xpRatio)
        {
            var xpCount = xpRatio * _maxXp;
            _xpText.text = (int)xpCount + "/" + _maxXp;
        }
    }
}
