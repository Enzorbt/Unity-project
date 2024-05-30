using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Supinfo.Project.Scripts.ScriptableObjects.Upgrades
{
    [CreateAssetMenu(menuName = "ScriptableObject/Upgrades/UpgradePrices", order = 0)]
    public class UpgradePricesSo : ScriptableObject
    {
        [Header("Melee")]
        [SerializeField] protected List<float> meleeAttackPrices;
        [SerializeField] protected List<float> meleeHealthPrices;
        
        [Header("Armor")]
        [SerializeField] protected List<float> armorAttackPrices;
        [SerializeField] protected List<float> armorHealthPrices;
        
        [Header("AntiArmor")]
        [SerializeField] protected List<float> antiArmorAttackPrices;
        [SerializeField] protected List<float> antiArmorHealthPrices;
        
        [Header("Range")]
        [SerializeField] protected List<float> rangeAttackPrices;
        [SerializeField] protected List<float> rangeRangePrices;
        
        [Header("Turret")]
        [SerializeField] protected List<float> turretAttackPrices;
        [FormerlySerializedAs("turretTurretPrices")] [SerializeField] protected List<float> turretRangePrices;
        
        [Header("GoldGiven")]
        [SerializeField] protected List<float> goldGivenPrices;

        public float GetPrice(UpgradeType upgradeType, int index)
        {
            switch (upgradeType)
            {
                case UpgradeType.MeleeAttack:
                    return meleeAttackPrices[index];
                case UpgradeType.MeleeHealth:
                    return meleeHealthPrices[index];
                case UpgradeType.RangeAttack:
                    return rangeAttackPrices[index];
                case UpgradeType.RangeRange:
                    return rangeRangePrices[index];
                case UpgradeType.AntiArmorAttack:
                    return antiArmorAttackPrices[index];
                case UpgradeType.AntiArmorHealth:
                    return antiArmorHealthPrices[index];
                case UpgradeType.ArmorAttack:
                    return armorAttackPrices[index];
                case UpgradeType.ArmorHealth:
                    return armorHealthPrices[index];
                case UpgradeType.TurretAttack:
                    return turretAttackPrices[index];
                case UpgradeType.TurretRange:
                    return turretRangePrices[index];
                case UpgradeType.GoldGiven:
                    return goldGivenPrices[index];
                default:
                    throw new ArgumentOutOfRangeException(nameof(upgradeType), upgradeType, null);
            }
        }
    }
}