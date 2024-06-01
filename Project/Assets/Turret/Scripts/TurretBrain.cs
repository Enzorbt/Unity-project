using Common;
using Interfaces;
using Supinfo.Project.Scripts.Interfaces;
using Supinfo.Project.Turret.Scripts;
using Unity.VisualScripting;
using UnityEngine;

namespace Turret.Scripts
{
    /// <summary>
    /// The TurretBrain class represents the brain of a turret.
    /// Inherits from the Brain class.
    /// </summary>
    [CreateAssetMenu(menuName = "Brains/TurretBrain")]
    public class TurretBrain : Brain
    {
        /// <summary>
        /// Handles the decision-making process of the turret.
        /// </summary>
        /// <param name="thinker">The thinker component of the turret.</param>
        public override void Think(Thinker thinker)
        {
            if (thinker is not TurretThinker turretThinker) return;
            turretThinker.TryGetComponent(out IDetection detection);
            var tags = turretThinker.transform.tag.Split(",");
            
            // Detection of enemies (far)
            var target = detection?.Detect(tags[1] == "Allies" ? "Unit,Enemies" : "Unit,Allies", turretThinker.TurretStatSo.Range);
            if (target is not null)
            {
                Attack(turretThinker, target);
            }
        }

        /// <summary>
        /// Handles the attack action of the turret.
        /// </summary>
        /// <param name="turretThinker">The thinker component of the turret.</param>
        /// <param name="target">The target collider detected by the turret.</param>
        protected void Attack(TurretThinker turretThinker, Collider2D target)
        {
            turretThinker.TryGetComponent(out IShooter shooter);
            shooter?.Shoot(
                turretThinker.TurretStatSo.Damage,
                turretThinker.TurretStatSo.HitSpeed, 
                10, 
                target.transform, 
                null
            );
        }
    }
}