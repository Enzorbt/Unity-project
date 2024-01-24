using Supinfo.Project.Scripts.Stats;
using UnityEngine;

namespace ScriptableObjects.Common
{
    public class HealthSO : ScriptableObject
    {
        [Header("Max health")]
         [SerializeField] private Stat maxHealth;
        public Stat MaxHealth => maxHealth;
    }
}