using Interfaces.IA;
using ScriptableObjects.Turret;
using ScriptableObjects.Unit;
using Supinfo.Project.Scripts;
using Supinfo.Project.Scripts.Events;
using Supinfo.Project.Scripts.ScriptableObjects.Capacity;
using UnityEngine;

namespace Supinfo.Project.IA
{
    public class Foundation : MonoBehaviour, IFoundation
    {
        [SerializeField] private GameEvent onLaunchCapacity;
        
        [SerializeField] private GameEvent onSpawnUnit;
        
        [SerializeField] private GameEvent onSpawnTurret;
        
// Voir les autres action  
        public void SpawnUnit(UnitStatSo unitStatSo)
        {
            onSpawnUnit.Raise(this, unitStatSo);
            throw new System.NotImplementedException();
        }

        public void UseCapacity(CapacitySo capacitySo)
        {
            throw new System.NotImplementedException();
        }

        public void UpgradeAge()
        {
            throw new System.NotImplementedException();
        }

        public void Upgrade(UpgradeType upgradeType)
        {
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