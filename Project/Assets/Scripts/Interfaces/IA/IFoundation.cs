using ScriptableObjects.Turret;
using ScriptableObjects.Unit;
using Supinfo.Project.Scripts;
using Supinfo.Project.Scripts.ScriptableObjects.Capacity;

namespace Interfaces.IA
{
    // Voir les autres action  
    public interface IFoundation
    {
        // SPAWN UNIT 
        public void SpawnUnit(UnitStatSo unitStatSo);
        
        // GET AMELIORATION 
        public void Upgrade(UpgradeType upgradeType);
        
        // BUY TURRET EMPLACEMENT 
        public void SpawnTurret(TurretStatSo turretStatSo);
        
        // BUY TURRET 
        public void BuyTurret();
        
        // USE CAPACITY 
        public void UseCapacity(CapacitySo capacitySo);
        
        // UPGRADE AGE 
        public void UpgradeAge();
    }
}