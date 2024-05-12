using UnityEngine;

namespace Interfaces
{
    public interface IDetection
    {
        public Collider2D Detect(string detectTag, float range);
    }
}