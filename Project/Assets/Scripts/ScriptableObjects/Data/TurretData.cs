using Supinfo.Project.Scripts.Common.Stats;
using UnityEngine;

namespace Supinfo.Project.Scripts.ScriptableObjects.Data
{
    /// <summary>
    /// TurretData is the ScriptableObject that is used to store data for the different turret types.
    /// </summary>
    // [CreateAssetMenu(fileName = "TurretData", menuName = "ScriptableObject/Turrets/TurretData", order = 1)]
    public class TurretData : ScriptableObject
    {
        //--------- General properties ---------
        /// <summary>
        /// An array of prefabs for the turret. Sprite data can be stored in the prefab or here.
        /// </summary>
        [Header("General properties")]
        [SerializeField] private GameObject[] prefabs;

        /// <summary>
        /// Gets the array of prefabs.
        /// </summary>
        public GameObject[] Prefabs => prefabs;

        //--------- Characteristics ---------
        /// <summary>
        /// The price of the turret.
        /// </summary>
        [Header("Characteristics")]
        [SerializeField] private Stat price;

        /// <summary>
        /// Gets the price stat of the turret.
        /// </summary>
        public Stat Price => price;

        /// <summary>
        /// The damage dealt by the turret.
        /// </summary>
        [SerializeField] private Stat damage;

        /// <summary>
        /// Gets the damage stat of the turret.
        /// </summary>
        public Stat Damage => damage;
        
        /// <summary>
        /// The hit speed of the turret.
        /// </summary>
        [SerializeField] private Stat hitSpeed;

        /// <summary>
        /// Gets the hit speed stat of the turret.
        /// </summary>
        public Stat HitSpeed => hitSpeed;
        
        /// <summary>
        /// The range of the turret.
        /// </summary>
        [SerializeField] private Stat range;

        /// <summary>
        /// Gets the range stat of the turret.
        /// </summary>
        public Stat Range => range;
    }
}
