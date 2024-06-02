using System.Collections;
using Supinfo.Project.Scripts.ScriptableObjects.UnitTypes;

namespace Supinfo.Project.Scripts.Interfaces
{
    /// <summary>
    /// Interface for objects that can attack other objects.
    /// </summary>
    public interface IAttacker
    {
        /// <summary>
        /// Attacks the target, dealing the specified amount of damage.
        /// This method does not have a built-in cooldown.
        /// </summary>
        /// <param name="amount">The amount of damage to deal.</param>
        /// <param name="target">The object to attack.</param>
        /// <param name="cooldown">The cooldown time for the attack. This parameter is included for consistency with the AttackWithCooldown method, but it has no effect in this method.</param>
        /// <param name="attackerType">The type of the attacker. This can be used to determine whether the attack is successful, or to apply type-specific effects.</param>
        public void Attack(float amount, IDamageable target, float cooldown, UnitType attackerType);

        /// <summary>
        /// Attacks the target, dealing the specified amount of damage.
        /// This method has a built-in cooldown.
        /// </summary>
        /// <param name="amount">The amount of damage to deal.</param>
        /// <param name="target">The object to attack.</param>
        /// <param name="cooldown">The cooldown time for the attack. The object will be unable to attack again until this time has elapsed.</param>
        /// <param name="attackerType">The type of the attacker. This can be used to determine whether the attack is successful, or to apply type-specific effects.</param>
        /// <returns>An IEnumerator that can be used to track the progress of the cooldown.</returns>
        public IEnumerator AttackWithCooldown(float amount, IDamageable target, float cooldown, UnitType attackerType);
    }
}