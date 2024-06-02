using Supinfo.Project.Scripts;
using Supinfo.Project.Scripts.Interfaces.Upgrades;
using UnityEngine;

namespace ScriptableObjects.Unit.StatSo
{
    /// <summary>
    /// MeleeStatSo is a ScriptableObject that represents the stats for melee units.
    /// It includes methods for upgrading various aspects of the unit based on the upgrade type.
    /// </summary>
    [CreateAssetMenu(menuName = "ScriptableObject/Stats/MeleeUnitStat", order = 0)]
    public class MeleeStatSo : UnitStatSo, IUpgradable
    {
        /// <summary>
        /// Upgrades the melee unit's stats based on the given upgrade type.
        /// </summary>
        /// <param name="type">The type of upgrade to apply.</param>
        public void Upgrade(UpgradeType type)
        {
            switch (type)
            {
                // Health-related upgrades
                case UpgradeType.MeleeHealth:
                case UpgradeType.ArmorHealth:
                case UpgradeType.AntiArmorHealth:
                    currentHealthUpgrade++;
                    break;
                
                // Gold given upgrade
                case UpgradeType.GoldGiven:
                    currentGoldGivenUpgrade++;
                    break;
                
                // Attack-related upgrades
                case UpgradeType.MeleeAttack:
                case UpgradeType.ArmorAttack:
                case UpgradeType.AntiArmorAttack:
                    currentAttackUpgrade++;
                    break;
                
                // Invalid upgrade type
                default:
                    Debug.Log("Wrong type of upgrade");
                    break;
            }
        }
    }
}