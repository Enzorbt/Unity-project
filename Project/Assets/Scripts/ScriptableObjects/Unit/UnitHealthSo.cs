using System;
using Supinfo.Project.ScriptableObjects.Common;
using Supinfo.Project.Scripts.Common.Stats;
using Supinfo.Project.Scripts.Interfaces.Upgrades;
using UnityEngine;

namespace Supinfo.Project.ScriptableObjects.Unit
{
    /// <summary>
    /// UnitHealthSO is a ScriptableObject that extends HealthSO to include additional properties specific to units.
    /// It inherits the max health functionality from HealthSO and adds new features related to rewards upon killing the unit.
    /// This makes it suitable for defining health properties as well as rewards for units in the game.
    /// </summary>
    [CreateAssetMenu(menuName = "ScriptableObject/Units/UnitHealthSo")]
    public class UnitHealthSo : HealthSo, IUpgradable
    {
        private int _currentGoldGivenUpgrade = 0;
        
        
        private void OnEnable()
        {
            currentAge = 0;
            currentHealthUpgrade = 0;
            _currentGoldGivenUpgrade = 0;
        }
        /// <summary>
        /// The amount of gold given to the player upon killing the unit.
        /// This value can be used to determine the economic reward for defeating this unit.
        /// </summary>
        [Header("Gold given when unit dies")]
        [SerializeField] private Stat goldGiven;
        public float GoldGiven => goldGiven.GetValue(currentAge, _currentGoldGivenUpgrade);
        
        /// <summary>
        /// The amount of experience given to the player upon killing the unit.
        /// This value helps in calculating the experience points a player earns, contributing to their overall progression.
        /// </summary>
        [Header("Exeprience given when unit dies")]
        [SerializeField] private Stat experienceGiven;
        public float ExperienceGiven => experienceGiven.GetValue(currentAge, 0);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        public void Upgrade(UpgradeType type)
        {
            switch (type)
            {
                case UpgradeType.GoldGiven:
                    currentHealthUpgrade++;
                    break;
                case UpgradeType.Health:
                    _currentGoldGivenUpgrade++;
                    break;
                default:
                    Debug.Log("Wrong type of upgrade (health)");
                    break;
            }
            currentHealthUpgrade++;
        }
        
    }
}