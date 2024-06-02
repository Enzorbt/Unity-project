using Supinfo.Project.Scripts;
using Supinfo.Project.Scripts.Common.Stats;
using Supinfo.Project.Scripts.Interfaces;
using Supinfo.Project.Scripts.Interfaces.Upgrades;
using UnityEngine;

namespace ScriptableObjects.Turret
{
    /// <summary>
    /// TurretStatSo is a ScriptableObject used for storing statistics related to turret units in the game.
    /// </summary>
    [CreateAssetMenu(menuName = "ScriptableObject/Stats/TurretUnitStat", order = 3)]
    public class TurretStatSo : ScriptableObject, IUpgradable, IAgeUpgradable
    {
        /// <summary>
        /// The current age of the turret.
        /// </summary>
        private int _currentAge;

        /// <summary>
        /// The current attack upgrade level of the turret.
        /// </summary>
        private int _currentAttackUpgrade;

        /// <summary>
        /// The current range upgrade level of the turret.
        /// </summary>
        private int _currentRangeUpgrade;
        
        /// <summary>
        /// Initializes the current age and upgrade levels when the scriptable object is enabled.
        /// </summary>
        private void OnEnable()
        {
            _currentAge = 0;
            _currentAttackUpgrade = 0;
            _currentRangeUpgrade = 0;
        }
        
        /// <summary>
        /// Gets the prefab for the turret unit.
        /// </summary>
        [Header("Prefab")] 
        [SerializeField] private GameObject prefabs;
        public GameObject Prefab => prefabs;
        
        /// <summary>
        /// Gets the purchase price of the turret unit.
        /// </summary>
        [Header("Price")] 
        [SerializeField] private Stat price;
        public float Price => price.GetValue();

        /// <summary>
        /// Gets the damage dealt by the turret unit.
        /// </summary>
        [Header("Damage dealt")]
        [SerializeField] protected Stat damage;
        public float Damage => damage.GetValue(_currentAge, _currentAttackUpgrade);
        
        /// <summary>
        /// Gets the attack speed of the turret unit.
        /// </summary>
        [Header("Hit Speed (Attack cooldown)")]
        [SerializeField] protected Stat hitSpeed;
        public float HitSpeed => hitSpeed.GetValue(_currentAge);
        
        /// <summary>
        /// Gets the attack range of the turret unit.
        /// </summary>
        [Header("Range of the Units")]
        [SerializeField] protected Stat range;
        public float Range => range.GetValue(_currentAge, _currentRangeUpgrade);
        
        /// <summary>
        /// Gets the sprite for the turret unit based on the current age.
        /// </summary>
        [Header("Sprites (7, 1 per age)")] [SerializeField]
        private Sprite[] _sprites;
        public Sprite Sprite => _sprites[_currentAge];
        
        /// <summary>
        /// Upgrades the age of the turret unit.
        /// </summary>
        public void UpgradeAge()
        {
            _currentAge++;
        }

        /// <summary>
        /// Upgrades the specified aspect of the turret unit.
        /// </summary>
        /// <param name="type">The type of upgrade to perform.</param>
        public void Upgrade(UpgradeType type)
        {
            switch (type)
            {
                case UpgradeType.TurretAttack:
                    _currentAttackUpgrade++;
                    break;
                case UpgradeType.TurretRange:
                    _currentRangeUpgrade++;
                    break;
                default:
                    break;
            }
        }
    }
}
