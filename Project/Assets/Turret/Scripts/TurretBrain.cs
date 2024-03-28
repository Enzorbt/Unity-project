using Common;
using Supinfo.Project.Scripts.Interfaces;
using Supinfo.Project.Turret.Scripts;
using UnityEngine;

namespace Turret.Scripts
{
    [CreateAssetMenu(menuName = "Brains/TurretBrain")]
    public class TurretBrain: Brain
    {
        public override void Think(Thinker thinker)
        {
            if (thinker is not TurretThinker turretThinker) return;
            turretThinker.TryGetComponent(out IDetection detection);
            if (detection is null) return ;
            var targetCollider = detection.Detect(turretThinker.TurretAttackSo.TargetTag, turretThinker.TurretAttackSo.Range);
            if (targetCollider is null) return;
            targetCollider.TryGetComponent(out IDamageable damageable);
            if (damageable is null) return;
            damageable.TakeDamage((int)turretThinker.TurretAttackSo.Damage);
        }
    }
}