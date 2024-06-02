using ScriptableObjects.Turret;
using ScriptableObjects.Unit;
using Supinfo.Project.Interfaces.IA;
using Supinfo.Project.Scripts;
using Supinfo.Project.Scripts.Events;
using Supinfo.Project.Scripts.ScriptableObjects.Capacity;
using UnityEngine;

namespace IA.Event
{
    /// <summary>
    /// Manages game events for the AI, such as spawning units, launching capacities, and upgrading.
    /// </summary>
    public class EventsFoundation : MonoBehaviour, IEventFoundation
    {
        /// <summary>
        /// Event triggered when a capacity is launched.
        /// </summary>
        [SerializeField] private GameEvent onLaunchCapacity;
        
        /// <summary>
        /// Event triggered when a unit is spawned.
        /// </summary>
        [SerializeField] private GameEvent onSpawnUnit;
        
        /// <summary>
        /// Event triggered when a turret is spawned.
        /// </summary>
        [SerializeField] private GameEvent onSpawnTurret;
        
        /// <summary>
        /// Event triggered when enemies are upgraded.
        /// </summary>
        [SerializeField] private GameEvent onEnemiesUpgrade;
        
        /// <summary>
        /// Event triggered when the age is upgraded.
        /// </summary>
        [SerializeField] private GameEvent onAgeUpgrade;
        
        /// <summary>
        /// Spawns a unit with the specified stats.
        /// </summary>
        /// <param name="unitStatSo">The stats of the unit to spawn.</param>
        public void SpawnUnit(UnitStatSo unitStatSo)
        {
            onSpawnUnit.Raise(this, unitStatSo);
        }

        /// <summary>
        /// Uses a capacity with the specified stats.
        /// </summary>
        /// <param name="capacitySo">The stats of the capacity to use.</param>
        public void UseCapacity(CapacitySo capacitySo)
        {
            onLaunchCapacity.Raise(this, capacitySo);
        }
        
        /// <summary>
        /// Upgrades the age.
        /// </summary>
        public void UpgradeAge()
        {
            onAgeUpgrade.Raise(this, 0);
        }

        /// <summary>
        /// Upgrades with the specified type.
        /// </summary>
        /// <param name="upgradeType">The type of upgrade to perform.</param>
        public void Upgrade(UpgradeType upgradeType)
        {
            onEnemiesUpgrade.Raise(this, upgradeType);
        }

        /// <summary>
        /// Spawns a turret with the specified stats.
        /// </summary>
        /// <param name="turretStatSo">The stats of the turret to spawn.</param>
        public void SpawnTurret(TurretStatSo turretStatSo)
        {
            onSpawnTurret.Raise(this, turretStatSo);
        }
    }
}
