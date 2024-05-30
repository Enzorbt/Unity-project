using System;
using System.Collections.Generic;
using Supinfo.Project.Scripts.Events;
using Supinfo.Project.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Supinfo.Project.UI.Button.Scripts
{
    public class SpawnButtonUIWithUnlock : MonoBehaviour
    {
        [SerializeField]
        private List<float> unlockCosts;

        [SerializeField]
        private GameEvent onGoldChange;
        
        private int _age;
        
        private UnityEngine.UI.Button _button;

        private SpawnButtonUI _spawnButtonUI;

        private bool _bought;

        private float _goldCount;

        public Image _image;
            
        private void Awake()
        {
            _button = transform.GetComponentInChildren<UnityEngine.UI.Button>();
            _spawnButtonUI = GetComponent<SpawnButtonUI>();
            _image = GetComponentsInChildren<Image>()[2];
        }

        private void Start()
        {
            if(_spawnButtonUI is null) return;
            _spawnButtonUI.IsActive = false;
            _image.enabled = true;
        }

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
        }
        
        public void OnGameSpeedChange(Component sender, object data)
        {
            if (data is not GameSpeed gameSpeed) return;

            if (_bought) return;
            
            EnableButton(gameSpeed == GameSpeed.Stop ? false : _goldCount >= unlockCosts[_age]);
        }
    }
}