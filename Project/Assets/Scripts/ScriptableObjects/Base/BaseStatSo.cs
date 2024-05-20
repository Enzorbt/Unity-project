using Supinfo.Project.Scripts.Common.Stats;
using Supinfo.Project.Scripts.Interfaces;
using UnityEngine;

namespace Supinfo.Project.ScriptableObjects.Base
{
    [CreateAssetMenu(menuName = "ScriptableObject/Stats/BaseUnitStat", order = 2)]
    public class BaseStatSo: ScriptableObject, IAgeUpgradable
    {
        private int _currentAge;
        
        private void OnEnable()
        {
            _currentAge = 0;
        }
        
        /// <summary>
        /// The maximum health of the object. This is a Stat type, allowing for the flexibility of assigning various health values.
        /// </summary>
        [Header("Max health")]
        [SerializeField] protected Stat maxHealth;
        public float MaxHealth => maxHealth.GetValue(_currentAge);
        
        [Header("Sprites (7, 1 per age)")] [SerializeField]
        private Sprite[] _sprites;
        public Sprite Sprite => _sprites[_currentAge];
        
        public void UpgradeAge()
        {
            _currentAge++;
        }
    }
}