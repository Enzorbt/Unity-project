using System.Collections;

namespace Supinfo.Project.Scripts.Interfaces
{
    public interface IAttacker
    {
        public void Attack(float amount, IDamageable target, float cooldown); // Deals damage
        public IEnumerator AttackWithCooldown(float amount, IDamageable target, float cooldown);
    }
}