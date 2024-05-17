using Interfaces;
using UnityEngine;

namespace Supinfo.Project.Projectiles.Scripts
{
    public class ProjectileDetection : MonoBehaviour, IUnitDetection
    {
        public Collider2D Detect(Vector3 direction, float range, string detectTag)
        {
            var hit = Physics2D.Raycast(transform.position, direction, range);
            if (hit.collider is null) return null;
            return hit.collider.CompareTag(detectTag) ? hit.collider : null;
        }
    }
}