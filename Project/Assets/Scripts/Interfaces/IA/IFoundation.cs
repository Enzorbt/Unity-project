namespace Interfaces.IA
{
    public interface IFoundation
    {
        // SPAWN UNIT 
        public void SpawnUnit();
        
        // GET AMELIORATION 
        public void GetAmelioration(int pAmeliorationID);
        
        // BUY TURRET EMPLACEMENT 
        public void BuyTurretEmplacement();
        
        // BUY TURRET 
        public void BuyTurret(int pTurretID);
    }
}