using System;
using Supinfo.Project.Scripts.Common.Stats;
using UnityEngine;

namespace Supinfo.Project.Scripts.ScriptableObjects.Unit
{
    [CreateAssetMenu(menuName = "ScriptableObject/Units/UnitSpawnSo")]
    public class UnitSpawnSo : ScriptableObject
    {
        /// <summary>
        /// Prefab for the unit
        /// </summary>
        [Header("Prefab")] 
        [SerializeField] private GameObject prefab;
        public GameObject Prefab => prefab;
        
        /// <summary>
        /// Build time of the unit
        /// </summary>
        [Header("Spawn Cooldown")] 
        [SerializeField] private Stat buildTime;
        public Stat BuildTime => buildTime;
        
        /// <summary>
        /// Purchase price of the unit.
        /// </summary>
        [SerializeField] private Stat price;
        public Stat Price => price; 
        
        
    }
}