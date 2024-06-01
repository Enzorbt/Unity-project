using System;
using System.Collections;
using System.Collections.Generic;
using Supinfo.Project.Scripts.Events;
using Supinfo.Project.Scripts.Managers;
using Supinfo.Project.Scripts.ScriptableObjects.Capacity;
using UnityEngine;
using UnityEngine.UI;
using Supinfo.Project.Scripts.Managers;

namespace Supinfo.Project.Capacity.Scripts
{
    public class SpecialCapacityButtonUI : MonoBehaviour
    {
        /// <summary>
        /// The maximum of xp (to display in the text).
        /// </summary>
        private float _xpMax;
        
        /// <summary>
        /// The game event to trigger when the button is clicked.
        /// </summary>
        [SerializeField] private GameEvent onClick;
        
        /// <summary>
        /// The game event to trigger to notify a xp change.
        /// </summary>
        [SerializeField] private GameEvent onXpChange;
        
        /// <summary>
        /// The capacity scriptable object with all its stats.
        /// </summary>
        [SerializeField]
        private CapacitySo _capacitySo;
        
        /// <summary>
        /// The cost of the capacity as a percentage of the xp total.
        /// </summary>
        [Range(0,1)]
        [SerializeField] private float cost;
        
        /// <summary>
        /// The image of the capacity (to change it for each age).
        /// </summary>
        private Image _image;

        /// <summary>
        /// The xp ratio of the user.
        /// </summary>
        private float _xpRatio;

        /// <summary>
        /// State of the usage of the capacity (if capacity is already playing, it is false.
        /// </summary>
        private bool _canUse = true;
        
        /// <summary>
        /// The cooldown image.
        /// </summary>
        [SerializeField] private Image cooldownImage;

        /// <summary>
        /// Called when the game object is instantiated.
        /// </summary>
        private void Awake()
        {
            SetActiveButton(false);
            _image = GetComponentsInChildren<Image>()[1];
            if (_image != null)
            {
                _image.sprite = _capacitySo.Sprite;
            }
            
            if (cooldownImage != null)
            {
                cooldownImage.type = Image.Type.Filled;
                cooldownImage.fillMethod = Image.FillMethod.Radial360;
                cooldownImage.fillOrigin = (int)Image.Origin360.Top;
                cooldownImage.fillAmount = 0f;
            }
        }

        /// <summary>
        /// Method to be called when the button is clicked.
        /// </summary>
        public void OnClick()
        {
            if (_canUse && _xpRatio >= cost)
            {
                StartCoroutine(UseCapacityWithCooldown());
            }
        }

        /// <summary>
        /// Coroutine function to use the capacity with a cooldown.
        /// </summary>
        /// <returns></returns>
        private IEnumerator UseCapacityWithCooldown()
        {
            _canUse = false;
            SetActiveButton(false);
            onClick.Raise(this, _capacitySo);
            onXpChange.Raise(this, -(_xpMax * cost));

            if (cooldownImage != null)
            {
                float elapsedTime = 0f;
                float cooldownDuration = _capacitySo.Cooldown;

                while (elapsedTime < cooldownDuration)
                {
                    elapsedTime += Time.deltaTime;
                    cooldownImage.fillAmount = elapsedTime / cooldownDuration;
                    yield return null;
                }
                cooldownImage.fillAmount = 0f;
            }

            yield return new WaitForSeconds(_capacitySo.Cooldown);

            _canUse = true;
            SetActiveButton(_xpRatio >= cost && _canUse);
        }

        /// <summary>
        /// Game event listener function called when the event onXpRatioChange is triggered (linked to a GameEventListener component).
        /// </summary>
        /// <param name="sender">The sender of the game event.</param>
        /// <param name="data">The data being transferred.</param>
        public void OnXpRatioChange(Component sender, object data)
        {
            if (data is not float xpRatio) return;

            _xpRatio = xpRatio;

            SetActiveButton(_xpRatio >= cost && _canUse);
        }

        /// <summary>
        /// Change the state of the button.
        /// </summary>
        /// <param name="state"></param>
        private void SetActiveButton(bool state)
        {
            gameObject.GetComponentInChildren<UnityEngine.UI.Button>().enabled = state;
        }

        /// <summary>
        /// Game event listener function called when the event onXPMaxChange is triggered (linked to a GameEventListener component).
        /// </summary>
        /// <param name="sender">The sender of the game event.</param>
        /// <param name="data">The data being transferred.</param>
        public void OnXpMaxChange(Component sender, object data)
        {
            if (data is not float xpMax) return;

            _xpMax = xpMax;
        }

        /// <summary>
        /// Game event listener function called when the event onAgeUpgrade is triggered (linked to a GameEventListener component).
        /// </summary>
        /// <param name="sender">The sender of the game event.</param>
        /// <param name="data">The data being transferred.</param>
        public void OnAgeUpgrade(Component sender, object data)
        {
            StartCoroutine(ChangeSprite());
        }

        /// <summary>
        /// Change the sprite inside the button (with cooldown to wait for the sprite change in the scriptable object).
        /// </summary>
        /// <returns></returns>
        private IEnumerator ChangeSprite()
        {
            yield return new WaitForSeconds(1f);
            if (_image is null) yield break;
            _image.sprite = _capacitySo.Sprite;
        }

        /// <summary>
        /// Game event listener function called when the event onGameSpeedChange is triggered (linked to a GameEventListener component).
        /// </summary>
        /// <param name="sender">The sender of the game event.</param>
        /// <param name="data">The data being transferred.</param>
        public void OnGameSpeedChange(Component sender, object data)
        {
            if (data is not GameSpeed gameSpeed) return;

            SetActiveButton(gameSpeed == GameSpeed.Stop ? false : _xpRatio >= cost && _canUse);
        }

        /// <summary>
        /// Game event listener function called when the event onSpecialCapacityStatusChange is triggered (linked to a GameEventListener component).
        /// </summary>
        /// <param name="sender">The sender of the game event.</param>
        /// <param name="data">The data being transferred.</param>
        public void OnSpecialCapacityStatusChange(Component sender, object data)
        {
            if(data is not bool status) return;
            _canUse = status;
            SetActiveButton(_xpRatio >= cost && _canUse);
        }
    }
}
