using Interfaces;
using Supinfo.Project.Scripts.Interfaces;
using UnityEngine;

namespace Supinfo.Project.Unit.Scripts
{
    public class UnitBasicDetection : MonoBehaviour, IUnitDetection
    {
        public Collider2D Detect(Vector3 direction, float range, string detectTag)
        {
            var hits = Physics2D.RaycastAll(transform.position, direction, range);
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