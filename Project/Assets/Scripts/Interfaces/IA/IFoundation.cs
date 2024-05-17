using ScriptableObjects.Unit;
using Supinfo.Project.Scripts;

namespace Interfaces.IA
{
    public interface IFoundation
    {
        // SPAWN UNIT 
        public void SpawnUnit(UnitStatSo unitStatSo);
        
        // GET AMELIORATION 
        public void Upgrade(UpgradeType upgradeType);
        
        // BUY TURRET EMPLACEMENT 
        public void SpawnTurret();
        
        // BUY TURRET 
        public void BuyTurret(int pTurretID);
    }
}