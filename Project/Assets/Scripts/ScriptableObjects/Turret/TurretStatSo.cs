using Supinfo.Project.Scripts.Common.Stats;
using Supinfo.Project.Scripts.Interfaces;
using Supinfo.Project.Scripts.Interfaces.Upgrades;
using UnityEngine;

namespace ScriptableObjects.Turret
{
    [CreateAssetMenu(menuName = "ScriptableObject/Stats/TurretUnitStat", order = 3)]

    public class TurretStatSo: ScriptableObject, IUpgradable, IAgeUpgradable
    {
        private int _currentAge;
        private int _currentAttackUpgrade;
        private int _currentRangeUpgrade;
        private int _currentPriceUpgrade;
        
        private void OnEnable()
        {
            _currentAge = 0;
            _currentAttackUpgrade = 0;
            _currentRangeUpgrade = 0;
            _currentPriceUpgrade = 0;
        }
        
        /// <summary>
        /// Prefab for the unit
        /// </summary>
        [Header("Prefabs - needs at least 1, max 7")] 
        [SerializeField] private GameObject[] prefabs;

        public GameObject GetPrefab ()
        {
            return prefabs[_currentAge];
        }
        
        /// <summary>
        /// Purchase price of the unit.
        /// </summary>
        [Header("Price")] 
        [SerializeField] private Stat price;
        public float Price => price.GetValue(_currentAge, _currentPriceUpgrade);

        /// <summary>
        /// Damage dealt by the unit.
        /// </summary>
        [Header("Damage dealt")]
        [SerializeField] protected Stat damage;
        public float Damage => damage.GetValue(_currentAge, _currentAttackUpgrade);
        
        /// <summary>
        /// Attack speed of the unit.
        /// </summary>
        [Header("Hit Speed (Attack cooldown)")]
        [SerializeField] protected Stat hitSpeed;
        public float HitSpeed => hitSpeed.GetValue(_currentAge, 0);
        
        /// <summary>
        /// The target type of the unit
        /// </summary>
        [SerializeField] private string targetTag;
        public string TargetTag => targetTag; 
        
        /// <summary>
        /// Attack range of the unit.
        /// </summary>
        [Header("Range of the Units")]
        [SerializeField] protected Stat range;
        public float Range => range.GetValue(_currentAge, _currentRangeUpgrade);
        
        
        public void UpgradeAge()
        {
            _currentAge++;
        }

        public void Upgrade(UpgradeType type)
        {
            switch (type)
            {
                case UpgradeType.Attack:
                    _currentAttackUpgrade++;
                    break;
                case UpgradeType.Range:
                    _currentRangeUpgrade++;
                    break;
                case UpgradeType.Price:
                    _currentPriceUpgrade++;
                    break;
                default:
                    Debug.LogError("Wrong Upgrade Type");
                    break;
            }
        }
    }
}