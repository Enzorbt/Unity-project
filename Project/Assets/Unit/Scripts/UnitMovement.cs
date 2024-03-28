using Supinfo.Project.Scripts.Interfaces;
using UnityEngine;


namespace Supinfo.Project.Unit.Scripts
{
    public class UnitMovement : MonoBehaviour, IMovement
    {
        public void Move(Vector3 direction, float speed)
        {
            transform.Translate(speed * Time.deltaTime * direction);
        }
    }
}