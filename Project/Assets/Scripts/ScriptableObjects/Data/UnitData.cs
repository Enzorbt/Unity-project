using Supinfo.Project.Scripts.ScriptableObjects.UnitTypes;
using Supinfo.Project.Scripts.Stats;
using UnityEngine;

namespace Supinfo.Project.Scripts.ScriptableObjects.Data
{
    /// <summary>
    ///  UnitData is the ScriptableObject that is used to store data for the different unit's types.
    /// </summary>
    [CreateAssetMenu(fileName = "UnitData", menuName = "ScriptableObject/Units/UnitData", order = 1)]
    public class UnitData : ScriptableObject
    {
        
        
        //--------- General properties ---------//
        [Header("General properties")]
        // sprite data can be stored in the prefab or here
        [SerializeField] private GameObject[] prefabs;
        public GameObject[] Prefab => prefabs;
        
        //--------- Characteristics ---------//
        [Header("Characteristics")] 
        [SerializeField] private UnitType type;
        public UnitType Type => type;
        
        [SerializeField] private Stat walkSpeed;
        public Stat WalkSpeed => walkSpeed;

        [SerializeField] private Stat price;
        public Stat Price => price;
        
        [SerializeField] private Stat damage;
        public Stat Damage => damage;
        
        [SerializeField] private Stat hitSpeed;
        public Stat HitSpeed => hitSpeed;
        
        [SerializeField] private Stat buildTime;
        public Stat BuildTime => buildTime;
        
        [SerializeField] private Stat range;
        public Stat Range => range;
        
        [SerializeField] private Stat hitPoints;
        public Stat HitPoints => hitPoints;

        //--------- Rewards upon killing ---------//
        [Header("Rewards upon killing")]
        [SerializeField] private Stat goldGiven;
        public Stat GoldGiven => goldGiven;
        
        [SerializeField] private Stat experienceGiven;
        public Stat ExperienceGiven => experienceGiven;
    }
}

