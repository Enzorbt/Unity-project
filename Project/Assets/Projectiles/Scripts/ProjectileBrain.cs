using Common;
using Interfaces;
using Supinfo.Project.Scripts.Interfaces;
using UnityEngine;

namespace Supinfo.Project.Projectiles.Scripts
{
    /// <summary>
    /// ScriptableObject that defines the behavior of a projectile.
    /// </summary>
    [CreateAssetMenu(menuName = "Brains/ProjectileBrain")]
    public class ProjectileBrain : Brain
    {
        /// <summary>
        /// Executes the behavior logic for the given thinker.
        /// </summary>
        /// <param name="thinker">The thinker component controlling the projectile.</param>
        public override void Think(Thinker thinker)
        {
            if (thinker is not ProjectileThinker projectileThinker) return;  // Validate thinker type.

            var tags = projectileThinker.transform.tag.Split(",");  // Split the tags associated with the projectile.
            
            // Enemy detection
            projectileThinker.TryGetComponent(out IUnitDetection detection);
            var target = detection?.Detect(projectileThinker.DetectionDirection, 0.1f, tags[1] == "Allies" ? "Unit,Enemies" : "Unit,Allies");  // Detect units.
            
            // Damage enemy if in range (distance)
            if (target is not null)
            {
                Attack(projectileThinker, target);  // Attack the detected target.
                return;
            }
            
            target = detection?.Detect(projectileThinker.DetectionDirection, 0.1f, tags[1] == "Allies" ? "Castle,Enemies" : "Castle,Allies");  // Detect castles.
            
            // Damage enemy if in range (distance)
            if (target is not null)
            {
                Attack(projectileThinker, target);  // Attack the detected target.
                return;
            }
            
            // Basic movement
            projectileThinker.TryGetComponent(out IMovement movement);
            movement?.Move(projectileThinker.Direction, projectileThinker.Speed);  // Move the projectile.

            if (projectileThinker.transform.position.y < -4f)
            {
                Destroy(projectileThinker.gameObject);  // Destroy the projectile if it goes below a certain position.
            }
        }

        /// <summary>
        /// Handles the attack logic for the projectile.
        /// </summary>
        /// <param name="projectileThinker">The projectile thinker component.</param>
        /// <param name="target">The target to attack.</param>
        private static void Attack(ProjectileThinker projectileThinker, Collider2D target)
        {
            // Attack the enemy
            projectileThinker.TryGetComponent(out IAttacker attacker);
            target.TryGetComponent(out IDamageable damageable);
            if (damageable is null) return;  // Return if the target is not damageable.
            attacker?.Attack(projectileThinker.Damage, damageable, 0, projectileThinker.UnitType);  // Inflict damage on the target.
                
            // After attack, object gets destroyed
            Destroy(projectileThinker.gameObject);  // Destroy the projectile after attacking.
        }
    }
}