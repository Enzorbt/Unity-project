using System.Collections.Generic;
using Common;
using Supinfo.Project.Scripts;
using UnityEngine;

namespace IA.Event
{
    public abstract class IAThinker : Thinker
    {
        protected float Gold {get; set;}
        protected float Xp {get; set;}
        protected int Age {get; set;}
        protected int TurretNumber {get; set;}
        protected bool IsUnlock {get; set;}
        public Dictionary<UpgradeType, int> UpgradeDict; // Unity
    }
}