using Common;
using ScriptableObjects.Unit;
using Supinfo.Project.Scripts.ScriptableObjects.UnitTypes;
using UnityEngine;

namespace Supinfo.Project.Unit.Scripts
{
    /// <summary>
    /// The UnitThinker class represents the thinker component of a unit.
    /// Inherits from the Thinker class.
    /// </summary>
    public class UnitThinker : Thinker
    {
        // Stats - fixed (to use in brain)
        public float Damage { get; set; }
        public float WalkSpeed { get; set; }
        public float HitSpeed { get; set; }
        public float Range { get; set; }
        
        public UnitType UnitType { get; set; }
        
        /// <summary>
        /// The direction in which the unit is facing.
        /// </summary>
        public Vector3 Direction { get; set; }

        /// <summary>
        /// Initializes the UnitThinker component.
        /// </summary>
        private void Awake()
        {
            // Determine the initial direction based on the unit's position
            Direction = transform.position.x > 0 ? Vector3.left : Vector3.right;
        }

        /// <summary>
        /// Executes actions when the unit starts.
        /// </summary>
        private void Start()
        {
            // If the unit is tagged as "Unit,Enemies", flip its sprite horizontally
            if (gameObject.tag == "Unit,Enemies")
            {
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
                
                // Check if the SpriteRenderer component exists
                if (spriteRenderer != null)
                {
                    // Flip the sprite horizontally
                    spriteRenderer.flipX = !spriteRenderer.flipX;
                }
            }
        }
    }
}