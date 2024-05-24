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
        
// Voir les autres action  
        public void SpawnUnit(UnitStatSo unitStatSo)
        {
            onSpawnUnit.Raise(this, unitStatSo);
            throw new System.NotImplementedException();
        }

        public void UseCapacity(CapacitySo capacitySo)
        {
            onLaunchCapacity.Raise(this, capacitySo);
            throw new System.NotImplementedException();
        }
        
        public void UpgradeAge()
        {
            throw new System.NotImplementedException();
        }

        public void Upgrade(UpgradeType upgradeType)
        {
            onEnemiesUpgrade.Raise(this, upgradeType);
            throw new System.NotImplementedException();
        }

        public void SpawnTurret(TurretStatSo turretStatSo)
        {
            onSpawnTurret.Raise(this, turretStatSo);
            throw new System.NotImplementedException();
        }

        public void BuyTurret()
        {
            throw new System.NotImplementedException();
        }
    }
}