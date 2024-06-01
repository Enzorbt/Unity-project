using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Supinfo.Project.Background.Scripts
{
    /// <summary>
    /// Manage the backgrounds of the game scene
    /// </summary>
    public class BackgroundManager : MonoBehaviour
    {
        /// <summary>
        /// The list of all the backgrounds.
        /// </summary>
        [SerializeField] private List<Sprite> _backgrounds;
        /// <summary>
        /// The current age of the game.
        /// </summary>
        private int _age;
        /// <summary>
        /// The image component of the game object.
        /// </summary>
        private Image _image;

        /// <summary>
        /// Called when the game object is instantiated.
        /// </summary>
        private void Awake()
        {
            _image = GetComponentInChildren<Image>();
        }

        /// <summary>
        /// Game event listener function called when the event onAgeUpgrade is triggered (linked to a GameEventListener component).
        /// </summary>
        /// <param name="sender">The sender of the game event.</param>
        /// <param name="data">The data being transferred.</param>
        public void OnAgeUpgrade(Component sender, object data)
        {
            _age++;
            _image.sprite = _backgrounds[_age];
        }
    }
}