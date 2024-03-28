using Common;
using ScriptableObjects.Turret;
using UnityEngine;

namespace Supinfo.Project.Turret.Scripts
{
    public class TurretThinker : Thinker
    {
        [SerializeField] private TurretAttackSo turretAttackSo;
        public TurretAttackSo TurretAttackSo => turretAttackSo;
    }
}