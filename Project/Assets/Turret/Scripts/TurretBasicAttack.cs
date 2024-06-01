using System.Collections;
using Interfaces;
using Supinfo.Project.Projectiles.Scripts;
using Supinfo.Project.Scripts.Events;
using Supinfo.Project.Scripts.ScriptableObjects.UnitTypes;
using UnityEngine;

namespace Supinfo.Project.Turret.Scripts
{
    /// <summary>
    /// The TurretBasicAttack class handles the basic attack behavior for turrets.
    /// It implements the IShooter interface.
    /// </summary>
    public class TurretBasicAttack : MonoBehaviour, IShooter
    {
        /// <summary>
        /// The projectile prefab to be shot.
        /// </summary>
        [SerializeField] private GameObject projectile;

        public GameEvent onPlaySound;
        [SerializeField]
        private AudioClip attackSound;
        
        /// <summary>
        /// Flag to determine if the turret can shoot.
        /// </summary>
        private bool _canShoot = true;

        /// <summary>
        /// Shoots a projectile at the target.
        /// </summary>
        /// <param name="amount">The amount of damage the projectile deals.</param>
        /// <param name="cooldown">The cooldown time between shots.</param>
        /// <param name="speed">The speed of the projectile.</param>
        /// <param name="target">The target at which to shoot.</param>
        /// <param name="attackerType">The type of the unit performing the attack.</param>
        public void Shoot(float amount, float cooldown, float speed, Transform target, UnitType attackerType)
        {
            if (!_canShoot) return;
            StartCoroutine(ShootWithCooldown(amount, cooldown, speed, target, attackerType));
        }

        /// <summary>
        /// Coroutine to handle the cooldown between shots and shooting projectiles.
        /// </summary>
        /// <param name="amount">The amount of damage the projectile deals.</param>
        /// <param name="cooldown">The cooldown time between shots.</param>
        /// <param name="speed">The speed of the projectile.</param>
        /// <param name="target">The target at which to shoot.</param>
        /// <param name="attackerType">The type of the unit performing the attack.</param>
        /// <returns>An IEnumerator to handle the coroutine.</returns>
        public IEnumerator ShootWithCooldown(float amount, float cooldown, float speed, Transform target, UnitType attackerType)
        {
            _canShoot = false;

            // Return if the projectile prefab is null
            if (projectile is null) yield break;

            // Calculate the position based on the sprite
            var sprite = projectile.GetComponentInChildren<SpriteRenderer>().sprite;
            var newPosition = transform.position;
            
            var scaledSpriteSize = projectile.transform.localScale * sprite.bounds.extents.x;
            newPosition.x += transform.position.x > 0 ? -scaledSpriteSize.x : scaledSpriteSize.x;

            // Calculate the rotation based on the target
            var angle = Mathf.Atan2(target.position.y - newPosition.y, target.position.x - newPosition.x) * Mathf.Rad2Deg;
            var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            
            var angleInRadians = angle * Mathf.Deg2Rad;

            var detectionDirection = new Vector3(Mathf.Cos(angleInRadians), Mathf.Sin(angleInRadians), 0f);

            // Instantiate the projectile
            var instantiatedProjectile = Instantiate(projectile, newPosition, rotation, transform);
            instantiatedProjectile.SetActive(false);

            // Set the projectile's parameters
            instantiatedProjectile.TryGetComponent(out ProjectileThinker projectileThinker);
            if (projectileThinker is null) yield break;

            projectileThinker.Direction = Vector3.right;
            projectileThinker.DetectionDirection = detectionDirection;
            projectileThinker.Damage = amount;
            projectileThinker.Speed = speed;
            projectileThinker.UnitType = attackerType;
            projectileThinker.tag = "Projectile," + gameObject.tag.Split(",")[1];

            instantiatedProjectile.SetActive(true);
            
            onPlaySound?.Raise(this, attackSound);

            // Wait for the cooldown duration
            yield return new WaitForSeconds(cooldown);

            _canShoot = true;
        }
    }
}
