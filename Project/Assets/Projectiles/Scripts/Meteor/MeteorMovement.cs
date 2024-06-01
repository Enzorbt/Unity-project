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
        
        private void Update()
        {
            // Move the meteor downward at a constant speed.
            transform.Translate(speed * Time.deltaTime * Vector3.down);
            
            // Destroy the meteor if it moves below a certain position.
            if (transform.position.y < 0)
            {
                Destroy(gameObject);  // Destroy the meteor game object.
            }
        }
    }
}