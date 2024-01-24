using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Unit;
using UnityEngine;
using UnityEngine.UI;

namespace Supinfo.Project.Unit.Scripts.Health
{
    /// <summary>
    /// The HealthBar class manages the UI representation of a unit's health.
    /// It interacts with the UnitHealth class to update the health bar based on the unit's current health.
    /// </summary>
    public class HealthBar : MonoBehaviour
    {
        /// <summary>
        /// The UI Slider component that represents the health bar.
        /// </summary>
        public Slider healthBar;

        /// <summary>
        /// Reference to the UnitHealth component of the player.
        /// </summary>
        public UnitHealth playerHealth;

        /// <summary>
        /// ScriptableObject containing health-related data.
        /// </summary>
        private UnitHealthSO _uniHealthSO;

        /// <summary>
        /// Maximum health of the unit, retrieved from the ScriptableObject.
        /// </summary>
        private float maxHealth;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// Initializes the maximum health of the unit from the ScriptableObject.
        /// </summary>
        public void Awake()
        {
            // Get maxHealth from ScriptableObject
            maxHealth = _uniHealthSO.MaxHealth.GetValue();
        }
        
        /// <summary>
        /// Start is called before the first frame update.
        /// It initializes the health bar's maximum value and current value based on the player's health.
        /// </summary>
        private void Start()
        {
            playerHealth = GetComponent<UnitHealth>();
            healthBar = GetComponent<Slider>();
            healthBar.maxValue = maxHealth;
            healthBar.value = maxHealth;
        }
        
        /// <summary>
        /// Sets the current health value on the health bar.
        /// </summary>
        /// <param name="hp">The current health to set on the health bar.</param>
        public void SetHealth(float hp)
        {
            healthBar.value = hp;
        }
    }
}
