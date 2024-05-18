using System.Collections;
using UnityEngine;

namespace Interfaces
{
    public interface IShooter
    {
        public void Shoot(float amount, string tags, float cooldown, Vector3 direction, float speed); // shoot at enemies
        public IEnumerator ShootWithCooldown(float amount, string tag, float cooldown, Vector3 direction, float speed);
    }
}