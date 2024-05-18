using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Common
{
    public class Thinker : MonoBehaviour
    {
        [SerializeField]
        private Brain brain;

        private void FixedUpdate()
        {
            brain.Think(this);
        }
    }
}