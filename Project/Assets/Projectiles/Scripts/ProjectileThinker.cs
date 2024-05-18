using System;
using Common;
using UnityEngine;

namespace Supinfo.Project.Projectiles.Scripts
{
    public class ProjectileThinker: Thinker
    {
        public float Speed { get; set; }
        
        public float Damage { get; set; }

        public Vector3 Direction { get; set; }
        
    }
}