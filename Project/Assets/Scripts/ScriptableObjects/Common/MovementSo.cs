using Supinfo.Project.Scripts.Common.Stats;
using Supinfo.Project.Scripts.Interfaces;
using UnityEngine;

namespace Supinfo.Project.ScriptableObjects.Common
{
    public class MovementSo : ScriptableObject, IAgeUpgradable
    {
        protected int currentAge;
        
        /// <summary>
        /// Walking speed of the unit.
        /// </summary>
        [Header("Speed of the unit")]
        [SerializeField] private Stat walkSpeed;
        public float WalkSpeed => walkSpeed.GetValue(currentAge, 0);
        
        public void UpgradeAge()
        {
            currentAge++;
        }
    }
}