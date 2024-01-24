using System.Collections;
using System.Collections.Generic;
using ScriptableObjects.Unit;
using UnityEngine;
using UnityEngine.UI;

namespace Supinfo.Project.Unit.Scripts.Health
{
    public class HealthBar : MonoBehaviour
    {
            public Slider healthBar;
            public UnitHealth playerHealth;
            private UnitHealthSO _uniHealthSO;
            private float maxHealth;

            public void Awake()
            {
                // get maxHealth in SO
                maxHealth = _uniHealthSO.MaxHealth.GetValue();
            }
            
            
            private void Start()
            {
                playerHealth = GetComponent<UnitHealth>();
                healthBar = GetComponent<Slider>();
                healthBar.maxValue = maxHealth;
                healthBar.value = maxHealth;
            }
            
            public void SetHealth(float hp)
            {
                healthBar.value = hp;
            }
    }
}