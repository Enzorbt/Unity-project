using System;
using Supinfo.Project.Scripts.Events;
using Supinfo.Project.Scripts.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Supinfo.Project.UI.Button.Scripts
{
    public class GameSpeedButtonUI : MonoBehaviour
    {
        [SerializeField] private GameSpeed gameSpeed;

        [SerializeField] private GameEvent onGameSpeedChange;
        [SerializeField] private Sprite spriteOn;
        [SerializeField] private Sprite spriteOff;
        private Image _image;

        private void Awake()
        {
            _image = GetComponentInChildren<Image>();
        }

        public void OnClick()
        {
            onGameSpeedChange.Raise(this, gameSpeed);
            SetActiveButton(false);
        }

        public void OnGameSpeedChange(Component sender, object data)
        {
            if (data is not GameSpeed currentGameSpeed) return;

            SetActiveButton(gameSpeed != currentGameSpeed);
        }

        private void SetActiveButton(bool state)
        {
            gameObject.GetComponentInChildren<UnityEngine.UI.Button>().enabled = state;
            _image.sprite = state ? spriteOn : spriteOff;
        }
    }
}