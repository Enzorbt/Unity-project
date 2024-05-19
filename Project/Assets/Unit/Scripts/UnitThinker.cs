using Common;
using ScriptableObjects.Unit;
using Supinfo.Project.Scripts.ScriptableObjects.UnitTypes;
using UnityEngine;

namespace Supinfo.Project.Unit.Scripts
{
    public class UnitThinker: Thinker
    {

        // Stats - fixed (to use in brain)
        public float Damage { get; set; }
        public float WalkSpeed { get; set; }
        public float HitSpeed { get; set; }
        public float Range { get; set; }
        
        public UnitType UnitType { get; set; }
        

        public Vector3 Direction { get; set; }
        private void Awake()
        {
            Direction = transform.position.x > 0 ? Vector3.left : Vector3.right;
        }
    }
}