using Supinfo.Project.Scripts.ScriptableObjects;
using UnityEngine;

namespace Supinfo.Project.Scripts
{
    public class Unit : MonoBehaviour
    {

        // Public fields

        // Private fields
        private Vector3 _direction;
        
        private readonly UnitData _unitData = Resources.Load<UnitData>("Units/MeleeData");

        private float _speed;
        
        private int _age = 0;
        private int _upgrades = 0;

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

            _speed = _unitData.WalkSpeed.GetValue(_age,_upgrades);
        }

        private void Start()
        { 
            
        }

        private void Update()
        {
            Move();
        }

        // Private methods

        private void Move()
        {
            transform.Translate(_speed * Time.deltaTime * _direction);
        }
    }
}