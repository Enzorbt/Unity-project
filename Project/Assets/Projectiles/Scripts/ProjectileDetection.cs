using Interfaces;
using UnityEngine;

namespace Supinfo.Project.Projectiles.Scripts
{
    /// <summary>
    /// Handles the detection of units by projectiles.
    /// </summary>
    public class ProjectileDetection : MonoBehaviour, IUnitDetection
    {
        /// <summary>
        /// Detects a unit in the given direction within a specified range.
        /// </summary>
        /// <param name="direction">Direction to cast the ray.</param>
        /// <param name="range">Range of the detection.</param>
        /// <param name="detectTag">Tag of the units to detect.</param>
        /// <returns>Detected unit's collider, or null if none are detected.</returns>
        public Collider2D Detect(Vector3 direction, float range, string detectTag)
        {
            var hits = new RaycastHit2D[10];  // Array to store the raycast hits.
            Physics2D.RaycastNonAlloc(transform.position, direction, hits, range);  // Perform the raycast.
            foreach (var hit in hits)
            {
                if (hit.collider is null) break;  // Exit the loop if no more hits are detected.
                if (hit.collider.CompareTag(detectTag))
                {
                    return hit.collider;  // Return the collider if it matches the detect tag.
                }
            }
            return null;  // Return null if no matching colliders are found.
        }
    }
}