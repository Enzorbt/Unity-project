using Supinfo.Project.Scripts.ScriptableObjects.Data;
using UnityEngine;

namespace Supinfo.Project.Unit.Scripts
{
    /// <summary>
    /// The Unit class represents a unit in the game, controlling its movement and behavior.
    /// It uses UnitData to access various properties such as speed and incorporates basic unit functionality.
    /// </summary>
    public class Unit : MonoBehaviour
    {
        // Public fields

        // Private fields

        /// <summary>
        /// Represents whether the unit is currently moving.
        /// </summary>
        private bool _isMoving = true;

        /// <summary>
        /// The direction in which the unit is moving.
        /// </summary>
        private Vector3 _direction;
        
        /// <summary>
        /// Data containing the unit's properties like speed, health, etc.
        /// </summary>
        [SerializeField] private UnitData unitData;

        /// <summary>
        /// The movement speed of the unit, derived from unitData.
        /// </summary>
        private float _speed;
        
        /// <summary>
        /// The age of the unit, can be used for unit lifecycle management.
        /// </summary>
        private int _age = 0;

        /// <summary>
        /// Number of upgrades applied to the unit.
        /// </summary>
        private int _upgrades = 0;

        // Public methods
        

        // MonoBehaviour methods

        /// <summary>
        /// Sets the movement state of the unit.
        /// </summary>
        /// <param name="value">Boolean value to set the _isMoving field.</param>
        public void SetIsMoving(bool value)
        {
            _isMoving = value;
        }

        public Vector3 GetDirection()
        {
            return _direction;
        }
        
       
        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// It initializes the unit's direction and speed based on its position and data.
        /// </summary>
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

        /// <summary>
        /// Start is called before the first frame update.
        /// It logs the initial direction and speed of the unit.
        /// </summary>
        private void Start()
        {
            // Debug.Log("unit direction: " + _direction);
            // Debug.Log("unit speed: " + _speed);
        }

        /// <summary>
        /// Update is called once per frame and is responsible for moving the unit if it is set to move.
        /// </summary>
        private void Update()
        {
            if (_isMoving)
            {
                Move();
            }
        }

        // Private methods

        /// <summary>
        /// Moves the unit in the set direction at its current speed.
        /// </summary>
        private void Move()
        {
            transform.Translate(_speed * Time.deltaTime * _direction);
        }
    }
}
