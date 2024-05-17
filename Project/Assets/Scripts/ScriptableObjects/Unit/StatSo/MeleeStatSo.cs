using Supinfo.Project.Scripts;
using Supinfo.Project.Scripts.Interfaces.Upgrades;
using UnityEngine;

namespace ScriptableObjects.Unit.StatSo
{
    [CreateAssetMenu(menuName = "ScriptableObject/Stats/MeleeUnitStat", order = 0)]

    public class MeleeStatSo: UnitStatSo, IUpgradable
    {
        public void Upgrade(UpgradeType type)
        {
            switch (type)
            {
                case UpgradeType.MeleeHealth or UpgradeType.ArmorHealth or UpgradeType.AntiArmorHealth:
                    currentHealthUpgrade++;
                    break;
                case UpgradeType.GoldGiven:
                    currentGoldGivenUpgrade++;
                    break;
                case UpgradeType.MeleeAttack or UpgradeType.ArmorAttack or UpgradeType.AntiArmorAttack:
                    currentAttackUpgrade++;
                    break;
                default:
                    Debug.Log("Wrong type of upgrade");
                    break;
            }
        }
    }
}