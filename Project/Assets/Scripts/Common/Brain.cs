using UnityEngine;

namespace Common
{
    /// <summary>
    /// The Brain class is the basic abstract class for all brain, allowing game objects to "Think".
    /// It allows a great deal of modularity for all classes using the same system. 
    /// </summary>
    public abstract class Brain : ScriptableObject
    {
        /// <summary>
        /// Function to be called by the thinker.
        /// </summary>
        /// <param name="thinker">The class that called the function.</param>
        public abstract void Think(Thinker thinker);
    }
}