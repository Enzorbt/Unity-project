using System.Collections.Generic;
using UnityEngine;

namespace Supinfo.Project.UI.Scripts
{
    /// <summary>
    /// Manages the floor sprites in the UI, updating them based on the current age.
    /// </summary>
    public class FloorManager : MonoBehaviour
    {
        /// <summary>
        /// List of sprites for different ages.
        /// </summary>
        [SerializeField] private List<Sprite> sprites;
        
        /// <summary>
        /// Array of SpriteRenderer components in child objects.
        /// </summary>
        private SpriteRenderer[] _images;
        
        /// <summary>
        /// Current age index.
        /// </summary>
        private int _age;

        /// <summary>
        /// Initializes the floor manager, setting the initial age and getting the SpriteRenderer components.
        /// </summary>
        private void Awake()
        {
            _age = 0;
            _images = GetComponentsInChildren<SpriteRenderer>();
            ChangeSprites();
        }

        /// <summary>
        /// Event handler for age upgrade. Increments the age and updates the sprites if within bounds.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="data">The data passed with the event.</param>
        public void OnAgeUpgrade(Component sender, object data)
        {
            _age++;
            
            if (_age >= sprites.Count) return;
            
            ChangeSprites();
        }

        /// <summary>
        /// Updates the sprites of all child SpriteRenderer components to match the current age.
        /// </summary>
        private void ChangeSprites()
        {
            foreach (var image in _images)
            {
                image.sprite = sprites[_age];
            }
        }
    }
}