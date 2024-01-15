using Supinfo.Project.Stats;
using UnityEngine;
using UnityEngine.Serialization;

namespace ScriptableObjects
{
    /// <summary>
    ///  TurretData is the ScriptableObject that is used to store data for the different turret's types.
    /// </summary>
    [CreateAssetMenu(fileName = "TurretData", menuName = "ScriptableObject/Turrets/TurretData", order = 1)]
    public class TurretData : ScriptableObject
    {
        //--------- General properties ---------
        [Header("General properties")]
        // sprite data can be stored in the prefab or here
        [SerializeField] private GameObject[] prefabs;
        public GameObject[] Prefab => prefabs;

        //--------- Characteristics ---------
        [Header("Characteristics")] [SerializeField]
        private Stat price;
        public Stat Price => price;

        [SerializeField] private Stat damage;
        public Stat Damage => damage;
        
        [SerializeField] private Stat hitSpeed;
        public Stat HitSpeed => hitSpeed;
            
        [SerializeField] private Stat range;
        public Stat Range => range;
    }
}
