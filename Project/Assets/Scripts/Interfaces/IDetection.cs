using UnityEngine;

namespace Interfaces
{
    /// <summary>
    /// Interface for objects that can detect other objects.
    /// </summary>
    public interface IDetection
    {
        /// <summary>
        /// Detects an object with the specified tag within the specified range.
        /// Returns the collider of the detected object, or null if no object is detected.
        /// </summary>
        /// <param name="detectTag">The tag of the object to detect.</param>
        /// <param name="range">The range within which to detect the object. The detection is performed from the position of the object that implements this interface.</param>
        /// <returns>The collider of the detected object, or null if no object is detected.</returns>
        public Collider2D Detect(string detectTag, float range);
    }
}