using System;
using System.Collections.Generic;
using Supinfo.Project.Scripts.Events;
using UnityEngine;

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
            
        private void Awake()
        {
            _button = transform.GetComponentInChildren<UnityEngine.UI.Button>();
            _spawnButtonUI = GetComponent<SpawnButtonUI>();
        }

        private void Start()
        {
            if(_spawnButtonUI is null) return;
            _spawnButtonUI.IsActive = false;
        }

        public void OnGoldCountChange(Component sender, object data)
        {
            if(data is not float goldCount) return;
            if (_bought) return;
            EnableButton(goldCount >= unlockCosts[_age]);

        }

        public void OnClick()
        {
            if (_bought) return;
            
            onGoldChange.Raise(this, - unlockCosts[_age]);
            
            _spawnButtonUI.IsActive = true;

            _bought = true;
        }

        public void OnAgeUpgrade(Component sender, object data)
        {
            _age++;
            _bought = false;
            _spawnButtonUI.IsActive = false;
        }
        
        // function to change visual and enable the button
        private void EnableButton(bool value)
        {
            if (_button is null) return;
            _button.enabled = value;
        }
    }
}