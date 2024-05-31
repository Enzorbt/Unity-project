using System;
using System.Collections;
using Supinfo.Project.Scripts.Interfaces;
using Supinfo.Project.Scripts.ScriptableObjects.UnitTypes;
using UnityEngine;

namespace Supinfo.Project.Unit.Scripts.UnitCollision
{
    public class UnitAttack : MonoBehaviour, IAttacker
    {
        private bool _canAttack;
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void Attack(float amount, IDamageable target, float cooldown, UnitType attackerType)
        {
            if (_canAttack) return;
            StartCoroutine(AttackWithCooldown(amount, target, cooldown, attackerType));
            //_animator.SetBool("attack", true);
        }

        public IEnumerator AttackWithCooldown(float amount, IDamageable target, float cooldown, UnitType attackerType)
        {
            _canAttack = true;
            target.TakeDamage(amount, attackerType);
            _animator.SetBool("attack", true);
            
            yield return new WaitForSeconds(cooldown);
            
            _animator.SetBool("attack", false);
            _canAttack = false;
        }
    }
}