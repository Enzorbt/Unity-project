using System;
using ScriptableObjects.Base;
using Supinfo.Project.Scripts.Common;
using Supinfo.Project.Scripts.Events;
using Supinfo.Project.Scripts.Interfaces;
using UnityEngine;

namespace Supinfo.Project.Castle.Scripts
{
    /// <summary>
    /// BaseHealth class manages the health of a base in the game.
    /// It implements the IDamageable interface to handle damage interactions.
    /// This class is responsible for tracking the base's health and triggering events when the health changes or the base is destroyed.
    /// </summary>
    public class BaseHealth : Health, IDamageable
    {
        // Events

        /// <summary>
        /// Event triggered when the base is destroyed.
        /// </summary>
        [SerializeField] 
        private GameEvent onBaseDeath;

        /// <summary>
        /// Event triggered when the base's health changes.
        /// </summary>
        [SerializeField] 
        private GameEvent onBaseHealthChange;
        
        // Private fields

        /// <summary>
        /// Identifier for the base (e.g., 0 or 1 / 1 or 2), set from a ScriptableObject.
        /// </summary>
        private int _baseNumber;
        
        /// <summary>
        /// Identifier for the base (e.g., 0 or 1 / 1 or 2), set from a ScriptableObject.
        /// </summary>
        private BaseHealthSO _baseHealthSo;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// Initializes the base's maximum health and number from a ScriptableObject.
        /// </summary>
        private void Awake()
        {
            maxHealth = _baseHealthSo.MaxHealth.GetValue();
        }

        /// <summary>
        /// Implementation of the IDamageable interface.
        /// Applies damage to the base, updates its health, and triggers events based on health changes or destruction.
        /// </summary>
        /// <param name="amount">The amount of damage to apply to the base.</param>
        public void TakeDamage(int amount)
        {
            // Remove amount of damage taken from health and raise the event
            curHealth -= amount;
            onBaseHealthChange.Raise(this, curHealth/maxHealth);
            
            // Check if health <= 0 : Base is dead
            if (curHealth <= 0)
            {
                curHealth = 0;
                onBaseDeath.Raise(this, _baseNumber);
            }
        }
        
    }
}
