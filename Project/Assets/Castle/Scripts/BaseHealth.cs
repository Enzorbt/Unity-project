using System;
using Supinfo.Project.Scripts.Events;
using Supinfo.Project.Scripts.Interfaces;
using UnityEngine;

namespace Supinfo.Project.Castle.Scripts
{
    public class BaseHealth : MonoBehaviour, IDamageable
    {
        // Events
        [SerializeField] 
        private GameEvent onBaseDeath;
        [SerializeField] 
        private GameEvent onBaseHealthChange;
        
        // Private fields
        private int health;
        private int maxHealth;
        private int baseNumber;

        private void Awake()
        {
            // get the maxHealth from SO
            // get the base number (0 or 1 / 1 or 2) from SO
        }

        private void Start()
        {
            health = maxHealth;
        }

        // implementation of IDamageable interface
        public void TakeDamage(int amount)
        {
            // remove amount of damage taken from health and raise the event
            health -= amount;
            onBaseHealthChange.Raise(this, health);
            
            // if health <= 0 : Base is dead
            if (health <= 0)
            {
                health = 0;
                onBaseDeath.Raise(this, baseNumber);
            }
        }
        
    }
}