using Supinfo.Project.Scripts.Interfaces;
using UnityEngine;

namespace Supinfo.Project.Projectiles.Scripts
{
    /// <summary>
    /// Handles the movement of the projectile.
    /// </summary>
    public class ProjectileMovement : MonoBehaviour, IMovement
    {
        /// <summary>
        /// Moves the projectile in the given direction at the specified speed.
        /// </summary>
        /// <param name="direction">Direction to move the projectile.</param>
        /// <param name="speed">Speed at which the projectile moves.</param>
        public void Move(Vector3 direction, float speed)
        {
            transform.Translate(speed * Time.deltaTime * direction);  // Move the projectile.
        }
    }
}