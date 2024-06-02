using Supinfo.Project.Scripts.Common.Stats;
using Supinfo.Project.Scripts.Interfaces;
using UnityEngine;

namespace Supinfo.Project.Scripts.ScriptableObjects.Capacity
{
    /// <summary>
    /// Enumeration representing different types of capabilities.
    /// </summary>
    public enum CapabilityType
    {
        Meteor,
        Lightning
    }

    /// <summary>
    /// ScriptableObject representing the capacity which can be upgraded with age.
    /// </summary>
    [CreateAssetMenu(menuName = "ScriptableObject/Capacity", order = 0)]
    public class CapacitySo : ScriptableObject, IAgeUpgradable
    {
        private int _currentAge = 0;

        /// <summary>
        /// Initializes the current age to zero when the ScriptableObject is enabled.
        /// </summary>
        private void OnEnable()
        {
            _currentAge = 0;
        }

        /// <summary>
        /// The damage stat of the capacity.
        /// </summary>
        [SerializeField] private Stat damage;
        public float Damage => damage.GetValue(_currentAge);

        /// <summary>
        /// The action time stat of the capacity.
        /// </summary>
        [SerializeField] private Stat _actionTime;
        public float ActionTime => _actionTime.GetValue(_currentAge);

        /// <summary>
        /// The hit probability stat of the capacity.
        /// </summary>
        [SerializeField] private Stat _hitProbability;
        public float HitProbability => _hitProbability.GetValue(_currentAge);

        /// <summary>
        /// The cooldown stat of the capacity.
        /// </summary>
        [SerializeField] private Stat _cooldown;
        public float Cooldown => _cooldown.GetValue(_currentAge);

        /// <summary>
        /// The tag associated with the capacity.
        /// </summary>
        [SerializeField] private string _tag;
        public string Tag => _tag;

        /// <summary>
        /// Array of sprites representing different ages of the capacity.
        /// </summary>
        [Header("Sprites (7, 1 per age)")] [SerializeField]
        private Sprite[] _sprites;
        public Sprite Sprite => _sprites[_currentAge];

        /// <summary>
        /// Array of animator controllers representing different ages of the capacity.
        /// </summary>
        [Header("Animator Controller (7, 1 per age)")] [SerializeField]
        private RuntimeAnimatorController[] controllers;
        public RuntimeAnimatorController Controllers => controllers[_currentAge];

        /// <summary>
        /// Upgrades the age of the capacity, incrementing the current age by one.
        /// </summary>
        public void UpgradeAge()
        {
            _currentAge++;
        }

        /// <summary>
        /// The prefab associated with the capacity.
        /// </summary>
        [Header("Prefab")] [SerializeField]
        private GameObject _prefab;
        public GameObject Prefab => _prefab;

        /// <summary>
        /// The type of capability (e.g., Meteor or Lightning).
        /// </summary>
        [Header("Capacity type")] [SerializeField]
        private CapabilityType capabilityType;
        public CapabilityType CapabilityType => capabilityType;
    }
}
