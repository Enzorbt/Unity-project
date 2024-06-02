using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using ScriptableObjects.Unit;
using Supinfo.Project.Scripts.Events;
using Supinfo.Project.Scripts.Managers;

namespace Supinfo.Project.UI.Button.Scripts
{
    /// <summary>
    /// The SpawnButtonUI class manages the UI button for spawning units.
    /// </summary>
    public class SpawnButtonUI : MonoBehaviour
    {
        [SerializeField] private UnitStatSo unitStatSo;
        private bool _isActive;
        
        /// <summary>
        /// Indicates whether the button is active or not.
        /// </summary>
        public bool IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                EnableButton(_goldCount >= unitStatSo.Price && _queueStatus && _canSpawn);
            }
        }

        /// <summary>
        /// Reference to the image component for this button of the button.
        /// </summary>
        private Image _imageButton;
        
        /// <summary>
        /// Reference to the image component for this button of the unitSprite.
        /// </summary>
        private Image _imageUnit;
        
        /// <summary>
        /// Event triggered when a unit is spawned.
        /// </summary>
        [SerializeField] private GameEvent onSpawnUnit;
        
        /// <summary>
        /// Event triggered when the gold count changes.
        /// </summary>
        [SerializeField] private GameEvent onGoldChange;
        
        /// <summary>
        /// Indicates whether the button can currently spawn units.
        /// </summary>
        private bool _canSpawn = true;
        
        /// <summary>
        /// Reference to the button component.
        /// </summary>
        private UnityEngine.UI.Button _button;
        
        /// <summary>
        /// The current amount of gold.
        /// </summary>
        private float _goldCount;
        
        /// <summary>
        /// Indicates the status of the spawn queue.
        /// </summary>
        private bool _queueStatus = true;
        
        /// <summary>
        /// Reference to the image component representing the cooldown.
        /// </summary>
        [SerializeField] private Image cooldownImage;


        private void Awake()
        {
            _button = transform.GetComponentInChildren<UnityEngine.UI.Button>();
            IsActive = true;
            _imageButton = GetComponentsInChildren<Image>()[0];
            _imageUnit = GetComponentsInChildren<Image>()[1];
            
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
                StartCoroutine(CooldownImageAnimation());
            }
        }

        /// <summary>
        /// Coroutine for spawning units with cooldown.
        /// </summary>
        private IEnumerator SpawnWithCooldown()
        {
            _canSpawn = false;
            EnableButton(_goldCount >= unitStatSo.Price && _queueStatus && _canSpawn);
            onSpawnUnit.Raise(this, unitStatSo);
            onGoldChange.Raise(this, -unitStatSo.Price);

            yield return new WaitForSeconds(unitStatSo.BuildTime);

            _canSpawn = true;
            EnableButton(_goldCount >= unitStatSo.Price && _queueStatus && _canSpawn);
        }
        
        /// <summary>
        /// Coroutine for playing the animation with cooldown image.
        /// </summary>
        private IEnumerator CooldownImageAnimation()
        {
            if (cooldownImage is null) yield break;
            var cooldownDuration = unitStatSo.BuildTime;
            var elapsedTime = 0f;

            while (elapsedTime < cooldownDuration)
            {
                elapsedTime += Time.deltaTime;
                cooldownImage.fillAmount = elapsedTime / cooldownDuration;
                yield return null;
            }

            cooldownImage.fillAmount = 0f;
        }

        /// <summary>
        /// Enables or disables the button based on the specified value.
        /// </summary>
        /// <param name="value">The value indicating whether the button should be enabled.</param>
        private void EnableButton(bool value)
        {
            if (_button is null) return;
            if(_imageButton is null) return;
            if (!IsActive) return;
            _button.enabled = value;
            _imageButton.color = value ? new Color(1,1,1,0.4f) : new Color(1,0,0,0.4f);
        }

        /// <summary>
        /// Handles the spawn queue status change event.
        /// </summary>
        public void OnSpawnQueueStatusChange(Component sender, object data)
        {
            if (data is not bool status) return;
            _queueStatus = status;
            EnableButton(_goldCount >= unitStatSo.Price && _queueStatus && _canSpawn);
        }

        /// <summary>
        /// Handles the gold count change event.
        /// </summary>
        public void OnGoldCountChange(Component sender, object data)
        {
            if (data is not float goldCount) return;
            _goldCount = goldCount;
            EnableButton(_goldCount >= unitStatSo.Price && _queueStatus && _canSpawn);
        }

        /// <summary>
        /// Handles the game speed change event.
        /// </summary>
        public void OnGameSpeedChange(Component sender, object data)
        {
            if (data is not GameSpeed gameSpeed) return;
            EnableButton(gameSpeed == GameSpeed.Stop ? false : _goldCount >= unitStatSo.Price && _queueStatus && _canSpawn);
        }

        /// <summary>
        /// Handles the age upgrade event.
        /// </summary>
        public void OnAgeUpgrade(Component sender, object data)
        {
            StartCoroutine(ChangeSprite());
        }

        /// <summary>
        /// Coroutine for changing the sprite.
        /// </summary>
        private IEnumerator ChangeSprite()
        {
            yield return new WaitForSeconds(1f);
            _imageUnit.sprite = unitStatSo.Sprite;
        }
    }
}
