using System;
using ScriptableObjects.Turret;
using ScriptableObjects.Unit.StatSo;
using UnityEngine;

namespace Supinfo.Project.Scripts
{
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
        // unlock armor is dealt in the armor button scripts
    }
    public class UpgradeManager : MonoBehaviour
    {
        [SerializeField] private MeleeStatSo meleeStatSo;
        [SerializeField] private MeleeStatSo antiArmorStatSo;
        [SerializeField] private MeleeStatSo armorStatSo;
        [SerializeField] private RangeStatSo rangeStatSo;
        [SerializeField] private TurretStatSo turretStatSo;
        
        [SerializeField] private MeleeStatSo otherMeleeStatSo;
        [SerializeField] private MeleeStatSo otherAntiArmorStatSo;
        [SerializeField] private MeleeStatSo otherArmorStatSo;
        [SerializeField] private RangeStatSo otherRangeStatSo;

        public void Upgrade(Component sender, object data)
        {
            if (data is not UpgradeType upgradeType) return;
            
            switch (upgradeType)
            {
                case UpgradeType.MeleeAttack:
                    meleeStatSo?.Upgrade(upgradeType);
                    break;
                case UpgradeType.MeleeHealth:
                    meleeStatSo?.Upgrade(upgradeType);
                    break;
                case UpgradeType.RangeAttack:
                    rangeStatSo?.Upgrade(upgradeType);
                    break;
                case UpgradeType.RangeRange:
                    rangeStatSo?.Upgrade(upgradeType);
                    break;
                case UpgradeType.AntiArmorAttack:
                    antiArmorStatSo?.Upgrade(upgradeType);
                    break;
                case UpgradeType.AntiArmorHealth:
                    antiArmorStatSo?.Upgrade(upgradeType);
                    break;
                case UpgradeType.ArmorAttack:
                    armorStatSo?.Upgrade(upgradeType);
                    break;
                case UpgradeType.ArmorHealth:
                    armorStatSo?.Upgrade(upgradeType);
                    break;
                case UpgradeType.TurretAttack:
                    turretStatSo?.Upgrade(upgradeType);
                    break;
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