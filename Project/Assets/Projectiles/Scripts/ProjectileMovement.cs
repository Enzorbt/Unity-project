using Supinfo.Project.Scripts.Interfaces;
using UnityEngine;

namespace Supinfo.Project.Projectiles.Scripts
{
    public class ProjectileMovement: MonoBehaviour, IMovement
    {
        public void Move(Vector3 direction, float speed)
        {
            transform.Translate(speed * Time.deltaTime * direction);
        }
    }
}