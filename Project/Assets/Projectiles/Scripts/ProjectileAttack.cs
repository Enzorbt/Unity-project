using System.Collections;
using Supinfo.Project.Scripts.Interfaces;
using Supinfo.Project.Scripts.ScriptableObjects.UnitTypes;
using UnityEngine;

namespace Supinfo.Project.Projectiles.Scripts
{
    /// <summary>
    /// Handles the projectile's attack behavior.
    /// </summary>
    public class ProjectileAttack : MonoBehaviour, IAttacker
    {
        
        /// <summary>
        /// Flag to check if the projectile can attack.
        /// </summary>
        private bool _canAttack;

        /// <summary>
        /// Initiates an attack on a target if not on cooldown.
        /// </summary>
        /// <param name="amount">Amount of damage to inflict.</param>
        /// <param name="target">Target to attack.</param>
        /// <param name="cooldown">Cooldown duration after the attack.</param>
        /// <param name="attackerType">Type of the attacking unit.</param>
        public void Attack(float amount, IDamageable target, float cooldown, UnitType attackerType)
        {
            if (_canAttack) return;  // Return if currently attacking.
            StartCoroutine(AttackWithCooldown(amount, target, cooldown, attackerType));  // Start attack with cooldown coroutine.
        }

        /// <summary>
        /// Coroutine to handle the attack and cooldown.
        /// </summary>
        /// <param name="amount">Amount of damage to inflict.</param>
        /// <param name="target">Target to attack.</param>
        /// <param name="cooldown">Cooldown duration after the attack.</param>
        /// <param name="attackerType">Type of the attacking unit.</param>
        /// <returns>IEnumerator for the coroutine.</returns>
        public IEnumerator AttackWithCooldown(float amount, IDamageable target, float cooldown, UnitType attackerType)
        {
            _canAttack = true;  // Set attack flag to true.
            target.TakeDamage(amount, attackerType);  // Inflict damage on the target.

            yield return new WaitForSeconds(cooldown);  // Wait for the cooldown duration.
            
            _canAttack = false;  // Reset attack flag to false.
        }
    }
}
