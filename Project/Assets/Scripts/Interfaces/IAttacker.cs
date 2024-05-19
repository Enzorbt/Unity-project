using System.Collections;
using Supinfo.Project.Scripts.ScriptableObjects.UnitTypes;

namespace Supinfo.Project.Scripts.Interfaces
{
    public interface IAttacker
    {
        public void Attack(float amount, IDamageable target, float cooldown, UnitType attackerType); // Deals damage
        public IEnumerator AttackWithCooldown(float amount, IDamageable target, float cooldown, UnitType attackerType);
    }
}