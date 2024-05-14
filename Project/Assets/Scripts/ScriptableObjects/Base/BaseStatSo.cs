using System;
using Supinfo.Project.Scripts.Common.Stats;
using Supinfo.Project.Scripts.Interfaces;
using Supinfo.Project.Scripts.Interfaces.Upgrades;
using UnityEngine;

namespace Supinfo.Project.ScriptableObjects.Base
{
    [CreateAssetMenu(menuName = "ScriptableObject/Stats/BaseUnitStat", order = 2)]
    public class BaseStatSo: ScriptableObject, IAgeUpgradable, IUpgradable
    {
        private int _currentAge;
        private int _currentHealthUpgrade = 0;
        
        private void OnEnable()
        {
            _currentAge = 0;
            _currentHealthUpgrade = 0;
        }
        
        /// <summary>
        /// The maximum health of the object. This is a Stat type, allowing for the flexibility of assigning various health values.
        /// </summary>
        [Header("Max health")]
        [SerializeField] protected Stat maxHealth;
        public float MaxHealth => maxHealth.GetValue(_currentAge, _currentHealthUpgrade);
        
        public void UpgradeAge()
        {
            _currentAge++;
        }

        public void Upgrade(UpgradeType type)
        {
            switch (type)
            {
                case UpgradeType.Health:
                    _currentHealthUpgrade++;
                    break;
                default:
                    Debug.LogError("Wrong upgrade type");
                    break;
            }
        }
    }
}