using System;
using UnityEngine;

namespace Supinfo.Project.Scripts.Events
{
    public class SpecialCapacity : MonoBehaviour
    {
        
        public void DisplayTags(Component sender, object data)
        {
            GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

            foreach (GameObject obj in allObjects)
            {
                if (obj.tag.Contains("Unit"))
                {
                    Debug.Log("GameObject: " + obj.name + " has tag: " + obj.tag);
                }
            }
        }
        
    }
}

// Ajouter la detection d'ID pour envoyer au joeur adverse 