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
        public GameObject projectilePrefab;
        public override void Think(Thinker thinker)
        {
            if (thinker is not TurretThinker turretThinker) return;
            turretThinker.TryGetComponent(out IDetection detection);
            if (detection is null) return ;
            string[] tags = thinker.transform.tag.Split(',');
            var targetCollider = detection.Detect(tags[1] == "Allies" ? "Unit,Enemies": "Unit,Allies", 500);
            //Debug.Log(tags[1] == "Allies" ? "Unit,Enemy": "Unit,Allies");
            if (targetCollider is null) return;
            //Debug.Log(targetCollider.transform.tag);
            /*targetCollider.TryGetComponent(out IDamageable damageable);
            if (damageable is null) return;
            damageable.TakeDamage((int)turretThinker.TurretAttackSo.Damage);*/
            
            GameObject newProjectile = Instantiate(projectilePrefab, turretThinker.transform.position, Quaternion.identity);
            
            ProjectileMovement projectileMovement = newProjectile.GetComponent<ProjectileMovement>();
            if (projectileMovement != null)
            {
                projectileMovement.SetTarget(targetCollider.transform.position);
            }
        }
    }
}