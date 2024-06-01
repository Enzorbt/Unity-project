using Supinfo.Project.Scripts.ScriptableObjects.UnitTypes;

namespace Supinfo.Project.Scripts.Interfaces
{
    /// <summary>
    /// Interface for objects that can take damage.
    /// </summary>
    public interface IDamageable
    {
        /// <summary>
        /// Causes the object to take the specified amount of damage.
        /// The type of the attacker can be used to determine whether the attack is successful, or to apply type-specific effects.
        /// </summary>
        /// <param name="amount">The amount of damage to take.</param>
        /// <param name="attackerType">The type of the attacker. This can be used to determine whether the attack is successful, or to apply type-specific effects.</param>
        public void TakeDamage(float amount, UnitType attackerType);
    }
}