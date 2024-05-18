using System.Collections;
using Common;
using Interfaces;
using Supinfo.Project.Projectiles.Scripts;
using UnityEngine;

namespace Supinfo.Project.Unit.Scripts
{
    public class RangeShooter : MonoBehaviour, IShooter
    {
        [SerializeField] private GameObject projectile;
        
        private bool _canShoot;

        public void Shoot(float damage, string tags, float cooldown, Vector3 direction, float speed)
        {
            if (_canShoot) return;
            StartCoroutine(ShootWithCooldown(damage, tags, cooldown, direction, speed));

        }

        public IEnumerator ShootWithCooldown(float amount, string tag, float cooldown, Vector3 direction, float speed)
        {
            _canShoot = true;
            if (projectile is null) yield break;
            
            
            GameObject instantiatedProjectile = Instantiate(projectile, new Vector3(0,-1), Quaternion.identity, transform);

            instantiatedProjectile.TryGetComponent(out ProjectileThinker projectileThinker);
            if (projectileThinker is null) yield break;
            
            projectileThinker.Direction = direction;
            projectileThinker.Damage = amount;
            projectileThinker.Speed = speed;
            projectileThinker.tag = "Projectile," + tag;

            yield return new WaitForSeconds(cooldown);

            _canShoot = false;
        }
    }
}