using System;
using ScriptableObjects;
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
        private UnitData _unitData;

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
            // loading scriptable objects must be done in the Start function and not Awake, otherwise an error is thrown 
            _unitData = Resources.Load<UnitData>("Units/MeleeData");
        }

        private void Update()
        {
            transform.Translate(_unitData.WalkSpeed.GetValue() * Time.deltaTime * _direction);
        }

        // Private methods
    }
}