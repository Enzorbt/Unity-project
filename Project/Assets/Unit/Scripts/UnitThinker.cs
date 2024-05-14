using Common;
using ScriptableObjects.Unit;
using UnityEngine;

namespace Supinfo.Project.Unit.Scripts
{
    public class UnitThinker: Thinker
    {
        [SerializeField] private UnitStatSo unitStatSo;
        public UnitStatSo UnitStatSo => unitStatSo;

        public Vector3 Direction { get; set; }
        private void Awake()
        {
            if (transform.position.x > 0)
            {
                Direction = Vector3.left;
            }
            else
            {
                Direction = Vector3.right;
            }
        }
    }
}