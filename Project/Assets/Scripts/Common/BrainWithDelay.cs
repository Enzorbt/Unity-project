using System.Collections;
using UnityEngine;

namespace Supinfo.Project.Common
{
    /// <summary>
    /// The BrainWithDelay class is the basic abstract class for all brain that uses delay, allowing game objects to "Think with a cooldown".
    /// It allows a great deal of modularity for all classes using the same system. 
    /// </summary>
    public abstract class BrainWithDelay : ScriptableObject
    {
        /// <summary>
        /// The delay to wait when thinking
        /// </summary>
        [SerializeField] protected float delayTime;
        
        /// <summary>
        /// Function to be called by the thinker.
        /// </summary>
        /// <param name="thinker">The class that called the function.</param>
        public abstract IEnumerator ThinkWithDelay(ThinkerWithDelay thinker);
    }
}