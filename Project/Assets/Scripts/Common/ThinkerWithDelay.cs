using Common;
using UnityEngine;

namespace Supinfo.Project.Common
{
    /// <summary>
    /// The ThinkerWithDelay class is responsible for launching the ThinkWithDelay function of the BrainWithDelay class, it can take as a brain any BrainWithDelay object.
    /// </summary>
    public class ThinkerWithDelay : MonoBehaviour
    {
        /// <summary>
        /// The state of the thinker, true if already thinking, false otherwise.
        /// </summary>
        public bool IsThinking {get; set;}
        
        /// <summary>
        /// The brain of the thinker.
        /// </summary>
        [SerializeField] private BrainWithDelay brain;

        /// <summary>
        /// The property to set the brain.
        /// </summary>
        public BrainWithDelay Brain
        {
            get => brain;
            set => brain = value;
        }

        private void FixedUpdate()
        {
            if (!IsThinking && brain is not null)
            {
                StartCoroutine(brain.ThinkWithDelay(this));
            }
        }
    }
}