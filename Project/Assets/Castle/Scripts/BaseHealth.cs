using System.Collections;
using Supinfo.Project.ScriptableObjects.Base;
using Supinfo.Project.Scripts.Common;
using Supinfo.Project.Scripts.Events;
using Supinfo.Project.Scripts.Interfaces;
using Supinfo.Project.Scripts.Managers;
using Supinfo.Project.Scripts.ScriptableObjects.UnitTypes;
using UnityEngine;
using UnityEngine.Serialization;

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
        private GameEvent onBaseHealthRatioChange;
        
        // Private fields

        /// <summary>
        /// Identifier for the base (e.g., 0 or 1 / 1 or 2), set from the editor.
        /// </summary>
        [SerializeField]
        private BaseIdentifier baseId;
        
        /// <summary>
        /// Identifier for the base (e.g., 0 or 1 / 1 or 2), set from a ScriptableObject.
        /// </summary>
        public BaseStatSo baseStatSo;

        private SpriteRenderer _spriteRenderer;
        
        private int _age;

        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// Initializes the base's maximum health and number from a ScriptableObject.
        /// </summary>
        private void Awake()
        {
            MaxHealth = baseStatSo.MaxHealth;
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        /// <summary>
        /// Implementation of the IDamageable interface.
        /// Applies damage to the base, updates its health, and triggers events based on health changes or destruction.
        /// </summary>
        /// <param name="amount">The amount of damage to apply to the base.</param>
        /// <param name="attackerType"></param>
        public void TakeDamage(float amount, UnitType attackerType)
        {
            // Remove amount of damage taken from health and raise the event
            CurHealth -= amount;
            if (!(onBaseHealthRatioChange is null))
            {
                onBaseHealthRatioChange.Raise(this, CurHealth/MaxHealth);
            }
            
            // Check if health <= 0 : Base is dead
            if (CurHealth <= 0)
            {
                if (!(onBaseDeath is null))
                {
                    onBaseDeath.Raise(this, baseId);
                }
                
                CurHealth = 0;
            }
        }

        public void OnAgeUpgrade(Component sender, object data)
        {
            StartCoroutine(UpdateStatsWithDelay());
        }

        private IEnumerator UpdateStatsWithDelay()
        {
            yield return new WaitForSeconds(2f);
            
            // update max heath and current health
            var healthToAdd = baseStatSo.MaxHealth - MaxHealth;
            MaxHealth = baseStatSo.MaxHealth;
            CurHealth += healthToAdd;
            
            // send data to listeners
            onBaseHealthRatioChange.Raise(this, CurHealth/MaxHealth);
            
            // update sprite
            _spriteRenderer.sprite = baseStatSo.Sprite;
        }
        
    }
}
