using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Common
{
    public class Thinker : MonoBehaviour
    {
        [SerializeField]
        private Brain brain;

        public Brain Brain => brain;

        private void Update()
        {
            brain.Think(this);
        }
    }
}