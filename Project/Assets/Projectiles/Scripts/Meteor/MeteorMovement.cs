using System;
using UnityEngine;

namespace Supinfo.Project.Projectiles.Scripts.Meteor
{
    public class MeteorMovement : MonoBehaviour
    {
        [SerializeField] private float speed;

        private void Update()
        {
            transform.Translate(speed * Time.deltaTime * Vector3.down);
            
            if (transform.position.y < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}