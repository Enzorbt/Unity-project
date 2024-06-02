using Supinfo.Project.Scripts.Events;
using UnityEngine;

namespace Supinfo.Project.Projectiles.Scripts.Meteor
{
    /// <summary>
    /// Controls the movement of the meteor projectile.
    /// </summary>
    public class MeteorMovement : MonoBehaviour
    {
        /// <summary>
        /// Speed at which the meteor moves.
        /// </summary>
        [SerializeField] private float speed;
        
        /// <summary>
        /// The game event that trigger the sound.
        /// </summary>
        public GameEvent onPlaySound;
        
        /// <summary>
        /// The audioclip to play when thunder capacity is used.
        /// </summary>
        [SerializeField] private AudioClip meteorSound;
        
        private void Update()
        {
            // Move the meteor downward at a constant speed.
            transform.Translate(speed * Time.deltaTime * Vector3.down);
            
            // Destroy the meteor if it moves below a certain position.
            if (transform.position.y < 0)
            {
                onPlaySound?.Raise(this, meteorSound);
                Destroy(gameObject);  // Destroy the meteor game object.
            }
        }
    }
}