using Supinfo.Project.Scripts.Common.Stats;
using Supinfo.Project.Scripts.Interfaces;
using UnityEngine;

namespace Supinfo.Project.ScriptableObjects.Base
{
    [CreateAssetMenu(menuName = "ScriptableObject/Stats/BaseUnitStat", order = 2)]
    public class BaseStatSo: ScriptableObject, IAgeUpgradable
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
    }
}