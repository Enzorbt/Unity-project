using Supinfo.Project.Scripts.Common.Stats;
using Supinfo.Project.Scripts.ScriptableObjects.UnitTypes;
using UnityEngine;

namespace Supinfo.Project.ScriptableObjects.Common
{
    public class AttackSo : ScriptableObject
    {
        /// <summary>
        /// Damage dealt by the unit.
        /// </summary>
        [Header("Damage dealt")]
        [SerializeField] protected Stat damage;
        public Stat Damage => damage;
        
        /// <summary>
        /// Attack speed of the unit.
        /// </summary>
        [Header("Hit Speed (Attack cooldown)")]
        [SerializeField] protected Stat hitSpeed;
        public Stat HitSpeed => hitSpeed;
        
        /// <summary>
        /// Attack range of the unit.
        /// </summary>
        [Header("Range of the Units")]
        [SerializeField] protected Stat range;
        public Stat Range => range;
    }
}