namespace Supinfo.Project.Scripts.Interfaces
{
    public interface IAttacker
    {
        public void Attack(float amount, IDamageable target); // Deals damage
    }
}