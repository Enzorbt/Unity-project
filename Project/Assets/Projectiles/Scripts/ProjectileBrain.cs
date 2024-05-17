using Common;
using Interfaces;
using Supinfo.Project.Scripts.Interfaces;
using UnityEngine;

namespace Supinfo.Project.Projectiles.Scripts
{
    [CreateAssetMenu(menuName = "Brains/ProjectileBrain")]
    public class ProjectileBrain: Brain
    {
        public override void Think(Thinker thinker)
        {
            if (thinker is not ProjectileThinker projectileThinker) return;
            projectileThinker.TryGetComponent(out IUnitDetection detection);
            var tags = projectileThinker.transform.tag.Split(",");
            
            // enemies detection
            var target = detection?.Detect(projectileThinker.Direction, 0.0001f, tags[1]=="Allies"?"Unit,Enemies":"Unit,Allies");
            if (target)
            {
                // attack the enemy
                projectileThinker.TryGetComponent(out IAttacker attacker);
                target.TryGetComponent(out IDamageable damageable);
                if(damageable is null) return;
                attacker?.Attack(projectileThinker.Damage, damageable, 0);
                
                // after attack, object gets destroyed
                Destroy(projectileThinker.gameObject);
                return;
            }
            
            // Mouvement basic
            projectileThinker.TryGetComponent(out IMovement movement);
            movement?.Move(projectileThinker.Direction, projectileThinker.Speed);
        }
    }
}