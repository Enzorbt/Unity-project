using ScriptableObjects.Turret;
using ScriptableObjects.Unit;
using Supinfo.Project.Interfaces.IA;
using Supinfo.Project.Scripts;
using Supinfo.Project.Scripts.Events;
using Supinfo.Project.Scripts.ScriptableObjects.Capacity;
using UnityEngine;

namespace IA.Event
{
    public class EventsFoundation : MonoBehaviour, IEventFoundation
    {
        [SerializeField] private GameEvent onLaunchCapacity;
        
        [SerializeField] private GameEvent onSpawnUnit;
        
        [SerializeField] private GameEvent onSpawnTurret;
        
        [SerializeField] private GameEvent onEnemiesUpgrade;
        
        [SerializeField] private GameEvent onAgeUpgrade;

        
        public void SpawnUnit(UnitStatSo unitStatSo)
        {
            onSpawnUnit.Raise(this, unitStatSo);
        }

        public void UseCapacity(CapacitySo capacitySo)
        {
            onLaunchCapacity.Raise(this, capacitySo);
        }
        
        public void UpgradeAge()
        {
            onAgeUpgrade.Raise(this, 0);
        }

        public void Upgrade(UpgradeType upgradeType)
        {
            onEnemiesUpgrade.Raise(this, upgradeType);
        }

        public void SpawnTurret(TurretStatSo turretStatSo)
        {
            onSpawnTurret.Raise(this, turretStatSo);
        }
    }
}