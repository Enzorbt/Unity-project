using UnityEngine;

namespace Common
{
    public abstract class Brain : ScriptableObject
    {
        public abstract void Think(Thinker think);
    }
}