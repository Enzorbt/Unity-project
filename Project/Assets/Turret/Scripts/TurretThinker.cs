using System;
using System.Collections;
using Common;
using ScriptableObjects.Turret;
using UnityEngine;
using UnityEngine.Serialization;

namespace Supinfo.Project.Turret.Scripts
{
    public class TurretThinker : Thinker
    {
        [FormerlySerializedAs("turretAttackSo")] [SerializeField] private TurretStatSo turretStatSo;
        public TurretStatSo TurretStatSo
        {
            get => turretStatSo;
            set => turretStatSo = value;
        }

        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        public void OnAgeUpgrade(Component sender, object data)
        {
            // update the sprite (stats are always drone from the Turret Stats So)
            StartCoroutine(ChangeSprite());
            
        }

        private IEnumerator ChangeSprite()
        {
            yield return new WaitForSeconds(1f);
            _spriteRenderer.sprite = TurretStatSo.Sprite;
        }
    }
}