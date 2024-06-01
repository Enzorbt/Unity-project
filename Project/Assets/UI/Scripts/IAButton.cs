using Supinfo.Project.Scripts.Events;
using UnityEngine;

namespace Supinfo.Project.UI.Scripts
{
    /// <summary>
    /// This class manages the behavior of an IA choice button in the UI.
    /// </summary>
    public class IAButton : MonoBehaviour
    {
        /// <summary>
        /// The GameEvent that is triggered when an IA choice is made.
        /// </summary>
        [SerializeField] private GameEvent onIAChoice;

        /// <summary>
        /// The IA choice associated with this button.
        /// </summary>
        [SerializeField] private IAChoice iaChoice;

        /// <summary>
        /// This method is called when the button is clicked.
        /// It triggers the onIAChoice event with the associated IA choice.
        /// </summary>
        public void MakeIAChoice()
        {
            // Trigger the onIAChoice event with the associated IA choice
            onIAChoice.Raise(this, iaChoice);
        }
    }
}
