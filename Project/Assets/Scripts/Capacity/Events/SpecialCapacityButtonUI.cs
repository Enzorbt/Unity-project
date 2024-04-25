using Supinfo.Project.Scripts.Events;
using Supinfo.Project.Scripts.ScriptableObjects.Unit;
using UnityEngine;
using Supinfo.Project.Scripts.Events;

namespace Supinfo.Project.Scripts.Events
{
    public class SpecialCapacityButtonUI : MonoBehaviour
    {
        /// <summary>
        /// Event to be raised when the button is clicked.
        /// </summary>
        [SerializeField] private GameEvent onClick;

        /// <summary>
        /// Method to be called when the button is clicked.
        /// Raises the onClick event with the associated unit.
        /// </summary>
        public void OnClick()
        {
            onClick.Raise(this, null);
        }
    }

}
