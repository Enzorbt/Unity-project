using ScriptableObjects.Unit;
using Supinfo.Project.Scripts.Events;
using UnityEngine;
using UnityEngine.Serialization;

namespace Supinfo.Project.UI.Button.Scripts
{
    public class SpawnButtonUI : MonoBehaviour
    {
        /// <summary>
        /// The unit spawn So with the prefab and cooldown stat
        /// </summary>
        [FormerlySerializedAs("unitSpawnSo")] [SerializeField]
        private UnitStatSo unitStatSo;
    
        /// <summary>
        /// Event to be raised when the button is clicked.
        /// </summary>
        [SerializeField] private GameEvent onClick;

        /// <summary>
        /// Method to be called when the button is clicked.
        /// Raises the onClick event with the associated unit.
        /// </summary>
        public void OnClick()
        {
            onClick.Raise(this, unitStatSo);
        }
    }

}
