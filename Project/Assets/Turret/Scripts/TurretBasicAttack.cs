using System.Collections;
using Interfaces;
using Supinfo.Project.Scripts.Interfaces;
using UnityEngine;

namespace Supinfo.Project.Turret.Scripts
{
    public class TurretBasicAttack : MonoBehaviour, IShooter
    {
        public void Shoot(float amount, string tags, float cooldown, Vector3 direction, float speed)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerator ShootWithCooldown(float amount, string tag, float cooldown, Vector3 direction, float speed)
        {
            throw new System.NotImplementedException();
        }
    }
}