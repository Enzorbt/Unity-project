using System;
using Common;
using ScriptableObjects.Turret;
using UnityEngine;
using UnityEngine.Serialization;

namespace Supinfo.Project.Turret.Scripts
{
    public class TurretThinker : Thinker
    {
        [FormerlySerializedAs("turretAttackSo")] [SerializeField] private TurretStatSo turretStatSo;
        public TurretStatSo TurretStatSo => turretStatSo;

        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        public void onAgeUpgrade(Component sender, object data)
        {
            // update the sprite (stats are always drone from the Turret Stats So)
            _spriteRenderer.sprite = TurretStatSo.Sprite;
        }
    }
}