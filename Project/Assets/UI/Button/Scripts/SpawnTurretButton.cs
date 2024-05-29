using System;
using ScriptableObjects.Turret;
using Supinfo.Project.Scripts.Events;
using Supinfo.Project.Scripts.Managers;
using UnityEngine;

namespace Supinfo.Project.UI.Button.Scripts
{
    public class SpawnTurretButton : MonoBehaviour
    {
        [SerializeField] private GameEvent onSpawnTurret;
        [SerializeField] private GameEvent onGoldChange;

        [SerializeField] private TurretStatSo turretStatSo;
        
        private int _spawnNumber;
        private UnityEngine.UI.Button _button;
        private float _goldCount;

        private void Awake()
        {
            _button = GetComponentInChildren<UnityEngine.UI.Button>();
        }

        public void OnClick()
        {
            if (_spawnNumber >= 4) return;
            onSpawnTurret.Raise(this, turretStatSo);
            onGoldChange.Raise(this, -turretStatSo.Price);
            _spawnNumber++;
            EnableButton(_spawnNumber <= 4 && _goldCount >= turretStatSo.Price);
        }

        private void EnableButton(bool value)
        {
            _button.enabled = value;
        }

        public void OnGameSpeedChange(Component sender, object data)
        {
            if (data is not GameSpeed gameSpeed) return;
            EnableButton(gameSpeed == GameSpeed.Stop ? false : _spawnNumber <= 4 && _goldCount >= turretStatSo.Price);
        }

        public void OnGoldCountChange(Component sender, object data)
        {
            if(data is not float goldCount) return;
            _goldCount = goldCount;
            EnableButton(_spawnNumber <= 4 && _goldCount >= turretStatSo.Price);
        }
    }
}