using Supinfo.Project.Scripts;
using Supinfo.Project.Scripts.Interfaces.Upgrades;
using UnityEngine;

namespace ScriptableObjects.Unit.StatSo
{
    /// <summary>
    /// RangeStatSo is a ScriptableObject that represents the stats for ranged units.
    /// It includes methods for upgrading various aspects of the unit based on the upgrade type.
    /// </summary>
    [CreateAssetMenu(menuName = "ScriptableObject/Stats/RangeUnitStat", order = 0)]
    public class RangeStatSo : UnitStatSo, IUpgradable
    {
        /// <summary>
        /// Upgrades the ranged unit's stats based on the given upgrade type.
        /// </summary>
        /// <param name="type">The type of upgrade to apply.</param>
        public void Upgrade(UpgradeType type)
        {
            switch (type)
            {
                // Gold given upgrade
                case UpgradeType.GoldGiven:
                    currentGoldGivenUpgrade++;
                    break;
                
                // Attack-related upgrades
                case UpgradeType.RangeAttack:
                    currentAttackUpgrade++;
                    break;
                
                // Range-related upgrades
                case UpgradeType.RangeRange:
                    currentRangeUpgrade++;
                    break;
                
                // Invalid upgrade type
                default:
                    Debug.Log("Wrong type of upgrade");
                    break;
            }
        }
    }
}