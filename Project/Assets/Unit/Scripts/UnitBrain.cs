using Common;
using Interfaces;
using ScriptableObjects.Unit;
using Supinfo.Project.Scripts.Interfaces;
using UnityEngine;

namespace Supinfo.Project.Unit.Scripts
{
    /// <summary>
    /// The UnitBrain class serves as an abstract base class for unit brains, defining common functionality for all unit brains.
    /// Inherits from the Brain class.
    /// </summary>
    public abstract class UnitBrain : Brain
    {
        /// <summary>
        /// Abstract method to be implemented by subclasses to handle attacking behavior.
        /// </summary>
        /// <param name="unitThinker">The UnitThinker component of the unit.</param>
        /// <param name="target">The target to attack.</param>
        protected abstract void Attack(UnitThinker unitThinker, Collider2D target);
    }
}