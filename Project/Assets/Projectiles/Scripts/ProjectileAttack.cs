using System.Collections;
using Supinfo.Project.Scripts.Interfaces;
using UnityEngine;

namespace Supinfo.Project.Projectiles.Scripts
{
    public class ProjectileAttack: MonoBehaviour, IAttacker
    {
        private bool _canAttack;
        
        public void Attack(float amount, IDamageable target, float cooldown)
        {
            if (_canAttack) return;
            StartCoroutine(AttackWithCooldown(amount, target, cooldown));

        }

        public IEnumerator AttackWithCooldown(float amount, IDamageable target, float cooldown)
        {
            _canAttack = true;
            target.TakeDamage(amount);

            yield return new WaitForSeconds(cooldown);
            
            _canAttack = false;        }
    }
}