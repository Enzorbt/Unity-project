using System.Collections;
using Supinfo.Project.Interfaces.Capacity;
using Supinfo.Project.Scripts.Events;
using Supinfo.Project.Scripts.Interfaces;
using Supinfo.Project.Scripts.ScriptableObjects.Capacity;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Public class which allows to launch capacity.
/// </summary>
public class SpecialCapacity : MonoBehaviour, ICapacity
{
    /// <summary>
    /// The leftmost point in the game scene.
    /// </summary>
    [SerializeField] private GameObject leftMostPoint;

    /// <summary>
    /// The rightmost point in the game scene.
    /// </summary>
    [SerializeField] private GameObject rightMostPoint;

    /// <summary>
    /// The game object that holds the lightning symbol.
    /// </summary>
    [SerializeField] private GameObject lightningSymbolHolder;

    /// <summary>
    /// Get the special capacity status.
    /// </summary>
    [SerializeField] private GameEvent onSpecialCapacityStatusChange;

    /// <summary>
    /// Private variable to know if the game is running or not, (running state).
    /// </summary>
    private bool _running;
    
    /// <summary>
    /// The game event that trigger the sound.
    /// </summary>
    public GameEvent onPlaySound;
    
    /// <summary>
    /// The audioclip to play when thunder capacity is used.
    /// </summary>
    [SerializeField] private AudioClip thunderSound;

    private void Start()
    {
        // Trigger the onSpecialCapacityStatusChange event and set the status to true at the start of the game.
        onSpecialCapacityStatusChange.Raise(this, true);
    }

    /// <summary>
    /// Game event listener function called when the event LaunchCapacity is triggered (linked to a GameEventListener component).
    /// If it is not running, it launches the capacity with a coroutine.
    /// </summary>
    /// <param name="sender">The sender of the game event.</param>
    /// <param name="data">The data being transferred (CapacitySO).</param>
    public void LaunchCapacity(Component sender, object data)
    {
        // Check if the data is of type CapacitySo. If not, return from the function.
        if (data is not CapacitySo capacitySo) return;

        // If the game is not running, start the CapacityDelayed coroutine.
        if (!_running)
        {
            StartCoroutine(CapacityDelayed(capacitySo));
        }
    }

    /// <summary>
    /// Launch the capacity with a delay.
    /// </summary>
    /// <param name="capacitySo"></param>
    /// <returns></returns>
    private IEnumerator CapacityDelayed(CapacitySo capacitySo)
    {
        // Trigger the onSpecialCapacityStatusChange event and set the status to false.
        onSpecialCapacityStatusChange.Raise(this, false);
        _running = true;

        // If the capacity type is Meteor, start the meteor animation.
        if (capacitySo.CapabilityType == CapabilityType.Meteor)
        {
            // Loop 10 times to create 10 meteors.
            for (int i = 0; i < 10; i++)
            {
                // Calculate a random x-position for the meteor within the game scene's boundaries.
                var length = rightMostPoint.transform.position.x - leftMostPoint.transform.position.x;
                var posX = leftMostPoint.transform.position.x + Random.value * length;
                var pos = new Vector3(posX, rightMostPoint.transform.position.y);

                // Instantiate the meteor prefab at the calculated position.
                var instantiatedObject = Instantiate(capacitySo.Prefab, pos, Quaternion.identity);

                // Get the SpriteRenderer component from the instantiated meteor.
                var spriteRenderer = instantiatedObject.GetComponentInChildren<SpriteRenderer>();
                if (spriteRenderer is null) break;

                // Set the meteor's sprite to the one defined in the CapacitySO.
                spriteRenderer.sprite = capacitySo.Sprite;

                // Get the Animator component from the instantiated meteor.
                var animator = instantiatedObject.GetComponentInChildren<Animator>();
                if (animator is null) break;

                // Set the meteor's animator controller to the one defined in the CapacitySO.
                animator.runtimeAnimatorController = capacitySo.Controllers;

                // Wait for 0.1 seconds before creating the next meteor.
                yield return new WaitForSeconds(0.1f);
            }
        }
        // If the capacity type is Lightning, start the lightning animation.
        else if (capacitySo.CapabilityType == CapabilityType.Lightning)
        {
            // Start the LightningAnim coroutine.
            StartCoroutine(LightningAnim());
        }

        // Damage the units.
        // Find all the units in the game scene with the tag defined in the CapacitySO.
        var allUnits = GameObject.FindGameObjectsWithTag("Unit," + capacitySo.Tag);

        // Loop through all the found units.
        foreach (var unit in allUnits)
        {
            // Get the IDamageable component from the unit.
            if (!unit.TryGetComponent(out IDamageable damageable)) break;

            // If a random value is less than the hit probability defined in the CapacitySO, damage the unit.
            if (Random.value < capacitySo.HitProbability)
            {
                damageable.TakeDamage(capacitySo.Damage, null);
            }
        }

        // Wait for the cooldown duration defined in the CapacitySO.
        yield return new WaitForSeconds(capacitySo.Cooldown);

        // Set the _running variable to false.
        _running = false;

        // Trigger the onSpecialCapacityStatusChange event and set the status to true.
        onSpecialCapacityStatusChange.Raise(this, true);
    }

    /// <summary>
    /// Coroutine for the lightning animation.
    /// </summary>
    private IEnumerator LightningAnim()
    {
    // Loop 5 times to blink the lightning symbol 5 times.
    for (var i = 0; i < 5; i++)
    {
        // Set the lightning symbol holder active to show the symbol.
        lightningSymbolHolder.SetActive(true);
        onPlaySound?.Raise(this, thunderSound);
        yield return new WaitForSeconds(0.2f);

        // Set the lightning symbol holder inactive to hide the symbol.
        lightningSymbolHolder.SetActive(false);
        yield return new WaitForSeconds(0.2f);
    }
    }
}
