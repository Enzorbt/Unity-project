using System.Collections;
using UnityEngine;

namespace Interfaces
{
    public interface IShooter
    {
        public void Shoot(float amount, float cooldown, float speed, Transform target); // shoot at enemies
        public IEnumerator ShootWithCooldown(float amount, float cooldown, float speed, Transform target);
    }
}