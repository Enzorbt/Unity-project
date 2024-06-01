using System.Collections;
using Supinfo.Project.Scripts.Events;
using UnityEngine;

namespace Supinfo.Project.Scripts.Managers
{
    /// <summary>
    /// Manages the gold resource in the game.
    /// </summary>
    public class GoldManager : MonoBehaviour
    {
        /// <summary>
        /// Event triggered when the gold count changes.
        /// </summary>
        [SerializeField] private GameEvent onGoldCountChange;

        /// <summary>
        /// Initial amount of gold.
        /// </summary>
        private float _gold = 2500;

        /// <summary>
        /// Amount of gold gained per interval.
        /// </summary>
        [SerializeField] private float goldGain;
        
        /// <summary>
        /// Time interval for gaining gold.
        /// </summary>
        [SerializeField] private float goldGainCooldown;

        /// <summary>
        /// Flag to indicate if gold is currently being gained.
        /// </summary>
        private bool _isGainingGold;
        
        private void Start()
        {
            onGoldCountChange.Raise(this, _gold);  // Raise event for initial gold count.
        }
        
        private void Update()
        {
            if (!_isGainingGold)
            {
                StartCoroutine(GainGold());  // Start coroutine to gain gold.
            }
        }

        /// <summary>
        /// Coroutine to gain gold at specified intervals.
        /// </summary>
        /// <returns>IEnumerator for the coroutine.</returns>
        private IEnumerator GainGold()
        {
            _isGainingGold = true;  // Set flag to indicate gold is being gained.
            _gold += goldGain;  // Increase gold count.
            onGoldCountChange.Raise(this, _gold);  // Raise event for gold count change.
            yield return new WaitForSeconds(goldGainCooldown);  // Wait for the cooldown period.
            _isGainingGold = false;  // Reset flag after gaining gold.
        }

        /// <summary>
        /// Listener for gold change event.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="data">Event data.</param>
        public void OnGoldChange(Component sender, object data)
        {
            if (data is not float gold) return;  // Validate data type.
            
            _gold += gold;  // Add received gold to the total.
            
            onGoldCountChange.Raise(this, _gold);  // Raise event for gold count change.
        }
    }
}
