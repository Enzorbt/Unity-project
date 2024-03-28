using System;
using UnityEngine;

namespace Supinfo.Project.Turret.Scripts
{
    public class ProjectileMovement : MonoBehaviour
    {
        public float speed = 10f;
        private Vector3 targetPosition;
        
        public void SetTarget(Vector3 target)
        {
            targetPosition = target;
        }

        private void Update()
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                Destroy(gameObject);
            }
        }
    }
}