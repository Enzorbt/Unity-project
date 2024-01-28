using Supinfo.Project.ScriptableObjects.Common;
using UnityEngine;

namespace ScriptableObjects.Turret
{
    [CreateAssetMenu(fileName = "TurretAttackSo", menuName = "ScriptableObject/Turrets/TurretAttackSo")]
    public class TurretAttackSo : AttackSo
    {
        private void OnEnable()
        {
            currentAge = 0;
            currentAttackUpgrade = 0;
            currentRangeUpgrade = 0;
        }
    }
}