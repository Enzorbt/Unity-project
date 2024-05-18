using Common;
using Interfaces;
using Supinfo.Project.Scripts.Interfaces;
using Supinfo.Project.Turret.Scripts;
using Unity.VisualScripting;
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
            var tags = turretThinker.transform.tag.Split(",");
            
            // Detection d'enemies (loin)
            var target = detection?.Detect(tags[1]=="Allies"?"Unit,Enemies":"Unit,Allies", turretThinker.TurretStatSo.Range);
            if (target is not null)
            {
                Attack(turretThinker, target);
            }
        }

        protected void Attack(TurretThinker unitThinker, Collider2D target)
        {
            unitThinker.TryGetComponent(out IShooter shooter);
            shooter?.Shoot(
                unitThinker.TurretStatSo.Damage,
                unitThinker.TurretStatSo.HitSpeed, 
                10, 
                target.transform);
        }
    }
}