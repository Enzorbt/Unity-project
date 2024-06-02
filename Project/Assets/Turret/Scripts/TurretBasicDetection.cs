using Interfaces;
using Supinfo.Project.Scripts.Interfaces;
using UnityEngine;

namespace Supinfo.Project.Turret.Scripts
{
    /// <summary>
    /// The TurretBasicDetection class handles basic detection for turrets.
    /// It implements the IDetection interface.
    /// </summary>
    public class TurretBasicDetection : MonoBehaviour, IDetection
    {
        /// <summary>
        /// Detects colliders within a specified range that match the specified tag.
        /// </summary>
        /// <param name="detectTag">The tag of the objects to detect.</param>
        /// <param name="range">The range within which to detect.</param>
        /// <returns>The nearest collider detected within the range and matching the tag, or null if no collider was detected.</returns>
        public Collider2D Detect(string detectTag, float range)
        {
            // Get all colliders within the specified range
            var targets = Physics2D.OverlapCircleAll(transform.position, range);

            // Initialize variables to track the nearest collider and its distance
            Collider2D nearestTarget = null;
            float minDistance = float.MaxValue;

            // Iterate through the detected colliders
            foreach (var target in targets)
            {
                // Check if the target has the correct tag
                if (!target.CompareTag(detectTag))
                {
                    continue;
                }

                // Calculate the distance to the target
                var distance = Vector2.Distance(transform.position, target.transform.position);

                // Update the nearest target if this one is closer
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestTarget = target;
                }
            }

            return nearestTarget;
        }

    }
}