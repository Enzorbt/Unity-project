using System;
using System.Collections.Generic;
using ScriptableObjects.Unit;
using UnityEngine;
using UnityEngine.Serialization;

namespace Supinfo.Project.Scripts
{
    public class AgeManager : MonoBehaviour
    {
        // Health SOs that can be modify with age
        [SerializeField] private List<UnitStatSo> unitStatSosAllies;
        [SerializeField] private List<UnitStatSo> unitStatSosEnemies;
        
        public void UpgradeAgePlayer(Component sender, object data)
        {
            // Modify SOs with age
            foreach (var unitStatSo in unitStatSosAllies)
            {
                unitStatSo.UpgradeAge();
            }
        }
        
        public void UpgradeAgeEnemy(Component sender, object data)
        {
            // Modify SOs with age
            foreach (var unitStatSo in unitStatSosEnemies)
            {
                unitStatSo.UpgradeAge();
            }
        }
    }
}