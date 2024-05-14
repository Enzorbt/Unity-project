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
                    currentHealthUpgrade++;
                    break;
                case UpgradeType.Health:
                    currentGoldGivenUpgrade++;
                    break;
                case UpgradeType.Attack:
                    currentAttackUpgrade++;
                    break;
                case UpgradeType.Range:
                    currentRangeUpgrade++;
                    break;
                default:
                    Debug.Log("Wrong type of upgrade");
                    break;
            }
        }
    }
}