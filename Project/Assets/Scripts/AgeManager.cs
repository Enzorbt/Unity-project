using System;
using System.Collections.Generic;
using Supinfo.Project.ScriptableObjects.Common;
using UnityEngine;
using UnityEngine.Serialization;

namespace Supinfo.Project.Scripts
{
    public class AgeManager : MonoBehaviour
    {
        // Health SOs that can be modify with age
        [SerializeField] private List<HealthSo> healthSosPlayer;
        [SerializeField] private List<HealthSo> healthSosEnemy;
        
        // Attack SOs that can be modify with age
        [SerializeField] private List<AttackSo> attackSosPlayer;
        [SerializeField] private List<AttackSo> attackSosEnemy; 
        
        // Spawn SOs that can be modify with age
        [SerializeField] private List<SpawnSo> spawnSosPlayer;
        [SerializeField] private List<SpawnSo> spawnSosEnemy;
        
        public void UpgradeAgePlayer(Component sender, object data)
        {
            // Modify SOs with age
            foreach (AttackSo attackSo in attackSosPlayer)
            {
                // call function to set age in SO
            }

            foreach (HealthSo healthSo in healthSosPlayer)
            {
                
            }
            
            foreach (SpawnSo spawnSo in spawnSosPlayer)
            {
                
            }
        }
        
        public void UpgradeAgeEnemy(Component sender, object data)
        {
            // Modify SOs with age
            foreach (AttackSo attackSo in attackSosEnemy)
            {
                // call function to set age in SO
            }

            foreach (HealthSo healthSo in healthSosEnemy)
            {
                
            }
            
            foreach (SpawnSo spawnSo in spawnSosEnemy)
            {
                
            }
        }
    }
}