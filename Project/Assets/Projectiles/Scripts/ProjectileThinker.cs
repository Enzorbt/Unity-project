using System;
using Common;
using Supinfo.Project.Scripts.ScriptableObjects.UnitTypes;
using UnityEngine;

namespace Supinfo.Project.Projectiles.Scripts
{
    public class ProjectileThinker: Thinker
    {
        public float Speed { get; set; }
        
        public float Damage { get; set; }

        public Vector3 Direction { get; set; }
        
        public UnitType UnitType { get; set; }
        
    }
}