using Supinfo.Project.Scripts.Common.Stats;
using Supinfo.Project.Scripts.ScriptableObjects.UnitTypes;
using UnityEngine;

namespace Supinfo.Project.ScriptableObjects.Unit
{
    [CreateAssetMenu(menuName = "ScriptableObject/Units/UnitAttackSo")]
    public class UnitAttackSo : ScriptableObject
    {
        /// <summary>
        /// Type of the unit, defined in UnitType.
        /// </summary>
        [Header("Characteristics")] 
        [SerializeField] private UnitType type;
        public UnitType Type => type; 
        
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
        /// Attack range of the unit.
        /// </summary>
        [SerializeField] private Stat range;
        public Stat Range => range;
    }
}