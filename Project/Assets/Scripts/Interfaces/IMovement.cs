using UnityEngine;

namespace Supinfo.Project.Scripts.Interfaces
{
    /// <summary>
    /// Interface for objects that can move.
    /// </summary>
    public interface IMovement
    {
        /// <summary>
        /// Moves the object in the specified direction at the specified speed.
        /// </summary>
        /// <param name="direction">The direction in which to move the object.</param>
        /// <param name="speed">The speed at which to move the object.</param>
        public void Move(Vector3 direction, float speed);
    }
}