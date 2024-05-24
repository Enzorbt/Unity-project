using Common;
using Interfaces;
using Supinfo.Project.Scripts.Interfaces;
using Supinfo.Project.Unit.Scripts;
using UnityEngine;

namespace Supinfo.Project.Projectiles.Scripts
{
    [CreateAssetMenu(menuName = "Brains/ProjectileBrain")]
    public class ProjectileBrain: Brain
    {
        public override void Think(Thinker thinker)
        {
            if (thinker is not ProjectileThinker projectileThinker) return;

            var tags = projectileThinker.transform.tag.Split(",");
            
            // enemy detection
            projectileThinker.TryGetComponent(out IUnitDetection detection);
            var target = detection?.Detect(projectileThinker.Direction, 0.1f, tags[1]=="Allies"?"Unit,Enemies":"Unit,Allies");            // damage enemy if in range (distance)
            
            if (target is not null)
            {
                Attack(projectileThinker, target);
                return;
            }
            target = detection?.Detect(projectileThinker.Direction, 0.1f, tags[1]=="Allies"?"Castle,Enemies":"Castle,Allies");            // damage enemy if in range (distance)
            
            if (target is not null)
            {
                Attack(projectileThinker, target);
                return;
            }
            
            // Mouvement basic
            projectileThinker.TryGetComponent(out IMovement movement);
            movement?.Move(projectileThinker.Direction, projectileThinker.Speed);

            if (projectileThinker.transform.position.y < -2.5f)
            {
                Destroy(projectileThinker.gameObject);
            }
        }

        private static void Attack(ProjectileThinker projectileThinker, Collider2D target)
        {
            // attack the enemy
            projectileThinker.TryGetComponent(out IAttacker attacker);
            target.TryGetComponent(out IDamageable damageable);
            if(damageable is null) return;
            attacker?.Attack(projectileThinker.Damage, damageable, 0, projectileThinker.UnitType);
                
            // after attack, object gets destroyed
            Destroy(projectileThinker.gameObject);
        }
    }
}