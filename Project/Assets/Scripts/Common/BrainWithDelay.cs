using System.Collections;
using UnityEngine;

namespace Supinfo.Project.Common
{
    /// <summary>
    /// Function that calls with delay game event to make action
    /// </summary>
    public abstract class BrainWithDelay : ScriptableObject
    {
        [SerializeField] protected float delayTime;
        public abstract IEnumerator ThinkWithDelay(ThinkerWithDelay thinker);
    }
}