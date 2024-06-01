using Supinfo.Project.Scripts.Common.Stats;
using Supinfo.Project.Scripts.Interfaces;
using UnityEngine;

namespace Supinfo.Project.ScriptableObjects.Base
{
    /// <summary>
    /// ScriptableObject representing the base unit stats which can be upgraded with age.
    /// </summary>
    [CreateAssetMenu(menuName = "ScriptableObject/Stats/BaseUnitStat", order = 2)]
    public class BaseStatSo : ScriptableObject, IAgeUpgradable
    {
        /// <summary>
        /// Private variable (int), to declare current age.
        /// </summary>
        private int _currentAge;
        
        /// <summary>
        /// Initializes the current age to zero when the ScriptableObject is enabled.
        /// </summary>
        private void OnEnable()
        {
            _currentAge = 0;
        }

        /// <summary>
        /// The maximum health of the object. This is a Stat type, allowing for the flexibility of assigning various health values.
        /// </summary>
        [Header("Max health")] [SerializeField] protected Stat maxHealth;

        /// <summary>
        /// Gets the maximum health value for the current age.
        /// </summary>
        public float MaxHealth => maxHealth.GetValue(_currentAge);

        /// <summary>
        /// Array of sprites representing different ages of the unit.
        /// </summary>
        [Header("Sprites (7, 1 per age)")] [SerializeField] private Sprite[] _sprites;

        /// <summary>
        /// Sprite variable fdr current age. 
        /// </summary>
        public Sprite Sprite => _sprites[_currentAge];

        /// <summary>
        /// Upgrades the age of the unit, incrementing the current age by one.
        /// </summary>
        public void UpgradeAge()
        {
            _currentAge++;
            Debug.Log(_currentAge);
        }
    }
}