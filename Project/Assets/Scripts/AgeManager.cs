using System.Collections.Generic;
using ScriptableObjects.Unit;
using UnityEngine;

namespace Supinfo.Project.Scripts
{
    public class AgeManager : MonoBehaviour
    {
        // Health SOs that can be modify with age
        [SerializeField] private List<UnitStatSo> statSosAllies;
        [SerializeField] private List<UnitStatSo> statSosEnemies;
        
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