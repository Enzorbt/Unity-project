using System.Collections;
using Supinfo.Project.Scripts.Events;
using Supinfo.Project.Scripts.ScriptableObjects.Capacity;
using UnityEngine;
using UnityEngine.UI;
using Supinfo.Project.Scripts.Managers;

namespace Supinfo.Project.Capacity.Scripts
{
    /// <summary>
    /// UI component to handle the special capacity button functionality.
    /// </summary>
    public class SpecialCapacityButtonUI : MonoBehaviour
    {
        /// <summary>
        /// Maximum experience points.
        /// </summary>
        private float _xpMax;

        /// <summary>
        /// Event triggered on button click.
        /// </summary>
        [SerializeField] private GameEvent onClick;

        /// <summary>
        /// Event triggered when XP changes.
        /// </summary>
        [SerializeField] private GameEvent onXpChange;
        
        /// <summary>
        /// Scriptable object for capacity data.
        /// </summary>
        [SerializeField] private CapacitySo _capacitySo;
        
        /// <summary>
        /// Cost of using the capacity in terms of XP ratio.
        /// </summary>
        [Range(0, 1)] [SerializeField] private float cost;

        /// <summary>
        /// // Image component of the button.
        /// </summary>
        private Image _image;  
        
        /// <summary>
        /// // Current XP ratio.
        /// </summary>
        private float _xpRatio; 
        
        /// <summary>
        /// Flag to check if the capacity can be used.
        /// </summary>
        private bool _canUse = true; 

        /// <summary>
        /// Image for cooldown representation.
        /// </summary>
        [SerializeField] private Image cooldownImage;

        /// <summary>
        /// The image of the button (background).
        /// </summary>
        private Image _imageButton;

        /// <summary>
        /// The button component of the game object.
        /// </summary>
        private Button _button;


        private void Awake()
        {
            _button = gameObject.GetComponentInChildren<Button>();
            _imageButton = GetComponentsInChildren<Image>()[0];
            SetActiveButton(false);  // Initially disable the button.
            _image = GetComponentsInChildren<Image>()[1];  // Get the second Image component in children.
            if (_image != null)
            {
                _image.sprite = _capacitySo.Sprite;  // Set the image sprite from capacity data.
            }
            
            if (cooldownImage != null)
            {
                cooldownImage.type = Image.Type.Filled;  // Set the image type to filled.
                cooldownImage.fillMethod = Image.FillMethod.Radial360;  // Set the fill method to radial.
                cooldownImage.fillOrigin = (int)Image.Origin360.Top;  // Set the fill origin to top.
                cooldownImage.fillAmount = 0f;  // Initialize fill amount to zero.
            }
        }

        public void OnClick()
        {
            if (_canUse && _xpRatio >= cost)
            {
                StartCoroutine(UseCapacityWithCooldown());  // Start coroutine for capacity usage and cooldown.
                StartCoroutine(CooldownImageAnimation());  // Start coroutine for cooldown image animation.
            }
        }

        /// <summary>
        /// Coroutine to handle the capacity usage with cooldown.
        /// </summary>
        private IEnumerator UseCapacityWithCooldown()
        {
            _canUse = false;  // Set capacity use flag to false.
            SetActiveButton(false);  // Disable the button.
            onClick.Raise(this, _capacitySo);  // Raise the click event.
            onXpChange.Raise(this, -(_xpMax * cost));  // Raise the XP change event.

            yield return new WaitForSeconds(_capacitySo.Cooldown + (_capacitySo.CapabilityType == CapabilityType.Meteor ? 1.25f : 0f));  // Wait for the cooldown duration.

            _canUse = true;  // Set capacity use flag to true.
            SetActiveButton(_xpRatio >= cost && _canUse);  // Update button state based on XP ratio and use flag.
        }
        private IEnumerator CooldownImageAnimation()
        {
            if (cooldownImage is null) yield break;
            var cooldownDuration = _capacitySo.Cooldown+ (_capacitySo.CapabilityType == CapabilityType.Meteor ? 1.25f : 0f);
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
        /// Method called when XP ratio changes.
        /// </summary>
        /// <param name="sender">Sender component.</param>
        /// <param name="data">XP ratio data.</param>
        public void OnXpRatioChange(Component sender, object data)
        {
            if (data is not float xpRatio) return;  // Validate XP ratio data.

            _xpRatio = xpRatio;  // Update XP ratio.

            SetActiveButton(_xpRatio >= cost && _canUse);  // Update button state based on XP ratio and use flag.
        }

        /// <summary>
        /// Method to set the button active state.
        /// </summary>
        /// <param name="state">Active state.</param>
        private void SetActiveButton(bool state)
        {
            if (_button is null) return;
            _button.enabled = state;  // Enable/disable the button.
            
            if(_imageButton is null) return;
            _imageButton.color = state ? new Color(1,1,1,0.4f) : new Color(1,0,0,0.4f);

        }

        /// <summary>
        /// Method called when maximum XP changes.
        /// </summary>
        /// <param name="sender">Sender component.</param>
        /// <param name="data">Maximum XP data.</param>
        public void OnXpMaxChange(Component sender, object data)
        {
            if (data is not float xpMax) return;  // Validate maximum XP data.

            _xpMax = xpMax;  // Update maximum XP.
        }

        /// <summary>
        /// Method called when age is upgraded.
        /// </summary>
        /// <param name="sender">Sender component.</param>
        /// <param name="data">Upgrade data.</param>
        public void OnAgeUpgrade(Component sender, object data)
        {
            StartCoroutine(ChangeSprite());  // Start coroutine to change sprite.
        }

        /// <summary>
        /// Coroutine to change the sprite after a delay.
        /// </summary>
        private IEnumerator ChangeSprite()
        {
            yield return new WaitForSeconds(1f);  // Wait for 1 second.
            if (_image == null) yield break;  // Exit if image is null.
            _image.sprite = _capacitySo.Sprite;  // Update image sprite.
        }

        /// <summary>
        /// Method called when game speed changes.
        /// </summary>
        /// <param name="sender">Sender component.</param>
        /// <param name="data">Game speed data.</param>
        public void OnGameSpeedChange(Component sender, object data)
        {
            if (data is not GameSpeed gameSpeed) return;  // Validate game speed data.

            SetActiveButton(gameSpeed == GameSpeed.Stop ? false : _xpRatio >= cost && _canUse);  // Update button state based on game speed.
        }

        /// <summary>
        /// Method called when special capacity status changes.
        /// </summary>
        /// <param name="sender">Sender component.</param>
        /// <param name="data">Status data.</param>
        public void OnSpecialCapacityStatusChange(Component sender, object data)
        {
            if (data is not bool status) return;  // Validate status data.
            _canUse = status;  // Update capacity use flag.
            SetActiveButton(_xpRatio >= cost && _canUse);  // Update button state based on XP ratio and use flag.
        }
    }
}
