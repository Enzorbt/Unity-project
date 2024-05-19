using Supinfo.Project.Scripts.ScriptableObjects.UnitTypes;

namespace Supinfo.Project.Scripts.Interfaces
{
    public interface IDamageable
    {
        public void TakeDamage(float amount, UnitType attackerType); // Take damage from attacker or effect
        
    }
}