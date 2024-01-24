using Supinfo.Project.Scripts.ScriptableObjects.UnitTypes;
using Supinfo.Project.Scripts.Stats;
using UnityEngine;

namespace Supinfo.Project.Scripts.ScriptableObjects.Data
{
    /// <summary>
    ///  UnitData is a ScriptableObject used for storing data for various unit types in the game.
    ///  It includes details about general properties, unit characteristics, and rewards for defeating the unit.
    /// </summary>
    [CreateAssetMenu(fileName = "UnitData", menuName = "ScriptableObject/Units/UnitData", order = 1)]
    public class UnitData : ScriptableObject
    {
        //--------- General properties ---------//
        
        /// <summary>
        /// Array of GameObject prefabs for the unit. This can store sprite data or other prefab related data.
        /// </summary>
        [Header("General properties")]
        [SerializeField] private GameObject[] prefabs;
        public GameObject[] Prefab => prefabs;
        
        //--------- Characteristics ---------//
        
        /// <summary>
        /// Type of the unit, defined in UnitType.
        /// </summary>
        [Header("Characteristics")] 
        [SerializeField] private UnitType type;
        public UnitType Type => type;
        
        /// <summary>
        /// Walking speed of the unit.
        /// </summary>
        [SerializeField] private Stat walkSpeed;
        public Stat WalkSpeed => walkSpeed;

        /// <summary>
        /// Purchase price of the unit.
        /// </summary>
        [SerializeField] private Stat price;
        public Stat Price => price;
        
        /// <summary>
        /// Damage dealt by the unit.
        /// </summary>
        [SerializeField] private Stat damage;
        public Stat Damage => damage;
        
        /// <summary>
        /// Attack speed of the unit.
        /// </summary>
        [SerializeField] private Stat hitSpeed;
        public Stat HitSpeed => hitSpeed;
        
        /// <summary>
        /// Time required to build or spawn the unit.
        /// </summary>
        [SerializeField] private Stat buildTime;
        public Stat BuildTime => buildTime;
        
        /// <summary>
        /// Attack range of the unit.
        /// </summary>
        [SerializeField] private Stat range;
        public Stat Range => range;
        
        /// <summary>
        /// Maximum health of the unit.
        /// </summary>
        [SerializeField] private Stat maxHealth;
        public Stat MaxHealth => maxHealth;

        //--------- Rewards upon killing ---------//
        
        /// <summary>
        /// Amount of gold given for defeating the unit.
        /// </summary>
        [Header("Rewards upon killing")]
        [SerializeField] private Stat goldGiven;
        public Stat GoldGiven => goldGiven;
        
        /// <summary>
        /// Amount of experience given for defeating the unit.
        /// </summary>
        [SerializeField] private Stat experienceGiven;
        public Stat ExperienceGiven => experienceGiven;
    }
}

