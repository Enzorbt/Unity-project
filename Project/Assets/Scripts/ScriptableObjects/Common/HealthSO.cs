using Supinfo.Project.Scripts.Stats;
using UnityEngine;

namespace ScriptableObjects.Common
{
    /// <summary>
    /// HealthSO is a ScriptableObject that defines the health-related properties of an object.
    /// It is used to store the maximum health value of the object.
    /// </summary>
    public class HealthSO : ScriptableObject
    {
        /// <summary>
        /// The maximum health of the object. This is a Stat type, allowing for the flexibility of assigning various health values.
        /// </summary>
        [Header("Max health")]
        [SerializeField] private Stat maxHealth;
        public Stat MaxHealth => maxHealth;
    }
}