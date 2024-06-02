using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Common
{
    /// <summary>
    /// The Thinker class is responsible for launching the Think function of the Brain class, it can take as a brain any Brain object.
    /// </summary>
    public class Thinker : MonoBehaviour
    {
        /// <summary>
        /// The brain of the thinker.
        /// </summary>
        [SerializeField] private Brain brain;
        
        private void FixedUpdate()
        {
            brain.Think(this);
        }
    }
}