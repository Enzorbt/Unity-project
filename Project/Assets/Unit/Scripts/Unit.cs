using Supinfo.Project.Scripts.ScriptableObjects.Data;
using UnityEngine;

namespace Supinfo.Project.Unit.Scripts
{
    public class Unit : MonoBehaviour
    {

        // Public fields

        // Private fields
        private bool _isMoving = true;
        private Vector3 _direction;
        
        [SerializeField] private UnitData unitData;

        private float _speed;
        
        private int _age = 0;
        private int _upgrades = 0;

        // Public methods
        

        // MonoBehaviour methods
        
        public void SetIsMoving(bool value)
        {
            _isMoving = value;
        }

        public Vector3 GetDirection()
        {
            return _direction;
        }
        
        
        // Private methode 
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

            _speed = unitData.WalkSpeed.GetValue();
        }

        private void Start()
        {
            Debug.Log("unit direction: " + _direction);
            Debug.Log("unit speed: " + _speed);
        }

        private void Update()
        {
            if (_isMoving)
            {
                Move();
            }
        }

        // Private methods

        private void Move()
        {
            transform.Translate(_speed * Time.deltaTime * _direction);
        }
    }
}