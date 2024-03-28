using UnityEngine;

namespace Supinfo.Project.Scripts.Interfaces
{
    public interface IMovement
    {
        public void Move(Vector3 direction, float speed);
    }
}