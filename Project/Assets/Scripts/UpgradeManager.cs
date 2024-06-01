using System;
using ScriptableObjects.Turret;
using ScriptableObjects.Unit.StatSo;
using UnityEngine;

namespace Supinfo.Project.Scripts
{
    /// <summary>
    /// Enum representing different types of upgrades available in the game.
    /// </summary>
    public enum UpgradeType
    {
        MeleeAttack,
        MeleeHealth,
        RangeAttack,
        RangeRange,
        AntiArmorAttack,
        AntiArmorHealth,
        ArmorAttack,
        ArmorHealth,
        TurretAttack,
        TurretRange,
        GoldGiven
        // Unlock armor is dealt with in the armor button scripts
    }

    /// <summary>
    /// The UpgradeManager class is responsible for handling the upgrades of various units and turrets in the game.
    /// </summary>
    public class UpgradeManager : MonoBehaviour
    {
        /// <summary>
        /// ScriptableObject for melee unit stats.
        /// </summary>
        [SerializeField] private MeleeStatSo meleeStatSo;
        
        /// <summary>
        /// ScriptableObject for anti-armor unit stats.
        /// </summary>
        [SerializeField] private MeleeStatSo antiArmorStatSo;
        
        /// <summary>
        /// ScriptableObject for armor unit stats.
        /// </summary>
        [SerializeField] private MeleeStatSo armorStatSo;
        
        /// <summary>
        /// ScriptableObject for range unit stats.
        /// </summary>
        [SerializeField] private RangeStatSo rangeStatSo;
        
        /// <summary>
        /// ScriptableObject for turret stats.
        /// </summary>
        [SerializeField] private TurretStatSo turretStatSo;
        
        /// <summary>
        /// ScriptableObject for other melee unit stats.
        /// </summary>
        [SerializeField] private MeleeStatSo otherMeleeStatSo;
        
        /// <summary>
        /// ScriptableObject for other anti-armor unit stats.
        /// </summary>
        [SerializeField] private MeleeStatSo otherAntiArmorStatSo;
        
        /// <summary>
        /// ScriptableObject for other armor unit stats.
        /// </summary>
        [SerializeField] private MeleeStatSo otherArmorStatSo;
        
        /// <summary>
        /// ScriptableObject for other range unit stats.
        /// </summary>
        [SerializeField] private RangeStatSo otherRangeStatSo;

        /// <summary>
        /// Upgrades the specified type of unit or turret based on the upgrade type provided.
        /// </summary>
        /// <param name="sender">The component that triggered the upgrade.</param>
        /// <param name="data">The upgrade type data.</param>
        public void Upgrade(Component sender, object data)
        {
            if (data is not UpgradeType upgradeType) return;
            
            switch (upgradeType)
            {
                case UpgradeType.MeleeAttack:
                case UpgradeType.MeleeHealth:
                    meleeStatSo?.Upgrade(upgradeType);
                    break;
                case UpgradeType.RangeAttack:
                case UpgradeType.RangeRange:
                    rangeStatSo?.Upgrade(upgradeType);
                    break;
                case UpgradeType.AntiArmorAttack:
                case UpgradeType.AntiArmorHealth:
                    antiArmorStatSo?.Upgrade(upgradeType);
                    break;
                case UpgradeType.ArmorAttack:
                case UpgradeType.ArmorHealth:
                    armorStatSo?.Upgrade(upgradeType);
                    break;
                case UpgradeType.TurretAttack:
                case UpgradeType.TurretRange:
                    turretStatSo?.Upgrade(upgradeType);
                    break;
                case UpgradeType.GoldGiven:
                    otherArmorStatSo?.Upgrade(upgradeType);
                    otherMeleeStatSo?.Upgrade(upgradeType);
                    otherRangeStatSo?.Upgrade(upgradeType);
                    otherAntiArmorStatSo?.Upgrade(upgradeType);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}