// using System;
// using Supinfo.Project.ScriptableObjects.Common;
// using Supinfo.Project.ScriptableObjects.Unit;
// using Supinfo.Project.Scripts.Interfaces;
// using Supinfo.Project.Scripts.ScriptableObjects.Data;
// using UnityEngine;
//
// namespace Supinfo.Project.Unit.Scripts
// {
//     /// <summary>
//     /// The Unit class represents a unit in the game, controlling its movement and behavior.
//     /// It uses UnitData to access various properties such as speed and incorporates basic unit functionality.
//     /// </summary>
//     public class UnitMovementBACKUP : MonoBehaviour, IMovement
//     {
//         // Public fields
//
//         // Private fields
//
//         /// <summary>
//         /// Represents whether the unit is currently moving.
//         /// </summary>
//         private bool _isMoving = false;
//
//         /// <summary>
//         /// The direction in which the unit is moving.
//         /// </summary>
//         private Vector3 _direction;
//         
//         /// <summary>
//         /// Data containing the unit's properties like speed, health, etc.
//         /// </summary>
//         [SerializeField] private UnitMovementSo unitMovementSo;
//
//         /// <summary>
//         /// The movement speed of the unit, derived from unitData.
//         /// </summary>
//         private float _speed;
//
//         // Public methods
//         
//
//         // MonoBehaviour methods
//
//         public Vector3 GetDirection()
//         {
//             return _direction;
//         }
//         
//        
//         /// <summary>
//         /// Awake is called when the script instance is being loaded.
//         /// It initializes the unit's direction and speed based on its position and data.
//         /// </summary>
         // private void Awake()
         // {
         //     if (transform.position.x > 0)
         //     {
         //         _direction = Vector3.left;
         //     }
         //     else
         //     {
         //         _direction = Vector3.right;
         //     }
         //
         //     _speed = unitMovementSo.WalkSpeed;
         // }
//
//         private void Start()
//         {
//             _isMoving = true;
//         }
//
//         /// <summary>
//         /// Update is called once per frame and is responsible for moving the unit if it is set to move.
//         /// </summary>
//         private void Update()
//         {
//             if (_isMoving)
//             {
//                 Move();
//             }
//         }
//
//         // Private methods
//
//         /// <summary>
//         /// Moves the unit in the set direction at its current speed.
//         /// </summary>
//         public void Move()
//         {
//             transform.Translate(_speed * Time.deltaTime * _direction);
//         }
//
//         public void StopMovement()
//         {
//             _isMoving = false;
//         }
//
//         public void StartMovement()
//         {
//             _isMoving = true;
//         }
//     }
// }
