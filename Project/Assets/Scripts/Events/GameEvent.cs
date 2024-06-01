using System;
using System.Collections.Generic;
using UnityEngine;

namespace Supinfo.Project.Scripts.Events
{
    /// <summary>
    /// Wrapper for the built-in unity event.
    /// </summary>
    [CreateAssetMenu(menuName = "GameEvent")]
    public class GameEvent : ScriptableObject, IEvent
    {
        /// <summary>
        /// The list of all gameEventListeners that subscribe to the event.
        /// </summary>
        private List<GameEventListener> _gameEventListeners;

        private void Awake()
        {
            _gameEventListeners = new List<GameEventListener>();
        }

        public void Raise(Component sender, object data)
        {
            foreach (var gameEventListener in _gameEventListeners)
            {
                gameEventListener.OnEventRaised(sender, data);
            }
        }
        
        public void RegisterListener(GameEventListener listener)
        {
            if (!_gameEventListeners.Contains(listener))
            {
                _gameEventListeners.Add(listener);
            }
        }
        
        public void UnregisterListener(GameEventListener listener)
        {
            if (_gameEventListeners.Contains(listener))
            {
                _gameEventListeners.Remove(listener);
            }
        }
    }
}