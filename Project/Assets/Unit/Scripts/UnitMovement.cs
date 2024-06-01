using System;
using Supinfo.Project.Scripts.Interfaces;
using UnityEngine;

namespace Supinfo.Project.Unit.Scripts
{
    /// <summary>
    /// The UnitMovement class handles movement behavior for units.
    /// It implements the IMovement interface.
    /// </summary>
    public class UnitMovement : MonoBehaviour, IMovement
    {
        /// <summary>
        /// Reference to the Animator component.
        /// </summary>
        private Animator _animator;

        /// <summary>
        /// Initializes the UnitMovement component.
        /// </summary>
        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        /// <summary>
        /// Moves the unit in the specified direction with the given speed.
        /// </summary>
        /// <param name="direction">The direction of movement.</param>
        /// <param name="speed">The speed of movement.</param>
        public void Move(Vector3 direction, float speed)
        {
            // Set the walk animation
            _animator.SetBool("walk", true);

            // Move the unit
            transform.Translate(speed * Time.deltaTime * direction);
        }
    }
}