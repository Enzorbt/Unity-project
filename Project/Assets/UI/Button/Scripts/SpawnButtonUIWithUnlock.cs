using System;
using System.Collections.Generic;
using Supinfo.Project.Scripts.Events;
using Supinfo.Project.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Supinfo.Project.UI.Button.Scripts
{
    /// <summary>
    /// Handles the functionality of a spawn button with an unlock feature.
    /// </summary>
    public class SpawnButtonUIWithUnlock : MonoBehaviour
    {
        /// <summary>
        /// The costs required to unlock the button for each age.
        /// </summary>
        [SerializeField] private List<float> unlockCosts;

        /// <summary>
        /// Event triggered when the gold count changes.
        /// </summary>
        [SerializeField] private GameEvent onGoldChange;
        
        /// <summary>
        /// The current age.
        /// </summary>
        private int _age;
        
        /// <summary>
        /// Reference to the button component.
        /// </summary>
        private UnityEngine.UI.Button _button;

        /// <summary>
        /// Reference to the spawn button UI component.
        /// </summary>
        private SpawnButtonUI _spawnButtonUI;

        /// <summary>
        /// Indicates whether the button has been bought.
        /// </summary>
        private bool _bought;

        /// <summary>
        /// The current amount of gold.
        /// </summary>
        private float _goldCount;

        /// <summary>
        /// Reference to the image component.
        /// </summary>
        private Image _image;

        /// <summary>
        /// The image component of the button (background).
        /// </summary>
        private Image _imageButton;
            
        private void Awake()
        {
            _button = transform.GetComponentInChildren<UnityEngine.UI.Button>();
            _spawnButtonUI = GetComponent<SpawnButtonUI>();
            _imageButton = GetComponentsInChildren<Image>()[0];
            _image = GetComponentsInChildren<Image>()[2];
        }

        private void Start()
        {
            if(_spawnButtonUI is null) return;
            _spawnButtonUI.IsActive = false;
            _image.enabled = true;
        }

        /// <summary>
        /// Handles the event when the gold count changes.
        /// </summary>
        public void OnGoldCountChange(Component sender, object data)
        {
            if(data is not float goldCount) return;
            _goldCount = goldCount;
            
            if (_bought) return;
            
            EnableButton(_goldCount >= unlockCosts[_age]);
        }
        
        public void OnClick()
        {
            if (_bought) return;
            
            onGoldChange.Raise(this, - unlockCosts[_age]);
            
            _spawnButtonUI.IsActive = true;
            _image.enabled = false;

            _bought = true;
        }

        /// <summary>
        /// Handles the event when the age upgrades.
        /// </summary>
        public void OnAgeUpgrade(Component sender, object data)
        {
            _age++;
            _bought = false;
            _spawnButtonUI.IsActive = false;
            _image.enabled = true;
        }
        
        // function to change visual and enable the button
        private void EnableButton(bool value)
        {
            if (_button is null) return;
            _button.enabled = value;
            _imageButton.color = value ? new Color(1,1,1,0.4f) : new Color(1,0,0,0.4f);
        }
        
        /// <summary>
        /// Handles the event when the game speed changes.
        /// </summary>
        public void OnGameSpeedChange(Component sender, object data)
        {
            if (data is not GameSpeed gameSpeed) return;

            if (_bought) return;
            
            EnableButton(gameSpeed == GameSpeed.Stop ? false : _goldCount >= unlockCosts[_age]);
        }
    }
}
