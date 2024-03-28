using System;
using Supinfo.Project.ScriptableObjects.Common;
using Supinfo.Project.Scripts.Interfaces.Upgrades;
using Supinfo.Project.Scripts.ScriptableObjects.UnitTypes;
using UnityEngine;

namespace Supinfo.Project.ScriptableObjects.Unit
{
    [CreateAssetMenu(menuName = "ScriptableObject/Units/UnitAttackSo")]
    public class UnitAttackSo : AttackSo, IUpgradable
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
        [SerializeField] private UnitType unitType;
        public UnitType Type => unitType;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">UpgradeType</param>
        public void Upgrade(UpgradeType type)
        {
            switch (type)
            {
                case UpgradeType.Attack:
                    currentAttackUpgrade++;
                    break;
                case UpgradeType.Range:
                    currentRangeUpgrade++;
                    break;
                default:
                    Debug.Log("Wrong type of upgrade (health)");
                    break;
            }
        }
    }
}