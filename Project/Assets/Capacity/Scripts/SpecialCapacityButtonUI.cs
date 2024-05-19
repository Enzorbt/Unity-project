using System;
using System.Collections.Generic;
using Supinfo.Project.Scripts.Events;
using Supinfo.Project.Scripts.ScriptableObjects.Capacity;
using UnityEngine;

namespace Capacity.Events
{
    public class SpecialCapacityButtonUI : MonoBehaviour
    {
        private float _xpMax;
        
        /// <summary>
        /// On capacity use event
        /// </summary>
        [SerializeField] private GameEvent onClick;
        
        /// <summary>
        /// Event to be raised when the button is clicked.
        /// </summary>
        [SerializeField] private GameEvent onXpChange;

        [SerializeField]
        private CapacitySo _capacitySo;
        
        [Range(0,1)]
        [SerializeField] private float cost;

        private void Awake()
        {
            SetActiveButton(false);
        }


        /// <summary>
        /// Method to be called when the button is clicked.
        /// Raises the onClick event with the associated unit.
        /// </summary>
        public void OnClick()
        {
            onClick.Raise(this, _capacitySo);
            onXpChange.Raise(this, - (_xpMax * cost));
        }

        public void OnXpRatioChange(Component sender, object data)
        {
            if (data is not float xpRatio) return;
            
            // activate the button
            SetActiveButton(xpRatio >= cost);
        }

        private void SetActiveButton(bool state)
        {
            gameObject.GetComponentInChildren<UnityEngine.UI.Button>().enabled = state;
        }
        
        public void OnXpMaxChange(Component sender, object data)
        {
            if (data is not float xpMax) return;
            
            // update xp count
            _xpMax = xpMax;
        }
    }
}
