using UnityEngine;

namespace Interfaces
{
    /// <summary>
    /// Interface for objects that can detect other objects.
    /// </summary>
    public interface IUnitDetection
    {
        /// <summary>
        /// Detects an object with the specified tag within the specified range in the specified direction.
        /// Returns the collider of the detected object, or null if no object is detected.
        /// </summary>
        /// <param name="direction">The direction in which to detect the object.</param>
        /// <param name="range">The maximum distance at which to detect the object.</param>
        /// <param name="detectTag">The tag of the object to detect.</param>
        /// <returns>The collider of the detected object, or null if no object is detected.</returns>
        public Collider2D Detect(Vector3 direction, float range, string detectTag);
    }
}