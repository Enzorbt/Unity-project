using Supinfo.Project.Scripts.Events;
using Supinfo.Project.Scripts.ScriptableObjects.Capacity;
using UnityEngine;

namespace Supinfo.Project.Capacity.Scripts
{
    public class SpecialCapacityButtonUI : MonoBehaviour
    {
        /// <summary>
        /// Event to be raised when the button is clicked.
        /// </summary>
        [SerializeField] private GameEvent onClick;

        [SerializeField]
        private CapacitySo _capacitySo;

        
        /// <summary>
        /// Method to be called when the button is clicked.
        /// Raises the onClick event with the associated unit.
        /// </summary>
        public void OnClick()
        {
            onClick.Raise(this, _capacitySo);
        }
    }
}
