using System;
using Common;
using UnityEngine;

namespace Supinfo.Project.Projectiles.Scripts
{
    public class ProjectileThinker: Thinker
    {
        public Vector3 Direction { get; set; }
        
        public float Speed { get; set; }
        
        public float Damage { get; set; }
    }
}