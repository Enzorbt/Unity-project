using Common;
using Supinfo.Project.Scripts.ScriptableObjects.UnitTypes;
using UnityEngine;

namespace Supinfo.Project.Projectiles.Scripts
{
    /// <summary>
    /// Controls the behavior and properties of a projectile.
    /// </summary>
    public class ProjectileThinker : Thinker
    {
        /// <summary>
        /// Speed of the projectile.
        /// </summary>
        public float Speed { get; set; }
        
        /// <summary>
        /// Damage inflicted by the projectile.
        /// </summary>
        public float Damage { get; set; }

        /// <summary>
        /// Direction in which the projectile is moving.
        /// </summary>
        public Vector3 Direction { get; set; }
        
        /// <summary>
        /// Type of unit associated with the projectile.
        /// </summary>
        public UnitType UnitType { get; set; }
        
        /// <summary>
        /// Direction used for detection.
        /// </summary>
        public Vector3 DetectionDirection { get; set; }
    }
}