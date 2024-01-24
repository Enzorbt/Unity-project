using System;
using Supinfo.Project.Scripts.Stats;
using UnityEngine;

namespace Supinfo.Project.Scripts.ScriptableObjects.Unit
{
    [CreateAssetMenu(menuName = "ScriptableObject/Units/UnitSpawnSo")]
    [Serializable]
    public class UnitSpawnSo : ScriptableObject
    {
        [Header("Prefab")] 
        [SerializeField] private GameObject prefab;
        public GameObject Prefab => prefab;
        
        [Header("Spawn Cooldown")] 
        [SerializeField] private Stat buildTime;
        public Stat BuildTime => buildTime;
        
        
    }
}