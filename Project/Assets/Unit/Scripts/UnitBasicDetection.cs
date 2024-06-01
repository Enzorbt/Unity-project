using Interfaces;
using Supinfo.Project.Scripts.Interfaces;
using UnityEngine;

namespace Supinfo.Project.Unit.Scripts
{
    /// <summary>
    /// The UnitBasicDetection class handles basic detection for units.
    /// It implements the IUnitDetection interface.
    /// </summary>
    public class UnitBasicDetection : MonoBehaviour, IUnitDetection
    {
        /// <summary>
        /// Reference to the Animator component.
        /// </summary>
        private Animator _animator;
        
        /// <summary>
        /// Initializes the UnitMovement component.
        /// </summary>
        private void Start()
        {
            _animator = GetComponent<Animator>();
        }
        
        /// <summary>
        /// Detects a collider within a specified range and direction that matches the specified tag.
        /// </summary>
        /// <param name="direction">The direction to perform the detection.</param>
        /// <param name="range">The range within which to detect.</param>
        /// <param name="detectTag">The tag of the object to detect.</param>
        /// <returns>The detected collider, or null if no collider was detected.</returns>
        public Collider2D Detect(Vector3 direction, float range, string detectTag)
        {
            // Perform a raycast in the specified direction and range
            var hits = Physics2D.RaycastAll(transform.position, direction, range);

            // Iterate through the hits
            foreach (var hit in hits)
            {
                // Set the walk animation
                _animator.SetBool("walk", false);
                
                // If there is no collider, break out of the loop
                if (hit.collider is null) break;

                // If the collider's tag matches the detectTag, return the collider
                if (hit.collider.CompareTag(detectTag))
                {
                    return hit.collider;
                }
            }

            // Return null if no matching collider was found
            return null;
        }
    }
}