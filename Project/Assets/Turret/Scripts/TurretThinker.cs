using System;
using System.Collections;
using Common;
using ScriptableObjects.Turret;
using UnityEngine;
using UnityEngine.Serialization;

namespace Supinfo.Project.Turret.Scripts
{
    /// <summary>
    /// The TurretThinker class represents the decision-making component of a turret.
    /// Inherits from the Thinker class.
    /// </summary>
    public class TurretThinker : Thinker
    {
        /// <summary>
        /// The TurretStatSo associated with the turret.
        /// </summary>
        public TurretStatSo TurretStatSo { get; set; }

        /// <summary>
        /// The SpriteRenderer component used to render the turret's sprite.
        /// </summary>
        private SpriteRenderer _spriteRenderer;

        /// <summary>
        /// Initializes the SpriteRenderer component.
        /// </summary>
        private void Awake()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        /// <summary>
        /// Handles the event when the turret is upgraded.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="data">The data associated with the event.</param>
        public void OnAgeUpgrade(Component sender, object data)
        {
            // Update the sprite after a delay
            StartCoroutine(ChangeSprite());
        }

        /// <summary>
        /// Coroutine to change the sprite after a delay.
        /// </summary>
        /// <returns>An IEnumerator to handle the coroutine.</returns>
        private IEnumerator ChangeSprite()
        {
            yield return new WaitForSeconds(1f);
            _spriteRenderer.sprite = TurretStatSo.Sprite;
        }
    }
}