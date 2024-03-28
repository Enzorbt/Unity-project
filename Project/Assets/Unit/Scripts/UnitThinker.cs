using Common;
using Supinfo.Project.ScriptableObjects.Unit;
using UnityEngine;

namespace Supinfo.Project.Unit.Scripts
{
    public class UnitThinker: Thinker
    {
        [SerializeField] private UnitAttackSo unityAttack;
        public UnitAttackSo UnitAttack => unityAttack;
        
        [SerializeField] private UnitHealthSo unitHealth;
        public UnitHealthSo UnitHealth => unitHealth;
        
        [SerializeField] private UnitMovementSo unitMovement;
        public UnitMovementSo UnitMovement => unitMovement;

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