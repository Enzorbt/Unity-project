using System.Collections;
using Supinfo.Project.Scripts.Interfaces;
using Supinfo.Project.Scripts.ScriptableObjects.UnitTypes;
using UnityEngine;

namespace Supinfo.Project.Unit.Scripts.UnitCollision
{
    public class UnitAttack : MonoBehaviour, IAttacker
    {
        private bool _canAttack;
        
        public void Attack(float amount, IDamageable target, float cooldown, UnitType attackerType)
        {
            if (_canAttack) return;
            StartCoroutine(AttackWithCooldown(amount, target, cooldown, attackerType));
        }

        public IEnumerator AttackWithCooldown(float amount, IDamageable target, float cooldown, UnitType attackerType)
        {
            _canAttack = true;
            target.TakeDamage(amount, attackerType);

            yield return new WaitForSeconds(cooldown);
            
            _canAttack = false;        
        }
    }
}