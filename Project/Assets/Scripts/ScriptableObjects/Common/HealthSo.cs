using Supinfo.Project.Scripts.Common.Stats;
using Supinfo.Project.Scripts.Interfaces;
using UnityEngine;

namespace Supinfo.Project.ScriptableObjects.Common
{
    /// <summary>
    /// HealthSO is a ScriptableObject that defines the health-related properties of an object.
    /// It is used to store the maximum health value of the object.
    /// </summary>
    public class HealthSo : ScriptableObject, IAgeUpgradable
    {
        protected int currentAge;
        protected int currentHealthUpgrade = 0;
        
        /// <summary>
        /// The maximum health of the object. This is a Stat type, allowing for the flexibility of assigning various health values.
        /// </summary>
        [Header("Max health")]
        [SerializeField] protected Stat maxHealth;
        public float MaxHealth => maxHealth.GetValue(currentAge, currentHealthUpgrade);
        
        public void UpgradeAge()
        {
            currentAge++;
        }
    }
}