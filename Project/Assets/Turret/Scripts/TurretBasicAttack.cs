using System.Collections;
using Interfaces;
using Supinfo.Project.Projectiles.Scripts;
using Supinfo.Project.Scripts.Interfaces;
using UnityEngine;

namespace Supinfo.Project.Turret.Scripts
{
    public class TurretBasicAttack : MonoBehaviour, IShooter
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
            
            var scaledSpriteSize = projectile.transform.localScale * sprite.bounds.extents.x;

            newPosition.x += transform.position.x > 0 ? - scaledSpriteSize.x : scaledSpriteSize.x;
            
            // calcul de la rotation en fonction de la cible
            var enticipationTarget = target.position + Vector3.right;
            var angle = Mathf.Atan2(enticipationTarget.y - newPosition.y, enticipationTarget.x - newPosition.x) * Mathf.Rad2Deg;
            var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            
            var instantiatedProjectile = Instantiate(projectile, newPosition, rotation, transform);
            
            // instantiate the projectile
            instantiatedProjectile.TryGetComponent(out ProjectileThinker projectileThinker);

            // change projectile params
            instantiatedProjectile.SetActive(false);

            if (projectileThinker is null) yield break;

            projectileThinker.Direction = Vector3.right;
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
