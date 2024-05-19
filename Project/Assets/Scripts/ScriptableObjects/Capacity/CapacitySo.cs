using Supinfo.Project.Scripts.Common.Stats;
using Supinfo.Project.Scripts.Interfaces;
using UnityEngine;

namespace Supinfo.Project.Scripts.ScriptableObjects.Capacity
{
    [CreateAssetMenu(menuName = "ScriptableObject/Capacity", order = 0)]
    public class CapacitySo : ScriptableObject, IAgeUpgradable
    {
        private int _ageUpgrade;

        [SerializeField] private Stat _value;
        public float Value => _value.GetValue(_ageUpgrade);

        [SerializeField] private Stat _actionTime;
        public float ActionTime => _actionTime.GetValue(_ageUpgrade);

        [SerializeField] private Stat _hitProbability;
        public float HitProbability => _hitProbability.GetValue(_ageUpgrade);

        [SerializeField] private Stat _cooldown;
        public float Cooldown => _cooldown.GetValue(_ageUpgrade);

        [SerializeField] private string _tag;
        public string Tag => _tag;
        
        [SerializeField] private Stat _cost;
        public float Cost => _cost.GetValue(_ageUpgrade);

        public void UpgradeAge()
        {
            _ageUpgrade++;
        }
    }
}