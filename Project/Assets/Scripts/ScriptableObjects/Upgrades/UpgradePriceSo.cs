using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Supinfo.Project.Scripts.ScriptableObjects.Upgrades
{
    /// <summary>
    /// The UpgradePricesSo class is a ScriptableObject that stores the prices for various upgrades.
    /// Each type of upgrade (e.g., melee, armor, anti-armor, range, turret, gold given) has its own list of prices.
    /// </summary>
    [CreateAssetMenu(menuName = "ScriptableObject/Upgrades/UpgradePrices", order = 0)]
    public class UpgradePricesSo : ScriptableObject
    {
        /// <summary>
        /// List of prices for melee attack upgrades.
        /// </summary>
        [Header("Melee")]
        [SerializeField] protected List<float> meleeAttackPrices;
        
        /// <summary>
        /// List of prices for melee health upgrades.
        /// </summary>
        [SerializeField] protected List<float> meleeHealthPrices;
        
        /// <summary>
        /// List of prices for armor attack upgrades.
        /// </summary>
        [Header("Armor")]
        [SerializeField] protected List<float> armorAttackPrices;
        
        /// <summary>
        /// List of prices for armor health upgrades.
        /// </summary>
        [SerializeField] protected List<float> armorHealthPrices;
        
        /// <summary>
        /// List of prices for anti-armor attack upgrades.
        /// </summary>
        [Header("AntiArmor")]
        [SerializeField] protected List<float> antiArmorAttackPrices;
        
        /// <summary>
        /// List of prices for anti-armor health upgrades.
        /// </summary>
        [SerializeField] protected List<float> antiArmorHealthPrices;
        
        /// <summary>
        /// List of prices for range attack upgrades.
        /// </summary>
        [Header("Range")]
        [SerializeField] protected List<float> rangeAttackPrices;
        
        /// <summary>
        /// List of prices for range range upgrades.
        /// </summary>
        [SerializeField] protected List<float> rangeRangePrices;
        
        /// <summary>
        /// List of prices for turret attack upgrades.
        /// </summary>
        [Header("Turret")]
        [SerializeField] protected List<float> turretAttackPrices;
        
        /// <summary>
        /// List of prices for turret range upgrades.
        /// </summary>
        [FormerlySerializedAs("turretTurretPrices")] 
        [SerializeField] protected List<float> turretRangePrices;
        
        /// <summary>
        /// List of prices for gold given upgrades.
        /// </summary>
        [Header("GoldGiven")]
        [SerializeField] protected List<float> goldGivenPrices;

        /// <summary>
        /// Retrieves the price of a specified upgrade type at a given index.
        /// </summary>
        /// <param name="upgradeType">The type of the upgrade.</param>
        /// <param name="index">The index of the price in the list.</param>
        /// <returns>The price of the upgrade at the specified index.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown when the upgrade type is not recognized.</exception>
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