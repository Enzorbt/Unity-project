using System.Collections.Generic;
using UnityEngine;

namespace Supinfo.Project.Scripts.Events
{
    [CreateAssetMenu(menuName = "GameEvent")]
    public class GameEvent : ScriptableObject, IEvent
    {
        private List<GameEventListener> _listeners;

        // Raise event throught different methods signatures
        public void Raise(Component sender, object data)
        {
            for (int i = 0; i < _listeners.Count; i++)
            {
                _listeners[i].OnEventRaised(sender, data);
            }
        }

        // Manage listeners
        public void RegisterListener(GameEventListener listener)
        {
            if (_listeners.Contains(listener))
            {
                _listeners.Add(listener);
            }
        }

        public void UnregisterListener(GameEventListener listener)
        {
            if (_listeners.Contains(listener))
            {
                _listeners.Remove(listener);
            }
        }
    }
}