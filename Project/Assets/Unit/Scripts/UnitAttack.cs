using System;
using System.Collections;
using Supinfo.Project.Scripts.Events;
using Supinfo.Project.Scripts.Interfaces;
using Supinfo.Project.Scripts.ScriptableObjects.UnitTypes;
using UnityEngine;

namespace Supinfo.Project.Unit.Scripts.UnitCollision
{
    /// <summary>
    /// The UnitAttack class handles the attack behavior for units.
    /// It implements the IAttacker interface.
    /// </summary>
    public class UnitAttack : MonoBehaviour, IAttacker
    {
        /// <summary>
        /// Flag to determine if the unit can attack.
        /// </summary>
        private bool _canAttack;

        /// <summary>
        /// Reference to the Animator component.
        /// </summary>
        private Animator _animator;
        
        public GameEvent onPlaySound;
        
        [SerializeField]
        private AudioClip attackSound;

        /// <summary>
        /// Initializes the UnitAttack component.
        /// </summary>
        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        /// <summary>
        /// Attacks a target with the specified parameters.
        /// </summary>
        /// <param name="amount">The damage amount of the attack.</param>
        /// <param name="target">The target to attack.</param>
        /// <param name="cooldown">The cooldown time between attacks.</param>
        /// <param name="attackerType">The type of the unit performing the attack.</param>
        public void Attack(float amount, IDamageable target, float cooldown, UnitType attackerType)
        {
            if (_canAttack) return;
            StartCoroutine(AttackWithCooldown(amount, target, cooldown, attackerType));
            //_animator.SetBool("attack", true);
        }

        /// <summary>
        /// Coroutine to handle the attack cooldown and perform the attack.
        /// </summary>
        /// <param name="amount">The damage amount of the attack.</param>
        /// <param name="target">The target to attack.</param>
        /// <param name="cooldown">The cooldown time between attacks.</param>
        /// <param name="attackerType">The type of the unit performing the attack.</param>
        /// <returns>An IEnumerator to handle the coroutine.</returns>
        public IEnumerator AttackWithCooldown(float amount, IDamageable target, float cooldown, UnitType attackerType)
        {
            _canAttack = true;
            
            // Perform the attack on the target
            target.TakeDamage(amount, attackerType);
                

            // Set the attack animation
            _animator.SetTrigger("attack");

            // Wait for the cooldown duration
            onPlaySound?.Raise(this, attackSound);
            
            yield return new WaitForSeconds(cooldown);
            
            _canAttack = false;
        }
    }
}
