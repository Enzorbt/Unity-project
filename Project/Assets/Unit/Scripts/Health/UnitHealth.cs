using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Unit;
using Supinfo.Project.Scripts.Events;
using Supinfo.Project.Scripts.Interfaces;
using UnityEngine;

namespace Supinfo.Project.Unit.Scripts.Health{
    /// <summary>
    /// The UnitHealth class manages the health of a unit, including taking damage and triggering events on death.
    /// It implements the IDamageable interface to handle damage interactions.
    /// </summary>
    public class UnitHealth : MonoBehaviour, IDamageable
    {
        /// <summary>
        /// Current health of the unit.
        /// </summary>
        private float curHealth;

        /// <summary>
        /// Coins rewarded upon unit death.
        /// </summary>
        private float coins;

        /// <summary>
        /// Experience points rewarded upon unit death.
        /// </summary>
        private float xp;

        /// <summary>
        /// Maximum health of the unit.
        /// </summary>
        private float maxHealth;

        /// <summary>
        /// Reference to the health bar UI for the unit.
        /// </summary>
        public HealthBar healthBar;

        /// <summary>
        /// Event triggered when the unit dies to provide coins.
        /// </summary>
        public GameEvent onUnitDeathCoins;

        /// <summary>
        /// Event triggered when the unit dies to provide experience points.
        /// </summary>
        public GameEvent onUnitDeathXp;

        /// <summary>
        /// ScriptableObject containing health-related data for the unit.
        /// </summary>
        private UnitHealthSO _uniHealthSO;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// Initializes the unit's health data from the ScriptableObject.
        /// </summary>
        public void Awake()
        {
            // Get coins, maxHealth, and xp from ScriptableObject
            coins = _uniHealthSO.GoldGiven.GetValue();
            xp = _uniHealthSO.ExperienceGiven.GetValue();
            maxHealth = _uniHealthSO.MaxHealth.GetValue();
        }
        
        /// <summary>
        /// Start is called before the first frame update.
        /// Initializes current health to maximum health.
        /// </summary>
        void Start()
        {
            curHealth = maxHealth;
        }
        
        /// <summary>
        /// Update is called once per frame. Here it's used to test damage reception on a key press.
        /// </summary>
        void Update()
        {
            if( Input.GetKeyDown( KeyCode.Space ) )
            {
                TakeDamage(10);
            }
        }
        
        /// <summary>
        /// Applies damage to the unit and updates the health bar.
        /// Triggers death events if health falls to zero or below.
        /// </summary>
        /// <param name="damage">The amount of damage to apply to the unit.</param>
        public void TakeDamage(int damage)
        {
            curHealth -= damage;
            healthBar.SetHealth(curHealth);
            
            if (curHealth <= 0)
            {
                onUnitDeathCoins.Raise(this, coins);
                onUnitDeathXp.Raise(this, xp);
            }
        }
    }
}
