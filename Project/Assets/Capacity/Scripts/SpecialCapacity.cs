using System.Collections;
using Supinfo.Project.Interfaces.Capacity;
using Supinfo.Project.Scripts.Events;
using Supinfo.Project.Scripts.Interfaces;
using Supinfo.Project.Scripts.ScriptableObjects.Capacity;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Supinfo.Project.Capacity.Scripts
{
    /// <summary>
    /// The capacity manager, deals damage to units and display the capacity on the game scene.
    /// </summary>
    public class SpecialCapacity : MonoBehaviour, ICapacity
    {
        /// <summary>
        /// The left most point of reference to choose a position for meteors spawn.
        /// </summary>
        [SerializeField] private GameObject leftMostPoint;
        /// <summary>
        /// The point of reference to the left to choose a position for meteors spawn.
        /// </summary>
        [SerializeField] private GameObject rightMostPoint;
        /// <summary>
        /// The game object that holds the lightning sprite.
        /// </summary>
        [SerializeField] private GameObject lightningSymbolHolder;
        /// <summary>
        /// The game event to trigger when special capacity is already being used.
        /// </summary>
        [SerializeField] private GameEvent onSpecialCapacityStatusChange;
        
        /// <summary>
        /// The state of the special capacity (running or not running).
        /// </summary>
        private bool _running;

        /// <summary>
        /// Called when the object is instantiated.
        /// </summary>
        private void Start()
        {
            onSpecialCapacityStatusChange.Raise(this, true);
        }

        /// <summary>
        /// Game event listener function called when the event onLaunchCapacity is triggered (linked to a GameEventListener component).
        /// </summary>
        /// <param name="sender">The sender of the game event.</param>
        /// <param name="data">The data being transferred.</param>
        public void LaunchCapacity(Component sender, object data)
        {
            if (data is not CapacitySo capacitySo) return;

            if (!_running)
            {
                StartCoroutine(CapacityDelayed(capacitySo));
            }
        }

        /// <summary>
        /// Coroutine used to make capacities being delayed.
        /// </summary>
        /// <param name="capacitySo">The capacity scriptable object.</param>
        /// <returns>Waiting for seconds.</returns>
        private IEnumerator CapacityDelayed(CapacitySo capacitySo)
        {
            onSpecialCapacityStatusChange.Raise(this, false);
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

                    var spriteRenderer = instantiatedObject.GetComponentInChildren<SpriteRenderer>();
                    if (spriteRenderer is null) break;
                    spriteRenderer.sprite = capacitySo.Sprite;
                    
                    var animator = instantiatedObject.GetComponentInChildren<Animator>();
                    if (animator is null) break;
                        
                    animator.runtimeAnimatorController = capacitySo.Controllers;
                    
                    yield return new WaitForSeconds(0.1f);
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
                    damageable.TakeDamage(capacitySo.Damage, null);
                }
            }
            
            yield return new WaitForSeconds(capacitySo.Cooldown);
            
            _running = false;
            onSpecialCapacityStatusChange.Raise(this, true);
        }

        /// <summary>
        /// Coroutine for the lightning animation.
        /// </summary>
        /// <returns>Wait for seconds.</returns>
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