using System;
using Supinfo.Project.Scripts.Common.Stats;
using Supinfo.Project.Scripts.Interfaces;
using Supinfo.Project.Scripts.ScriptableObjects.UnitTypes;
using UnityEngine;

namespace Supinfo.Project.ScriptableObjects.Common
{
    public class AttackSo : ScriptableObject, IAgeUpgradable
    {
        protected int currentAge;
        protected int currentAttackUpgrade = 0;
        protected int currentRangeUpgrade = 0;

        /// <summary>
        /// Damage dealt by the unit.
        /// </summary>
        [Header("Damage dealt")]
        [SerializeField] protected Stat damage;
        public float Damage => damage.GetValue(currentAge, currentAttackUpgrade);
        
        /// <summary>
        /// Attack speed of the unit.
        /// </summary>
        [Header("Hit Speed (Attack cooldown)")]
        [SerializeField] protected Stat hitSpeed;
        public float HitSpeed => hitSpeed.GetValue(currentAge, 0);
        
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
        public float Range => range.GetValue(currentAge, currentRangeUpgrade);
        
        
        public void UpgradeAge()
        {
            currentAge++;
        }
    }
}