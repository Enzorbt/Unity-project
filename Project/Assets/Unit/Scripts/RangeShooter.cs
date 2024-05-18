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
        
        private bool _canShoot = true;

        public void Shoot(float amount, float cooldown, float speed, Transform target)
        {
            if (!_canShoot) return;
            StartCoroutine(ShootWithCooldown(amount, cooldown, speed, target));

        }

        public IEnumerator ShootWithCooldown(float amount, float cooldown, float speed, Transform target)
        {
            _canShoot = false;
            if (projectile is null) yield break;
            
            // calcul de la position en fonction du sprite
            var sprite = projectile.GetComponentInChildren<SpriteRenderer>().sprite;
            var newPosition = transform.position;
            
            var direction = new Vector3((target.position - newPosition).normalized.x, 0, 0);
            
            newPosition.x += direction.x > 0 ? -sprite.bounds.extents.x : sprite.bounds.extents.x;
            
            var instantiatedProjectile = Instantiate(projectile, newPosition, Quaternion.identity, transform);

            // instantiate the projectile
            instantiatedProjectile.TryGetComponent(out ProjectileThinker projectileThinker);
            
            // change projectile params
            instantiatedProjectile.SetActive(false);
            
            if (projectileThinker is null) yield break;
            
            projectileThinker.Direction = direction;
            projectileThinker.Damage = amount;
            projectileThinker.Speed = speed;
            projectileThinker.tag = "Projectile," + gameObject.tag.Split(",")[1];
            
            instantiatedProjectile.SetActive(true);

            // wait for cooldown
            yield return new WaitForSeconds(cooldown);

            _canShoot = true;
        }
    }
}