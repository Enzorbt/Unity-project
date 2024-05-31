using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using ScriptableObjects.Unit;
using Supinfo.Project.Scripts.Events;
using Supinfo.Project.Scripts.Managers;

namespace Supinfo.Project.UI.Button.Scripts
{
    public class SpawnButtonUI : MonoBehaviour
    {
        [SerializeField]
        private UnitStatSo unitStatSo;

        private bool _isActive;
        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                EnableButton(_goldCount >= unitStatSo.Price && _queueStatus && _canSpawn);
            }
        }

        private Image _image;

        [SerializeField] private GameEvent onSpawnUnit;
        [SerializeField] private GameEvent onGoldChange;

        private bool _canSpawn = true;
        private UnityEngine.UI.Button _button;
        private float _goldCount;
        private bool _queueStatus = true;
        
        [SerializeField] private Image cooldownImage;

        private void Awake()
        {
            _button = transform.GetComponentInChildren<UnityEngine.UI.Button>();
            IsActive = true;
            _image = GetComponentsInChildren<Image>()[1];
            
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
            if (_canSpawn && IsActive)
            {
                StartCoroutine(SpawnWithCooldown());
            }
        }

        private IEnumerator SpawnWithCooldown()
        {
            _canSpawn = false;
            EnableButton(_goldCount >= unitStatSo.Price && _queueStatus && _canSpawn);
            onSpawnUnit.Raise(this, unitStatSo);
            onGoldChange.Raise(this, -unitStatSo.Price);

            if (cooldownImage != null)
            {
                float elapsedTime = 0f;
                float cooldownDuration = unitStatSo.BuildTime;

                while (elapsedTime < cooldownDuration)
                {
                    elapsedTime += Time.deltaTime;
                    cooldownImage.fillAmount = elapsedTime / cooldownDuration;
                    yield return null;
                }

                cooldownImage.fillAmount = 0f;
            }

            yield return new WaitForSeconds(unitStatSo.BuildTime);

            _canSpawn = true;
            EnableButton(_goldCount >= unitStatSo.Price && _queueStatus && _canSpawn);
        }

        private void EnableButton(bool value)
        {
            if (_button == null) return;
            if (!IsActive) return;
            _button.enabled = value;
        }

        public void OnSpawnQueueStatusChange(Component sender, object data)
        {
            if (data is not bool status) return;
            _queueStatus = status;
            EnableButton(_goldCount >= unitStatSo.Price && _queueStatus && _canSpawn);
        }

        public void OnGoldCountChange(Component sender, object data)
        {
            if (data is not float goldCount) return;
            _goldCount = goldCount;
            EnableButton(_goldCount >= unitStatSo.Price && _queueStatus && _canSpawn);
        }

        public void OnGameSpeedChange(Component sender, object data)
        {
            if (data is not GameSpeed gameSpeed) return;
            EnableButton(gameSpeed == GameSpeed.Stop ? false : _goldCount >= unitStatSo.Price && _queueStatus && _canSpawn);
        }

        public void OnAgeUpgrade(Component sender, object data)
        {
            StartCoroutine(ChangeSprite());
        }

        private IEnumerator ChangeSprite()
        {
            yield return new WaitForSeconds(1f);
            _image.sprite = unitStatSo.Sprite;
        }
    }
}
