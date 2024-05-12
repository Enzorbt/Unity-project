using UnityEngine;

namespace Interfaces
{
    public interface IUnitDetection
    {
        public Collider2D Detect(Vector3 direction, float range, string detectTag);
    }
}