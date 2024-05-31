using System;
using Supinfo.Project.Scripts.Common.Stats;
using Supinfo.Project.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.Serialization;

namespace Supinfo.Project.Scripts.ScriptableObjects.Capacity
{
    public enum CapabilityType
    {
        Meteor,
        Lightning
    }
    
    [CreateAssetMenu(menuName = "ScriptableObject/Capacity", order = 0)]
    public class CapacitySo : ScriptableObject, IAgeUpgradable
    {
        private int _currentAge = 0;

        private void OnEnable()
        {
            _currentAge = 0;
        }

        [SerializeField] private Stat damage;
        public float Damage => damage.GetValue(_currentAge);

        [SerializeField] private Stat _actionTime;
        public float ActionTime => _actionTime.GetValue(_currentAge);

        [SerializeField] private Stat _hitProbability;
        public float HitProbability => _hitProbability.GetValue(_currentAge);

        [SerializeField] private Stat _cooldown;
        public float Cooldown => _cooldown.GetValue(_currentAge);

        [SerializeField] private string _tag;
        public string Tag => _tag;
        
        [Header("Sprites (7, 1 per age)")] [SerializeField]
        private Sprite[] _sprites;
        public Sprite Sprite => _sprites[_currentAge];

        [Header("Animator Controller (7, 1 per age")] [SerializeField]
        private RuntimeAnimatorController[] controllers;

        public RuntimeAnimatorController Controllers => controllers[_currentAge];

        public void UpgradeAge()
        {
            _currentAge++;
        }
        
        [Header("Prefab")] [SerializeField]
        private GameObject _prefab;
        public GameObject Prefab => _prefab;
        
        [Header("Capacity type")] [SerializeField]
        private CapabilityType capabilityType;
        public CapabilityType CapabilityType => capabilityType;
    }
}