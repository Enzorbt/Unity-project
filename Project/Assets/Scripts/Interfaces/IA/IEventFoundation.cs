using ScriptableObjects.Turret;
using ScriptableObjects.Unit;
using Supinfo.Project.Scripts;
using Supinfo.Project.Scripts.ScriptableObjects.Capacity;

namespace Supinfo.Project.Interfaces.IA
{
    /// <summary>
    /// Interface for objects that can trigger game events.
    /// </summary>
    public interface IEventFoundation
    {
        /// <summary>
        /// Spawns a unit with the given stats.
        /// </summary>
        /// <param name="unitStatSo">The stats of the unit to spawn.</param>
        public void SpawnUnit(UnitStatSo unitStatSo);

        /// <summary>
        /// Upgrades the object in the way specified by the upgrade type.
        /// </summary>
        /// <param name="upgradeType">The type of upgrade to apply.</param>
        public void Upgrade(UpgradeType upgradeType);

        /// <summary>
        /// Spawns a turret with the given stats.
        /// </summary>
        /// <param name="turretStatSo">The stats of the turret to spawn.</param>
        public void SpawnTurret(TurretStatSo turretStatSo);

        /// <summary>
        /// Uses the capacity specified by the capacity stats.
        /// </summary>
        /// <param name="capacitySo">The stats of the capacity to use.</param>
        public void UseCapacity(CapacitySo capacitySo);

        /// <summary>
        /// Upgrades the age of the game, potentially unlocking new units, turrets, or capacities.
        /// </summary>
        public void UpgradeAge();
    }
}