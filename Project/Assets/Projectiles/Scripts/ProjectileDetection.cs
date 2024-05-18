using System.Collections.Generic;
using Interfaces;
using UnityEngine;

namespace Supinfo.Project.Projectiles.Scripts
{
    public class ProjectileDetection : MonoBehaviour, IUnitDetection
    {
        public Collider2D Detect(Vector3 direction, float range, string detectTag)
        {
            var hits = new RaycastHit2D[10];
            Physics2D.RaycastNonAlloc(transform.position, direction, hits, range);
            foreach (var hit in hits)
            {
                if (hit.collider is null) break;
                if (hit.collider.CompareTag(detectTag))
                {
                    return hit.collider;
                }
            }
            return null;
        }
    }
}