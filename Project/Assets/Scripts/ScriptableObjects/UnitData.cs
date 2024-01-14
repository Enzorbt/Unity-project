using System;
using Supinfo.Project.Stats;
using UnityEngine;

namespace ScriptableObjects
{
    [Serializable]
    public enum UnitType
    {
        Melee,
        Range,
        AntiArmor,
        Armor,
    }
    /// <summary>
    ///  
    /// </summary>
    [CreateAssetMenu(fileName = "UnitData", menuName = "ScriptableObject/Units/UnitData", order = 1)]
    public class UnitData : ScriptableObject
    {
        //--------- General properties ---------
        [Header("General properties")]
        // sprite data can be stored in the prefab or here
        [SerializeField]
        private GameObject[] _prefab;
        
        
        //--------- Characteristics ---------
        [Header("Characteristics")] 
        [SerializeField]
        private UnitType _type;
        public UnitType Type => _type;

        [SerializeField] 
        private Stat _price;
        public Stat Price => _price;
        [SerializeField]
        private Stat _damage;

        public Stat Damage => _damage;
        [SerializeField]
        private Stat _hitSpeed;

        public Stat HitSpeed => _hitSpeed;
        [SerializeField]
        private Stat _buildTime;

        public Stat BuildTime => _buildTime;
        [SerializeField]
        private Stat _walkSpeed;

        public Stat WalkSpeed => _walkSpeed;
        [SerializeField]
        private Stat _range;

        public Stat Range => _range;
        [SerializeField]
        private Stat _hitPoints;

        public Stat HitPoints => _hitPoints;

        //--------- Rewards upon killing ---------
        [Header("Rewards upon killing")]
        [SerializeField]
        private Stat _goldGiven;

        public Stat GoldGiven => _goldGiven;
        [SerializeField]
        private Stat _experienceGiven;

        public Stat ExperienceGiven => _experienceGiven;
    }
}

