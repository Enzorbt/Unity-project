using System.Collections;
using Common;
using Interfaces;
using Supinfo.Project.Projectiles.Scripts;
using Supinfo.Project.Scripts.Events;
using Supinfo.Project.Scripts.ScriptableObjects.UnitTypes;
using UnityEngine;

namespace Supinfo.Project.Unit.Scripts
{
    /// <summary>
    /// The RangeShooter class handles shooting projectiles for ranged units.
    /// It implements the IShooter interface.
    /// </summary>
    public class RangeShooter : MonoBehaviour, IShooter
    {
        /// <summary>
        /// Reference to the Animator component.
        /// </summary>
        private Animator _animator;

        /// <summary>
        /// The projectile GameObject to be instantiated.
        /// </summary>
        [SerializeField] private GameObject projectile;
        public GameEvent onPlaySound;
        [SerializeField]
        private AudioClip attackSound;

        /// <summary>
        /// Flag to determine if the unit can shoot.
        /// </summary>
        private bool _canShoot = true;

        /// <summary>
        /// Initializes the RangeShooter component.
        /// </summary>
        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        /// <summary>
        /// Shoots a projectile towards a target.
        /// </summary>
        /// <param name="amount">The damage amount of the projectile.</param>
        /// <param name="cooldown">The cooldown time between shots.</param>
        /// <param name="speed">The speed of the projectile.</param>
        /// <param name="target">The target to shoot at.</param>
        /// <param name="attackerType">The type of the unit shooting the projectile.</param>
        public void Shoot(float amount, float cooldown, float speed, Transform target, UnitType attackerType)
        {
            if (!_canShoot) return;
            StartCoroutine(ShootWithCooldown(amount, cooldown, speed, target, attackerType));
            //_animator.SetBool("attack", true);
        }

        /// <summary>
        /// Coroutine to handle the shooting cooldown and projectile instantiation.
        /// </summary>
        /// <param name="amount">The damage amount of the projectile.</param>
        /// <param name="cooldown">The cooldown time between shots.</param>
        /// <param name="speed">The speed of the projectile.</param>
        /// <param name="target">The target to shoot at.</param>
        /// <param name="attackerType">The type of the unit shooting the projectile.</param>
        /// <returns>An IEnumerator to handle the coroutine.</returns>
        public IEnumerator ShootWithCooldown(float amount, float cooldown, float speed, Transform target, UnitType attackerType)
        {
            _canShoot = false;
            if (projectile is null) yield break;

            // Calculate the new position based on the sprite size
            var sprite = projectile.GetComponentInChildren<SpriteRenderer>().sprite;
            var newPosition = transform.position;

            // Initialize direction for rotation calculation
            var detectionDirection = new Vector3((target.position - newPosition).normalized.x, 0, 0);

            // Determine the rotation based on direction
            var rotation = detectionDirection.x > 0 ? Quaternion.identity : Quaternion.Euler(0, 180, 0);

            // Calculate scaled sprite size
            var scaledSpriteSize = projectile.transform.localScale * sprite.bounds.extents.x;

            // Adjust new position based on direction
            newPosition.x += detectionDirection.x > 0 ? -scaledSpriteSize.x : scaledSpriteSize.x;

            // Instantiate the projectile
            var instantiatedProjectile = Instantiate(projectile, newPosition, rotation);

            // Set the projectile layer
            instantiatedProjectile.layer = 3;

            // Try to get the ProjectileThinker component
            instantiatedProjectile.TryGetComponent(out ProjectileThinker projectileThinker);

            // Deactivate the projectile initially
            instantiatedProjectile.SetActive(false);

            if (projectileThinker is null) yield break;

            // Set projectile parameters
            projectileThinker.Direction = Vector3.right;
            projectileThinker.DetectionDirection = detectionDirection;
            projectileThinker.Damage = amount;
            projectileThinker.Speed = speed;
            projectileThinker.UnitType = attackerType;
            projectileThinker.tag = "Projectile," + gameObject.tag.Split(",")[1];

            // Activate the projectile
            instantiatedProjectile.SetActive(true);

            // Set the attack animation
            _animator.SetBool("attack", true);
            onPlaySound?.Raise(this, attackSound);

            // Wait for the cooldown duration
            yield return new WaitForSeconds(cooldown);

            // Reset the attack animation
            _animator.SetBool("attack", false);

            
            _canShoot = true;
        }
    }
}
