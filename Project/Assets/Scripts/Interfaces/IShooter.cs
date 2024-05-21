using System.Collections;
using Supinfo.Project.Scripts.ScriptableObjects.UnitTypes;
using UnityEngine;

namespace Interfaces
{
    public interface IShooter
    {
        public void Shoot(float amount, float cooldown, float speed, Transform target, UnitType attackerType); // shoot at enemies
        public IEnumerator ShootWithCooldown(float amount, float cooldown, float speed, Transform target, UnitType attackerType);
    }
}