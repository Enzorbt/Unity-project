using System;
using System.Collections;
using Supinfo.Project.Scripts.Events;
using Supinfo.Project.Scripts.ScriptableObjects.Capacity;
using UnityEngine;
using UnityEngine.UI;
using Supinfo.Project.Scripts.Managers;

namespace Supinfo.Project.Capacity.Scripts
{
    public class SpecialCapacityButtonUI : MonoBehaviour
    {
        private float _xpMax;

        [SerializeField] private GameEvent onClick;
        [SerializeField] private GameEvent onXpChange;
        [SerializeField] private CapacitySo _capacitySo;
        [Range(0, 1)] [SerializeField] private float cost;

        private Image _image;
        private float _xpRatio;
        private bool _canUse = true;
        
        [SerializeField] private Image cooldownImage;

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

        public void OnClick()
        {
            if (_canUse && _xpRatio >= cost)
            {
                StartCoroutine(UseCapacityWithCooldown());
            }
        }

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

        public void OnXpRatioChange(Component sender, object data)
        {
            if (data is not float xpRatio) return;

            _xpRatio = xpRatio;

            SetActiveButton(_xpRatio >= cost && _canUse);
        }

        private void SetActiveButton(bool state)
        {
            gameObject.GetComponentInChildren<UnityEngine.UI.Button>().enabled = state;
        }

        public void OnXpMaxChange(Component sender, object data)
        {
            if (data is not float xpMax) return;

            _xpMax = xpMax;
        }

        public void OnAgeUpgrade(Component sender, object data)
        {
            StartCoroutine(ChangeSprite());
        }

        private IEnumerator ChangeSprite()
        {
            yield return new WaitForSeconds(1f);
            if (_image == null) yield break;
            _image.sprite = _capacitySo.Sprite;
        }

        public void OnGameSpeedChange(Component sender, object data)
        {
            if (data is not GameSpeed gameSpeed) return;

            SetActiveButton(gameSpeed == GameSpeed.Stop ? false : _xpRatio >= cost && _canUse);
        }

        public void OnSpecialCapacityStatusChange(Component sender, object data)
        {
            if (data is not bool status) return;
            _canUse = status;
            SetActiveButton(_xpRatio >= cost && _canUse);
        }
    }
}
