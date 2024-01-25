using Supinfo.Project.Scripts.Common.Stats;
using UnityEngine;

namespace Supinfo.Project.ScriptableObjects.Common
{
    public class SpawnSo : ScriptableObject
    {
        /// <summary>
        /// Prefab for the unit
        /// </summary>
        [Header("Prefabs - needs at least 1, max 7")] 
        [SerializeField] private GameObject[] prefabs;

        public GameObject GetPrefabWithAge (int age = 0)
        {
            return prefabs[age];
        }
        
        /// <summary>
        /// Purchase price of the unit.
        /// </summary>
        [Header("Price")] 
        [SerializeField] private Stat price;
        public Stat Price => price; 
    }
}