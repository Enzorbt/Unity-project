using Supinfo.Project.Scripts.Common.Stats;
using Supinfo.Project.Scripts.Interfaces;
using UnityEngine;

namespace Supinfo.Project.ScriptableObjects.Common
{
    public class SpawnSo : ScriptableObject, IAgeUpgradable
    {
        protected int currentAge;
        
        /// <summary>
        /// Prefab for the unit
        /// </summary>
        [Header("Prefabs - needs at least 1, max 7")] 
        [SerializeField] private GameObject[] prefabs;

        public GameObject GetPrefab ()
        {
            return prefabs[currentAge];
        }
        
        /// <summary>
        /// Purchase price of the unit.
        /// </summary>
        [Header("Price")] 
        [SerializeField] private Stat price;
        public Stat Price => price;
        public void UpgradeAge()
        {
            currentAge++;
        }
    }
}