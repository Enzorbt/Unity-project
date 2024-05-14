using Common;
using ScriptableObjects.Turret;
using UnityEngine;
using UnityEngine.Serialization;

namespace Supinfo.Project.Turret.Scripts
{
    public class TurretThinker : Thinker
    {
        [FormerlySerializedAs("turretAttackSo")] [SerializeField] private TurretStatSo turretStatSo;
        public TurretStatSo TurretStatSo => turretStatSo;
    }
}