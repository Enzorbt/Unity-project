using ScriptableObjects.Unit;
using Supinfo.Project.Scripts.Events;
using Supinfo.Project.Scripts.Interfaces;
using Supinfo.Project.Scripts.ScriptableObjects.UnitTypes;
using UnityEngine;
using UnityEngine.Serialization;

namespace Supinfo.Project.Unit.Scripts.Health
{
    /// <summary>
    /// The UnitHealth class manages the health of a unit, including taking damage and triggering events on death.
    /// It implements the IDamageable interface to handle damage interactions.
    /// </summary>
    public class UnitHealth : Project.Scripts.Common.Health, IDamageable
    {
        /// <summary>
        /// Coins rewarded upon unit death.
        /// </summary>
        public float GoldGiven { get; set; }

        /// <summary>
        /// Experience points rewarded upon unit death.
        /// </summary>
        public float XpGiven { get; set; }

        /// <summary>
        /// Reference to the health bar UI for the unit.
        /// </summary>
        public HealthBar healthBar;
        
        public UnitType UnitType { get; set; }

        /// <summary>
        /// Event triggered when the unit dies to provide coins.
        /// </summary>
        public GameEvent onUnitDeathCoins;

        /// <summary>
        /// Event triggered when the unit dies to provide experience points.
        /// </summary>
        public GameEvent onUnitDeathXp;
        
        /// <summary>
        /// Update is called once per frame. Here it's used to test damage reception on a key press.
        /// </summary>

        /// <summary>
        /// Applies damage to the unit and updates the health bar.
        /// Triggers death events if health falls to zero or below.
        /// </summary>
        /// <param name="damage">The amount of damage to apply to the unit.</param>
        /// <param name="attackerType"></param>
        public void TakeDamage(float damage, UnitType attackerType)
        {
            if (attackerType is not null && attackerType.StrongAgainst == UnitType)
            {
                damage *= 1.5f;
            }
            
            CurHealth -= damage;
            healthBar.SetHealthSliderValue(CurHealth/MaxHealth);
            
            if (CurHealth <= 0)
            {
                if (!(onUnitDeathCoins is null))
                {
                    onUnitDeathCoins.Raise(this, GoldGiven);
                }
                if (!(onUnitDeathXp is null))
                {
                    onUnitDeathXp.Raise(this, XpGiven);
                }
                Destroy(gameObject);
            }
        }
    }
}
