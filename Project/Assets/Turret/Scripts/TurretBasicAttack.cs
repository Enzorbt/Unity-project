using System.Collections;
using Interfaces;
using Supinfo.Project.Scripts.Interfaces;
using UnityEngine;

namespace Supinfo.Project.Turret.Scripts
{
    public class TurretBasicAttack : MonoBehaviour, IShooter
    {
        public void Shoot(float amount, float cooldown, float speed, Transform target)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerator ShootWithCooldown(float amount, float cooldown, float speed, Transform target)
        {
            throw new System.NotImplementedException();
        }
    }
}