using System;
using System.Collections;
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
        [SerializeField]
        private UnitStatSo unitStatSo;
    
        /// <summary>
        /// Event to be raised when the button is clicked.
        /// </summary>
        [SerializeField] private GameEvent onClick;

        private bool spawning;
        private UnityEngine.UI.Button button;

        private void Awake()
        {
            button = transform.GetComponentInChildren<UnityEngine.UI.Button>();
        }

        /// <summary>
        /// Method to be called when the button is clicked.
        /// Raises the onClick event with the associated unit.
        /// </summary>
        public void OnClick()
        {
            if (!spawning)
            {
                StartCoroutine(SpawnWithCooldown());
            }
        }

        private IEnumerator SpawnWithCooldown()
        {
            spawning = true;
            
            EnableButton(false);

            yield return new WaitForSeconds(unitStatSo.BuildTime);
            
            onClick.Raise(this, unitStatSo);
            
            EnableButton(true);
            
            spawning = false;
        }

        // function to change visual and enable the button
        private void EnableButton(bool value)
        {
            if (button is null) return;
            button.enabled = value;
        }

        public void OnSpawnQueueStatusChange(Component sender, object data)
        {
            if (data is not bool status) return;
            
            EnableButton(status);
        }
    }

}
