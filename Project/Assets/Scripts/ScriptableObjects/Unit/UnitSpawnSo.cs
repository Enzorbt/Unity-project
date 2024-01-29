using System;
using Supinfo.Project.ScriptableObjects.Common;
using Supinfo.Project.Scripts.Common.Stats;
using UnityEngine;

namespace Supinfo.Project.Scripts.ScriptableObjects.Unit
{
    [CreateAssetMenu(menuName = "ScriptableObject/Units/UnitSpawnSo")]
    public class UnitSpawnSo : SpawnSo
    {
        private void OnEnable()
        {
            currentAge = 0;
        }
        
        /// <summary>
        /// Build time of the unit
        /// </summary>
        [Header("Spawn Cooldown")] 
        [SerializeField] private Stat buildTime;
        public float BuildTime => buildTime.GetValue(currentAge, 0);
    }
}