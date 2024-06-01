using Supinfo.Project.Scripts.ScriptableObjects.UnitTypes;
using Supinfo.Project.Scripts.Common.Stats;
using UnityEngine;

namespace Supinfo.Project.Scripts.ScriptableObjects.Data
{
    /// <summary>
    /// UnitData is a ScriptableObject used for storing data for various unit types in the game.
    /// It includes details about general properties, unit characteristics, and rewards for defeating the unit.
    /// </summary>
    // [CreateAssetMenu(fileName = "UnitData", menuName = "ScriptableObject/Units/UnitData", order = 1)]
    public class UnitData : ScriptableObject
    {
        //--------- General properties ---------//

        /// <summary>
        /// Array of GameObject prefabs for the unit. This can store sprite data or other prefab related data.
        /// </summary>
        [Header("General properties")]
        [SerializeField] private GameObject[] prefabs;

        /// <summary>
        /// Gets the array of prefabs.
        /// </summary>
        public GameObject[] Prefab => prefabs;

        //--------- Characteristics ---------//

        /// <summary>
        /// Type of the unit, defined in UnitType.
        /// </summary>
        [Header("Characteristics")]
        [SerializeField] private UnitType type;

        /// <summary>
        /// Gets the type of the unit.
        /// </summary>
        public UnitType Type => type;

        /// <summary>
        /// Walking speed of the unit.
        /// </summary>
        [SerializeField] private Stat walkSpeed;

        /// <summary>
        /// Gets the walking speed of the unit.
        /// </summary>
        public Stat WalkSpeed => walkSpeed;

        /// <summary>
        /// Purchase price of the unit.
        /// </summary>
        [SerializeField] private Stat price;

        /// <summary>
        /// Gets the purchase price of the unit.
        /// </summary>
        public Stat Price => price;

        /// <summary>
        /// Damage dealt by the unit.
        /// </summary>
        [SerializeField] private Stat damage;

        /// <summary>
        /// Gets the damage dealt by the unit.
        /// </summary>
        public Stat Damage => damage;

        /// <summary>
        /// Attack speed of the unit.
        /// </summary>
        [SerializeField] private Stat hitSpeed;

        /// <summary>
        /// Gets the attack speed of the unit.
        /// </summary>
        public Stat HitSpeed => hitSpeed;

        /// <summary>
        /// Time required to build or spawn the unit.
        /// </summary>
        [SerializeField] private Stat buildTime;

        /// <summary>
        /// Gets the build time of the unit.
        /// </summary>
        public Stat BuildTime => buildTime;

        /// <summary>
        /// Attack range of the unit.
        /// </summary>
        [SerializeField] private Stat range;

        /// <summary>
        /// Gets the attack range of the unit.
        /// </summary>
        public Stat Range => range;

        /// <summary>
        /// Maximum health of the unit.
        /// </summary>
        [SerializeField] private Stat maxHealth;

        /// <summary>
        /// Gets the maximum health of the unit.
        /// </summary>
        public Stat MaxHealth => maxHealth;

        //--------- Rewards upon killing ---------//

        /// <summary>
        /// Amount of gold given for defeating the unit.
        /// </summary>
        [Header("Rewards upon killing")]
        [SerializeField] private Stat goldGiven;

        /// <summary>
        /// Gets the amount of gold given for defeating the unit.
        /// </summary>
        public Stat GoldGiven => goldGiven;

        /// <summary>
        /// Amount of experience given for defeating the unit.
        /// </summary>
        [SerializeField] private Stat experienceGiven;

        /// <summary>
        /// Gets the amount of experience given for defeating the unit.
        /// </summary>
        public Stat ExperienceGiven => experienceGiven;
    }
}
