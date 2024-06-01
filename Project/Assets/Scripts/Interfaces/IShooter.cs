using System.Collections;
using Supinfo.Project.Scripts.ScriptableObjects.UnitTypes;
using UnityEngine;

namespace Interfaces
{
    /// <summary>
    /// Interface for objects that can shoot at other objects.
    /// </summary>
    public interface IShooter
    {
        /// <summary>
        /// Shoots at the target, dealing the specified amount of damage.
        /// This method does not have a built-in cooldown.
        /// </summary>
        /// <param name="amount">The amount of damage to deal.</param>
        /// <param name="cooldown">The cooldown time for the shoot. This parameter is included for consistency with the ShootWithCooldown method, but it has no effect in this method.</param>
        /// <param name="speed">The speed of the shot.</param>
        /// <param name="target">The object to shoot at.</param>
        /// <param name="attackerType">The type of the attacker. This can be used to determine whether the shot is successful, or to apply type-specific effects.</param>
        public void Shoot(float amount, float cooldown, float speed, Transform target, UnitType attackerType);

        /// <summary>
        /// Shoots at the target, dealing the specified amount of damage.
        /// This method has a built-in cooldown.
        /// </summary>
        /// <param name="amount">The amount of damage to deal.</param>
        /// <param name="cooldown">The cooldown time for the shoot. The object will be unable to shoot again until this time has elapsed.</param>
        /// <param name="speed">The speed of the shot.</param>
        /// <param name="target">The object to shoot at.</param>
        /// <param name="attackerType">The type of the attacker. This can be used to determine whether the shot is successful, or to apply type-specific effects.</param>
        /// <returns>An IEnumerator that can be used to track the progress of the cooldown.</returns>
        public IEnumerator ShootWithCooldown(float amount, float cooldown, float speed, Transform target, UnitType attackerType);
    }
}
