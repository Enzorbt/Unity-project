using Supinfo.Project.ScriptableObjects.Common;
using Supinfo.Project.Scripts.Common.Stats;
using Supinfo.Project.Scripts.ScriptableObjects.UnitTypes;
using UnityEngine;

namespace Supinfo.Project.ScriptableObjects.Unit
{
    [CreateAssetMenu(menuName = "ScriptableObject/Units/UnitAttackSo")]
    public class UnitAttackSo : AttackSo
    {
        private void OnEnable()
        {
            currentAge = 0;
            currentAttackUpgrade = 0;
            currentRangeUpgrade = 0;
        }
        
        /// <summary>
        /// Type of the unit, defined in UnitType.
        /// </summary>
        [Header("Characteristics")] 
        [SerializeField] private UnitType type;
        public UnitType Type => type; 
        
        /// <summary>
        /// Build time of the unit
        /// </summary>
        [Header("Spawn Cooldown")] 
        [SerializeField] private Stat buildTime;
        public float BuildTime => buildTime.GetValue(currentAge, 0);
    }
}