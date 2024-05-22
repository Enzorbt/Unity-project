using UnityEngine;

namespace Supinfo.Project.Scripts.Events
{
    public interface IEvent
    {
        // Raise Event
        public void Raise(Component sender, object data);

        // Manage listeners
        public void RegisterListener(GameEventListener listener);

        public void UnregisterListener(GameEventListener listener);
    }
}