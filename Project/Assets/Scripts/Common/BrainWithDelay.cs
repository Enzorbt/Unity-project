using System.Collections;
using UnityEngine;

namespace Supinfo.Project.Common
{
    public abstract class BrainWithDelay : ScriptableObject
    {
        [SerializeField] protected float delayTime;
        public abstract IEnumerator ThinkWithDelay(ThinkerWithDelay thinker);
    }
}