using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Unit;
using Supinfo.Project.Scripts.Events;
using Supinfo.Project.Scripts.Interfaces;
using UnityEngine;

namespace Supinfo.Project.Unit.Scripts.Health{
    public class UnitHealth : MonoBehaviour, IDamageable
    {
            private float curHealth;
            private float coins;
            private float xp;
            private float maxHealth;
            public HealthBar healthBar;
            public GameEvent onUnitDeathCoins;
            public GameEvent onUnitDeathXp;
            private UnitHealthSO _uniHealthSO;

            public void Awake()
            {
                // get coins, maxHealth and xp in SO
                coins = _uniHealthSO.GoldGiven.GetValue();
                xp = _uniHealthSO.ExperienceGiven.GetValue();
                maxHealth = _uniHealthSO.MaxHealth.GetValue();
            }
            
            void Start()
            {
                curHealth = maxHealth;
            }
            
            void Update()
            {
                if( Input.GetKeyDown( KeyCode.Space ) )
                {
                    TakeDamage(10);
                }
            }
            
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