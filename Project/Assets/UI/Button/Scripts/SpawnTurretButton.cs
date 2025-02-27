﻿using System;
using System.Collections;
using ScriptableObjects.Turret;
using Supinfo.Project.Scripts.Events;
using Supinfo.Project.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Supinfo.Project.UI.Button.Scripts
{
    /// <summary>
    /// Handles the functionality of a button to spawn turrets.
    /// </summary>
    public class SpawnTurretButton : MonoBehaviour
    {
        /// <summary>
        /// Event triggered when a turret is spawned.
        /// </summary>
        [SerializeField] private GameEvent onSpawnTurret;

        /// <summary>
        /// Event triggered when the gold count changes.
        /// </summary>
        [SerializeField] private GameEvent onGoldChange;

        /// <summary>
        /// The turret statistics ScriptableObject.
        /// </summary>
        [SerializeField] private TurretStatSo turretStatSo;
        
        /// <summary>
        /// The number of turrets spawned.
        /// </summary>
        private int _spawnNumber;

        /// <summary>
        /// The button component.
        /// </summary>
        private UnityEngine.UI.Button _button;

        /// <summary>
        /// The current gold count.
        /// </summary>
        private float _goldCount;

        /// <summary>
        /// The image component used for visuals.
        /// </summary>
        private Image _image;

        /// <summary>
        /// The image component of the button
        /// </summary>
        private Image _imageButton;

        private void Awake()
        {
            _button = GetComponentInChildren<UnityEngine.UI.Button>();
            _image = GetComponentsInChildren<Image>()[1];
            _imageButton = GetComponentsInChildren<Image>()[0];
        }
        
        public void OnClick()
        {
            onSpawnTurret.Raise(this, turretStatSo);
            onGoldChange.Raise(this, -turretStatSo.Price);
            _spawnNumber++;
            EnableButton(_spawnNumber <= 3 && _goldCount >= turretStatSo.Price);
        }

        /// <summary>
        /// Enables or disables the button based on conditions.
        /// </summary>
        private void EnableButton(bool value)
        {
            if (_button is null) return;
            if (_imageButton is null) return;
            _button.enabled = value;
            _imageButton.color = value ? new Color(1,1,1,0.4f) : new Color(1,0,0,0.4f);
        }

        /// <summary>
        /// Handles the event when the game speed changes.
        /// </summary>
        public void OnGameSpeedChange(Component sender, object data)
        {
            if (data is not GameSpeed gameSpeed) return;
            EnableButton(gameSpeed == GameSpeed.Stop ? false : _spawnNumber <= 3 && _goldCount >= turretStatSo.Price);
        }

        /// <summary>
        /// Handles the event when the gold count changes.
        /// </summary>
        public void OnGoldCountChange(Component sender, object data)
        {
            if(data is not float goldCount) return;
            _goldCount = goldCount;
            EnableButton(_spawnNumber <= 3 && _goldCount >= turretStatSo.Price);
        }
        
        /// <summary>
        /// Handles the event when the age upgrades.
        /// </summary>
        public void OnAgeUpgrade(Component sender, object data)
        {
            StartCoroutine(ChangeSprite());
        }

        /// <summary>
        /// Changes the turret sprite after a delay.
        /// </summary>
        private IEnumerator ChangeSprite()
        {
            yield return new WaitForSeconds(1f);
            _image.sprite = turretStatSo.Sprite;
        }
    }
}
