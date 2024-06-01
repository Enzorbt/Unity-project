using System;
using UnityEngine;
using UnityEngine.Events;

namespace Supinfo.Project.Scripts.Events
{
    /// <summary>
    /// CustomUnityEvent extends UnityEvent with two parameters:
    /// - one of type Component (which is more general than monoBehaviour).
    /// - one of type object, which is the an alias of System.Object, in .NET, every types derive from it.
    /// </summary>
    [Serializable]
    public class CustomUnityEvent : UnityEvent<Component, object>{ }
    
    /// <summary>
    /// Class that allows game object to listen to an event and using the component based architecture of Unity.
    /// </summary>
    public class GameEventListener : MonoBehaviour
    {
        /// <summary>
        /// The GameEvent to listen.
        /// </summary>
        [SerializeField] private GameEvent gameEvent;

        /// <summary>
        /// The response to trigger when a game event is triggered.
        /// </summary>
        public CustomUnityEvent response;
        
        private void Start()
        {
            gameEvent.RegisterListener(this);
        }

        private void OnDisable()
        {
            gameEvent.UnregisterListener(this);
        }

        public void OnEventRaised(Component sender, object data)
        {
            response.Invoke(sender, data);   
        }
    }
}