using UnityEngine;

namespace Supinfo.Project.Scripts.Common
{
    public abstract class Health : MonoBehaviour
    {
        /// <summary>
        /// Current health of the unit.
        /// </summary>
        protected float curHealth;

        /// <summary>
        /// Maximum health of the unit.
        /// </summary>
        protected float maxHealth;
        
        /// <summary>
        /// Start is called before the first frame update.
        /// Initializes current health to maximum health.
        /// </summary>
        void Start()
        {
            curHealth = maxHealth;
        }
    }
}