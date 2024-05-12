using Interfaces;
using Supinfo.Project.Scripts.Interfaces;
using UnityEngine;

namespace Supinfo.Project.Unit.Scripts
{
    public class UnitBasicDetection : MonoBehaviour, IUnitDetection
    {
        public Collider2D Detect(Vector3 direction, float range, string detectTag)
        {
            var hit = Physics2D.Raycast(transform.position, direction, range);
            if (hit.collider is not null)
            {
                if (hit.collider.CompareTag(detectTag))
                {
                    return hit.collider;
                }
            }
            return null;
        }
    }
}