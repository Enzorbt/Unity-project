using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Supinfo.Project
{
    public class Unit : MonoBehaviour
    {

        // Public fields
        public int speed = 2;

        private Vector3 _direction;

        // Private fields


        // Public methods
        

        // MonoBehaviour methods
        private void Awake()
        {
            if (transform.position.x > 0)
            {
                _direction = Vector3.left;
            }
            else
            {
                _direction = Vector3.right;
            }
        }

        private void Start()
        {
            Debug.Log("unit direction: " + _direction);
        }

        private void Update()
        {
            transform.Translate(speed * Time.deltaTime * _direction);
        }

        // Private methods
    }
}