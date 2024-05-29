using System.Collections;
using Interfaces.Capacity;
using Supinfo.Project.Scripts.Interfaces;
using Supinfo.Project.Scripts.ScriptableObjects.Capacity;
using UnityEngine;
using UnityEngine.Serialization;

namespace Capacity.Scripts
{
    public class SpecialCapacity : MonoBehaviour, ICapacity
    {
        [SerializeField] private GameObject leftMostPoint;
        [SerializeField] private GameObject rightMostPoint;
        [SerializeField] private GameObject lightningSymbolHolder;
        
        private bool _running;
        public void LaunchCapacity(Component sender, object data)
        {
            if (data is not CapacitySo capacitySo) return;

            if (!_running)
            {
                StartCoroutine(CapacityDelayed(capacitySo));
            }
        }

        private IEnumerator CapacityDelayed(CapacitySo capacitySo)
        {
            _running = true;
            
            // launch animation
            // capacity 1 (with falling meteors)
            if (capacitySo.CapabilityType == CapabilityType.Meteor)
            {
                for (int i = 0; i < 10; i++)
                {
                    var length = rightMostPoint.transform.position.x - leftMostPoint.transform.position.x;
                    var posX = leftMostPoint.transform.position.x + Random.value * length;
                    var pos = new Vector3(posX, rightMostPoint.transform.position.y);
                    var instantiatedObject = Instantiate(capacitySo.Prefab, pos, Quaternion.identity);
                    
                    if (instantiatedObject.TryGetComponent(out SpriteRenderer spriteRenderer))
                    {
                        spriteRenderer.sprite = capacitySo.Sprite;
                    }
                }
            }
            // capacity 2 (with just a lighting symbol)
            else if (capacitySo.CapabilityType == CapabilityType.Lightning)
            {
                StartCoroutine(LightningAnim());
            }
            
            // damage units
            var allUnits = GameObject.FindGameObjectsWithTag("Unit," + capacitySo.Tag);

            foreach (var unit in allUnits)
            {
                if (!unit.TryGetComponent(out IDamageable damageable)) break;
                if (Random.value < capacitySo.HitProbability)
                {
                    damageable.TakeDamage(capacitySo.Value, null);
                }
            }
            
            yield return new WaitForSeconds(capacitySo.Cooldown);
            
            _running = false;
        }

        private IEnumerator LightningAnim()
        {
            for (var i = 0; i < 5; i++)
            {
                lightningSymbolHolder.SetActive(true);
                yield return new WaitForSeconds(0.2f);
                
                lightningSymbolHolder.SetActive(false);
                yield return new WaitForSeconds(0.2f);
            }
        }
    }
}