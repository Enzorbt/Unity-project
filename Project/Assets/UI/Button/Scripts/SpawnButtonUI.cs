using System;
using System.Collections;
using ScriptableObjects.Unit;
using Supinfo.Project.Scripts.Events;
using Supinfo.Project.Scripts.Managers;
using UnityEngine;
using UnityEngine.Serialization;

namespace Supinfo.Project.UI.Button.Scripts
{
    public class SpawnButtonUI : MonoBehaviour
    {
        /// <summary>
        /// The unit spawn So with the prefab and cooldown stat
        /// </summary>
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

        /// <summary>
        /// Event to be raised when the button is clicked.
        /// </summary>
        [SerializeField] private GameEvent onSpawnUnit;
        
        [SerializeField]
        private GameEvent onGoldChange;

        private bool _canSpawn = true;
        
        private UnityEngine.UI.Button _button;

        private float _goldCount;

        private bool _queueStatus = true;

        private void Awake()
        {
            _button = transform.GetComponentInChildren<UnityEngine.UI.Button>();
            IsActive = true;
        }

        /// <summary>
        /// Method to be called when the button is clicked.
        /// Raises the onClick event with the associated unit.
        /// </summary>
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
            
            onGoldChange.Raise(this, - unitStatSo.Price);
            
            yield return new WaitForSeconds(unitStatSo.BuildTime);
            
            _canSpawn = true;
            
            EnableButton(_goldCount >= unitStatSo.Price && _queueStatus && _canSpawn);
        }

        // function to change visual and enable the button
        private void EnableButton(bool value)
        {
            if (_button is null) return;
            if(!IsActive) return;
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
            if(data is not float goldCount) return;
            _goldCount = goldCount;
            EnableButton(_goldCount >= unitStatSo.Price && _queueStatus && _canSpawn);
        }


        public void OnGameSpeedChange(Component sender, object data)
        {
            if (data is not GameSpeed gameSpeed) return;
            
            EnableButton(gameSpeed == GameSpeed.Stop ? false : _goldCount >= unitStatSo.Price && _queueStatus && _canSpawn);
        }
    }

}
