using System.Collections;
using Common;
using Interfaces;
using Supinfo.Project.Projectiles.Scripts;
using Supinfo.Project.Scripts.ScriptableObjects.UnitTypes;
using UnityEngine;

namespace Supinfo.Project.Unit.Scripts
{
    public class RangeShooter : MonoBehaviour, IShooter
    {
        [SerializeField] private GameObject projectile;
        
        private bool _canShoot = true;

        public void Shoot(float amount, float cooldown, float speed, Transform target, UnitType attackerType)
        {
            if (!_canShoot) return;
            StartCoroutine(ShootWithCooldown(amount, cooldown, speed, target, attackerType));
        }

        public IEnumerator ShootWithCooldown(float amount, float cooldown, float speed, Transform target, UnitType attackerType)
        {
            _canShoot = false;
            if (projectile is null) yield break;
            
            // calcul de la position en fonction du sprite
            var sprite = projectile.GetComponentInChildren<SpriteRenderer>().sprite;
            var newPosition = transform.position;
            
            var direction = new Vector3((target.position - newPosition).normalized.x, 0, 0);

            var scaledSpriteSize = projectile.transform.localScale * sprite.bounds.extents.x; 
            
            newPosition.x += direction.x > 0 ? - scaledSpriteSize.x : scaledSpriteSize.x;
            
            var instantiatedProjectile = Instantiate(projectile, newPosition, Quaternion.identity, transform);

            // set the layer
            instantiatedProjectile.layer =  3;
            
            // instantiate the projectile
            instantiatedProjectile.TryGetComponent(out ProjectileThinker projectileThinker);
            
            // change projectile params
            instantiatedProjectile.SetActive(false);
            
            if (projectileThinker is null) yield break;
            
            projectileThinker.Direction = direction;
            projectileThinker.Damage = amount;
            projectileThinker.Speed = speed;
            projectileThinker.UnitType = attackerType;
            projectileThinker.tag = "Projectile," + gameObject.tag.Split(",")[1];
            
            instantiatedProjectile.SetActive(true);

            // wait for cooldown
            yield return new WaitForSeconds(cooldown);

            _canShoot = true;
        }
    }
}