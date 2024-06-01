using System;
using Supinfo.Project.Scripts.Events;
using Supinfo.Project.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Supinfo.Project.UI.Scripts
{
    /// <summary>
    /// Class that manages the enter and exit of the option panel.
    /// </summary>
    public class OptionPanel : MonoBehaviour
    {
        /// <summary>
        /// The game event that changes the speed of the game.
        /// </summary>
        [SerializeField] private GameEvent onGameSpeedChange;
        
        /// <summary>
        /// The game event that changes the state of the drag manager.
        /// </summary>
        [SerializeField] private GameEvent onDragStateChange;

        private Image _image;

        private void Start()
        {
            _image = GetComponent<Image>();
            if (_image is null) return;
            _image.raycastTarget = false;
        }

        /// <summary>
        /// Called when the option panel and return button is clicked.
        /// </summary>
        /// <param name="state">The state of the menu (true when opened and false when closed).</param>
        public void OnClick(bool state)
        {
            onGameSpeedChange.Raise(this, state ? GameSpeed.Play : GameSpeed.Stop);
            onDragStateChange.Raise(this, state);
            if (_image is null) return;
            _image.raycastTarget = !state;
        }
    }
}