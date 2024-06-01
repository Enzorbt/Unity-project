using UnityEngine;

namespace Supinfo.Project.Scripts.Events
{
    /// <summary>
    /// The interface for our game events.
    /// </summary>
    public interface IEvent
    {
        /// <summary>
        /// Function called to trigger the event.
        /// </summary>
        /// <param name="sender">The sender of the game event.</param>
        /// <param name="data">The data being transferred.</param>
        public void Raise(Component sender, object data);

        /// <summary>
        /// Register the listener to the list of game event listeners.
        /// </summary>
        /// <param name="listener">The listener to register.</param>
        public void RegisterListener(GameEventListener listener);

        /// <summary>
        /// Unregister the listener to the list of game event listeners.
        /// </summary>
        /// <param name="listener">The listener to unregister.</param>
        public void UnregisterListener(GameEventListener listener);
    }
}