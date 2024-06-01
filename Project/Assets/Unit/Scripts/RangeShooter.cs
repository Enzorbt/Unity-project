using System.Collections;
using Common;
using Interfaces;
using Supinfo.Project.Projectiles.Scripts;
using Supinfo.Project.Scripts.Events;
using Supinfo.Project.Scripts.ScriptableObjects.UnitTypes;
using UnityEngine;

namespace Supinfo.Project.Unit.Scripts
{
    public class RangeShooter : MonoBehaviour, IShooter
    {
        private Animator _animator;
        
        [SerializeField] private GameObject projectile;
        public GameEvent onPlaySound;
        [SerializeField]
        private AudioClip attackSound;
        
        private bool _canShoot = true;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        public void Shoot(float amount, float cooldown, float speed, Transform target, UnitType attackerType)
        {
            if (!_canShoot) return;
            StartCoroutine(ShootWithCooldown(amount, cooldown, speed, target, attackerType));
            //_animator.SetBool("attack", true);
        }

        public IEnumerator ShootWithCooldown(float amount, float cooldown, float speed, Transform target, UnitType attackerType)
        {
            _canShoot = false;
            if (projectile is null) yield break;
            
            // calcul de la position en fonction du sprite
            var sprite = projectile.GetComponentInChildren<SpriteRenderer>().sprite;
            var newPosition = transform.position;
            
            // direction is initialized for calculation rotation
            var detectionDirection = new Vector3((target.position - newPosition).normalized.x, 0, 0);
            
            var rotation = detectionDirection.x > 0 ? Quaternion.identity : Quaternion.Euler(0, 180, 0);
            
            var scaledSpriteSize = projectile.transform.localScale * sprite.bounds.extents.x; 
            
            newPosition.x += detectionDirection.x > 0 ? - scaledSpriteSize.x : scaledSpriteSize.x;
            
            var instantiatedProjectile = Instantiate(projectile, newPosition, rotation);
            
            // set the layer
            instantiatedProjectile.layer =  3;
            
            // instantiate the projectile
            instantiatedProjectile.TryGetComponent(out ProjectileThinker projectileThinker);
            
            // change projectile params
            instantiatedProjectile.SetActive(false);
            
            if (projectileThinker is null) yield break;
            
            projectileThinker.Direction = Vector3.right;
            projectileThinker.DetectionDirection = detectionDirection;
            projectileThinker.Damage = amount;
            projectileThinker.Speed = speed;
            projectileThinker.UnitType = attackerType;
            projectileThinker.tag = "Projectile," + gameObject.tag.Split(",")[1];
            
            instantiatedProjectile.SetActive(true);
            _animator.SetBool("attack", true);
            onPlaySound?.Raise(this, attackSound);
            // wait for cooldown
            yield return new WaitForSeconds(cooldown);
            _animator.SetBool("attack", false);

            
            _canShoot = true;
        }
    }
}