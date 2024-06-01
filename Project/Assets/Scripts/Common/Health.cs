using UnityEngine;
using UnityEngine.Serialization;

namespace Supinfo.Project.Scripts.Common
{
    /// <summary>
    /// The Health class is used for other classes to derived from, it implements the basic functionalities of a health manager.
    /// </summary>
    public abstract class Health : MonoBehaviour
    {
        /// <summary>
        /// Current health of the unit.
        /// </summary>
        public float CurHealth { get; set; }

        /// <summary>
        /// Maximum health of the unit.
        /// </summary>
        public float MaxHealth { get; set; }
        
        /// <summary>
        /// Start is called before the first frame update.
        /// Initializes current health to maximum health.
        /// </summary>
        private void Start()
        {
            CurHealth = MaxHealth;
        }
    }
}