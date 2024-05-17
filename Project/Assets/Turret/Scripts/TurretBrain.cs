using Common;
using Interfaces;
using Supinfo.Project.Scripts.Interfaces;
using Supinfo.Project.Unit.Scripts;
using Unity.VisualScripting;
using UnityEngine;

namespace Supinfo.Project.Turret.Scripts
{
    [CreateAssetMenu(menuName = "Brains/TurretBrain")]
    public class TurretBrain: Brain
    {
        public GameObject projectilePrefab;
        private Collider2D _targetCollider;
        public override void Think(Thinker thinker)
        {
            if (thinker is not TurretThinker turretThinker) return;
            turretThinker.TryGetComponent(out IDetection detection);
            if (detection is null) return ;
            string[] tags = thinker.transform.tag.Split(',');
            if (!_targetCollider)
            {
                _targetCollider = detection.Detect(tags[1] == "Allies" ? "Unit,Enemy": "Unit,Allies", 500);
            }
            //Debug.Log(tags[1] == "Allies" ? "Unit,Enemy": "Unit,Allies");
            //Debug.Log(targetCollider.transform.tag);
            /*targetCollider.TryGetComponent(out IDamageable damageable);
            if (damageable is null) return;
            damageable.TakeDamage((int)turretThinker.TurretAttackSo.Damage);*/
            if (_targetCollider)
            {
                GameObject newProjectile = Instantiate(projectilePrefab, turretThinker.transform.position, Quaternion.identity);
            
                ProjectileMovement projectileMovement = newProjectile.GetComponent<ProjectileMovement>();
                if (projectileMovement)
                {
                    //calcul position target
                    var distance = Vector3.Distance(thinker.transform.position, _targetCollider.transform.position);
                    var time = distance / projectileMovement.Speed;
                    _targetCollider.TryGetComponent(out UnitThinker targetThinker);
                    var distanceParcouru = time / targetThinker.UnitMovement.WalkSpeed;
                    if (tags[1] == "Allies")
                    {
                        distanceParcouru *= -1;
                    }
                    
                    //SetTarget(position)
                    var position = _targetCollider.transform.position + new Vector3(distanceParcouru, 0, 0);
                    projectileMovement.SetTarget(position);
                }
            }
        }
    }
}