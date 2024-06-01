using Supinfo.Project.Scripts.Common.Stats;
using Supinfo.Project.Scripts.Interfaces;
using Supinfo.Project.Scripts.ScriptableObjects.UnitTypes;
using UnityEngine;

namespace ScriptableObjects.Unit
{
    /// <summary>
    /// UnitStatSo is a ScriptableObject used to manage and store stats for various units.
    /// It supports age-based upgrades and other specific upgrades for attack, range, health, and gold given.
    /// </summary>
    public class UnitStatSo : ScriptableObject, IAgeUpgradable
    {
        private int _currentAge;
        protected int currentAttackUpgrade;
        protected int currentRangeUpgrade;
        protected int currentGoldGivenUpgrade;
        protected int currentHealthUpgrade;

        /// <summary>
        /// Initializes the unit stats when the scriptable object is enabled.
        /// Resets age and upgrade values.
        /// </summary>
        private void OnEnable()
        {
            _currentAge = 0;
            currentAttackUpgrade = 0;
            currentRangeUpgrade = 0;
            currentGoldGivenUpgrade = 0;
            currentHealthUpgrade = 0;
        }

        /// <summary>
        /// Upgrades the age of the unit, which can affect its stats.
        /// </summary>
        public void UpgradeAge()
        {
            _currentAge++;
        }

        //--------- Unit Stats ---------//
        
        /// <summary>
        /// Damage dealt by the unit.
        /// </summary>
        [Header("Damage dealt")]
        [SerializeField] protected Stat damage;
        public float Damage => damage.GetValue(_currentAge, currentAttackUpgrade);

        /// <summary>
        /// Attack speed of the unit.
        /// </summary>
        [Header("Hit Speed (Attack cooldown)")]
        [SerializeField] protected Stat hitSpeed;
        public float HitSpeed => hitSpeed.GetValue(_currentAge, 0);

        /// <summary>
        /// The target type of the unit.
        /// </summary>
        [SerializeField] private string targetTag;
        public string TargetTag => targetTag; 

        /// <summary>
        /// Attack range of the unit.
        /// </summary>
        [Header("Range of the Units")]
        [SerializeField] protected Stat range;
        public float Range => range.GetValue(_currentAge, currentRangeUpgrade);

        /// <summary>
        /// Build time of the unit.
        /// </summary>
        [Header("Spawn Cooldown")] 
        [SerializeField] private Stat buildTime;
        public float BuildTime => buildTime.GetValue(_currentAge, 0);

        /// <summary>
        /// The amount of gold given to the player upon killing the unit.
        /// This value can be used to determine the economic reward for defeating this unit.
        /// </summary>
        [Header("Gold given when unit dies")]
        [SerializeField] private Stat goldGiven;
        public float GoldGiven => goldGiven.GetValue(_currentAge, currentGoldGivenUpgrade);

        /// <summary>
        /// The amount of experience given to the player upon killing the unit.
        /// This value helps in calculating the experience points a player earns, contributing to their overall progression.
        /// </summary>
        [Header("Experience given when unit dies")]
        [SerializeField] private Stat experienceGiven;
        public float ExperienceGiven => experienceGiven.GetValue(_currentAge, 0);

        /// <summary>
        /// Type of the unit, defined in UnitType.
        /// </summary>
        [Header("Unit type")] 
        [SerializeField] private UnitType unitType;
        public UnitType Type => unitType;

        /// <summary>
        /// The maximum health of the object. This is a Stat type, allowing for the flexibility of assigning various health values.
        /// </summary>
        [Header("Max health")]
        [SerializeField] protected Stat maxHealth;
        public float MaxHealth => maxHealth.GetValue(_currentAge, currentHealthUpgrade);

        /// <summary>
        /// Walking speed of the unit.
        /// </summary>
        [Header("Walk speed")]
        [SerializeField] protected Stat walkSpeed;
        public float WalkSpeed => walkSpeed.GetValue(_currentAge);

        /// <summary>
        /// Prefab for the unit.
        /// </summary>
        [Header("Prefabs - needs at least 1, max 7")] 
        [SerializeField] private GameObject[] prefabs;

        /// <summary>
        /// Returns the first prefab for the unit. Adjust this if different prefabs or more are needed.
        /// </summary>
        /// <returns>The unit's prefab GameObject.</returns>
        public GameObject GetPrefab()
        {
            return prefabs[0]; // TODO: Change if different prefab or just one
        }

        /// <summary>
        /// Purchase price of the unit.
        /// </summary>
        [Header("Price")] 
        [SerializeField] private Stat price;
        public float Price => price.GetValue(_currentAge);

        /// <summary>
        /// Basic sprites of the unit.
        /// </summary>
        [Header("Sprites (7, 1 per age)")] 
        [SerializeField] private Sprite[] sprite;
        public Sprite Sprite => sprite[_currentAge];

        /// <summary>
        /// Animations of the unit.
        /// </summary>
        [Header("Animator Controller (7, 1 per age)")] 
        [SerializeField] private RuntimeAnimatorController[] controllers;
        public RuntimeAnimatorController Controllers => controllers[_currentAge];
    }
}
