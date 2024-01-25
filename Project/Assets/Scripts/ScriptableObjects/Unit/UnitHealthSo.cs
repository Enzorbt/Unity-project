using System;
using Supinfo.Project.ScriptableObjects.Common;
using Supinfo.Project.Scripts.Common.Stats;
using UnityEngine;

namespace Supinfo.Project.ScriptableObjects.Unit
{
    /// <summary>
    /// UnitHealthSO is a ScriptableObject that extends HealthSO to include additional properties specific to units.
    /// It inherits the max health functionality from HealthSO and adds new features related to rewards upon killing the unit.
    /// This makes it suitable for defining health properties as well as rewards for units in the game.
    /// </summary>
    [CreateAssetMenu(menuName = "ScriptableObject/Units/UnitHealthSo")]
    public class UnitHealthSo : HealthSO
    {
        //--------- Rewards upon killing ---------//

        /// <summary>
        /// The amount of gold given to the player upon killing the unit.
        /// This value can be used to determine the economic reward for defeating this unit.
        /// </summary>
        [Header("Rewards upon killing")]
        [SerializeField] private Stat goldGiven;
        public Stat GoldGiven => goldGiven;
        
        /// <summary>
        /// The amount of experience given to the player upon killing the unit.
        /// This value helps in calculating the experience points a player earns, contributing to their overall progression.
        /// </summary>
        [SerializeField] private Stat experienceGiven;
        public Stat ExperienceGiven => experienceGiven;
    }
}