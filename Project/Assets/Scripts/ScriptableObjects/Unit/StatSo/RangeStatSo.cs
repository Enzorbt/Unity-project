using Supinfo.Project.Scripts;
using Supinfo.Project.Scripts.Interfaces.Upgrades;
using UnityEngine;

namespace ScriptableObjects.Unit.StatSo
{
    [CreateAssetMenu(menuName = "ScriptableObject/Stats/RangeUnitStat", order = 0)]
    public class RangeStatSo: UnitStatSo, IUpgradable
    {
        public void Upgrade(UpgradeType type)
        {
            switch (type)
            {
                case UpgradeType.GoldGiven:
                    currentGoldGivenUpgrade++;
                    break;
                case UpgradeType.RangeAttack:
                    currentAttackUpgrade++;
                    break;
                case UpgradeType.RangeRange:
                    currentRangeUpgrade++;
                    break;
                default:
                    break;
            }
        }
    }
}