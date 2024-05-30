using Supinfo.Project.Scripts.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Supinfo.Project.UI.Scripts
{
    public class IAButton : MonoBehaviour
    {
        [SerializeField] private GameEvent onIAChoice;
        [SerializeField] private IAChoice iaChoice;
        
        public void MakeIAChoice()
        {
            onIAChoice.Raise(this, iaChoice);
        }
    }
}