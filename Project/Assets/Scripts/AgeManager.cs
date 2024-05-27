using System.Collections.Generic;
using ScriptableObjects.Turret;
using ScriptableObjects.Unit;
using Supinfo.Project.ScriptableObjects.Base;
using Supinfo.Project.Scripts.ScriptableObjects.Capacity;
using UnityEngine;

namespace Supinfo.Project.Scripts
{
    public class AgeManager : MonoBehaviour
    {
        [Header("All Stats Scriptable Objects")]
        [SerializeField] private List<UnitStatSo> statSosAllies;
        [SerializeField] private List<UnitStatSo> statSosEnemies;
        
        [SerializeField] private List<CapacitySo> capacitySosAllies;
        [SerializeField] private List<CapacitySo> capacitySosEnemies;
        
        [SerializeField] private BaseStatSo baseStatSoAllies;
        [SerializeField] private BaseStatSo baseStatSoEnemies;
        
        [SerializeField] private TurretStatSo turretStatSoAllies;
        [SerializeField] private TurretStatSo turretStatSoEnemies;
        
        
        public void UpgradeAgePlayer(Component sender, object data)
        {
            // Modify SOs with age
            foreach (var unitStatSo in statSosAllies)
            {
                unitStatSo.UpgradeAge();
            }
        }
        
        public void UpgradeAgeEnemy(Component sender, object data)
        {
            // Modify SOs with age
            foreach (var unitStatSo in statSosEnemies)
            {
                unitStatSo.UpgradeAge();
            }
        }
    }
}