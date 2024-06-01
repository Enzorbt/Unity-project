using System.Collections.Generic;
using ScriptableObjects.Turret;
using ScriptableObjects.Unit;
using Supinfo.Project.ScriptableObjects.Base;
using Supinfo.Project.Scripts.ScriptableObjects.Capacity;
using UnityEngine;

namespace Supinfo.Project.Scripts
{
    /// <summary>
    /// The AgeManager class handles the progression of ages for both players and enemies in the game.
    /// It upgrades various stats and capabilities as the age progresses.
    /// </summary>
    public class AgeManager : MonoBehaviour
    {
        /// <summary>
        /// List of UnitStatSo objects representing ally unit stats.
        /// </summary>
        [Header("All Stats Scriptable Objects")]
        [SerializeField] private List<UnitStatSo> statSosAllies;
        
        /// <summary>
        /// List of UnitStatSo objects representing enemy unit stats.
        /// </summary>
        [SerializeField] private List<UnitStatSo> statSosEnemies;
        
        /// <summary>
        /// List of CapacitySo objects representing ally capacities.
        /// </summary>
        [SerializeField] private List<CapacitySo> capacitySosAllies;
        
        /// <summary>
        /// List of CapacitySo objects representing enemy capacities.
        /// </summary>
        [SerializeField] private List<CapacitySo> capacitySosEnemies;
        
        /// <summary>
        /// BaseStatSo object representing ally base stats.
        /// </summary>
        [SerializeField] private BaseStatSo baseStatSoAllies;
        
        /// <summary>
        /// BaseStatSo object representing enemy base stats.
        /// </summary>
        [SerializeField] private BaseStatSo baseStatSoEnemies;
        
        /// <summary>
        /// TurretStatSo object representing ally turret stats.
        /// </summary>
        [SerializeField] private TurretStatSo turretStatSoAllies;
        
        /// <summary>
        /// TurretStatSo object representing enemy turret stats.
        /// </summary>
        [SerializeField] private TurretStatSo turretStatSoEnemies;

        /// <summary>
        /// Upgrades the age for all ally units, capacities, base, and turrets.
        /// This method is triggered by an event sent by the player.
        /// </summary>
        /// <param name="sender">The component that triggered the upgrade.</param>
        /// <param name="data">Additional data for the upgrade.</param>
        public void UpgradeAgePlayer(Component sender, object data)
        {
            // Modify ScriptableObjects with age
            foreach (var unitStatSo in statSosAllies)
            {
                unitStatSo.UpgradeAge();
            }
            foreach (var capacitySo in capacitySosAllies)
            {
                capacitySo.UpgradeAge();
            }
            
            baseStatSoAllies.UpgradeAge();
            turretStatSoAllies.UpgradeAge();
        }
        
        /// <summary>
        /// Upgrades the age for all enemy units, capacities, base, and turrets.
        /// This method is triggered by an event sent by the enemy.
        /// </summary>
        /// <param name="sender">The component that triggered the upgrade.</param>
        /// <param name="data">Additional data for the upgrade.</param>
        public void UpgradeAgeEnemy(Component sender, object data)
        {
            // Modify ScriptableObjects with age
            foreach (var unitStatSo in statSosEnemies)
            {
                unitStatSo.UpgradeAge();
            }
            foreach (var capacitySo in capacitySosEnemies)
            {
                capacitySo.UpgradeAge();
            }
            
            baseStatSoEnemies.UpgradeAge();
            turretStatSoEnemies.UpgradeAge();
        }
    }
}
