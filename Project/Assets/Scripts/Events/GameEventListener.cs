using System;
using UnityEngine;
using UnityEngine.Events;

namespace Supinfo.Project.Scripts.Events
{
    /// <summary>
    /// CustomUnityEvent extends UnityEvent with two parameters:
    /// - on of type Component (which is more general than monoBehaviour
    /// - on of type object, which is the an alias of System.Object, in .NET, every types derive from it.
    /// </summary>
    [Serializable]
    public class CustomUnityEvent : UnityEvent<Component, object>{ }
    
    /// <summary>
    /// GameEventListener
    /// </summary>
    public class GameEventListener : MonoBehaviour
    {
        private GameEvent gameEvent;

        public CustomUnityEvent response;
        private void OnEnable()
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