using Supinfo.Project.Scripts.Interfaces;
using UnityEngine;

namespace Supinfo.Project.Unit.Scripts.UnitCollision
{
    public class UnitAttack : MonoBehaviour, IAttacker
    {
        public void Attack(float amount, IDamageable target)
        {
            target.TakeDamage(amount);
        }
    }
}