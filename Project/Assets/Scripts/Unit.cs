using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Supinfo.Project
{
    public class Unit : MonoBehaviour
    {

        // Public
        public int speed = 2;

        public Vector3 direction;

        // Private


        // Public methods

        public void SetDirection(Vector3 dir)
        {
            direction = dir;
        }

        // MonoBehaviour methods
        private void Start()
        {
            Debug.Log("unit direction: " + direction);
        }

        private void Update()
        {
            transform.Translate(speed * Time.deltaTime * direction);
        }

        // Private methods
    }
}