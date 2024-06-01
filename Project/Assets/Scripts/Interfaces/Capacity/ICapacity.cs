using UnityEngine;

namespace Supinfo.Project.Interfaces.Capacity
{
    /// <summary>
    /// Interface for objects that have a capacity that can be launched.
    /// </summary>
    public interface ICapacity
    {
        /// <summary>
        /// Game event listener function called when the event onLaunchCapacity is triggered (linked to a GameEventListener component).
        /// </summary>
        /// <param name="sender">The sender of the game event.</param>
        /// <param name="data">The data being transferred.</param>
        public void LaunchCapacity(Component sender, object data);
    }
}