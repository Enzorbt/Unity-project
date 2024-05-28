using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Supinfo.Project.Background.Scripts
{
    public class BackgroundManager : MonoBehaviour
    {
        [SerializeField] private List<Sprite> _backgrounds;
        private int _age;
        private Image _image;

        private void Awake()
        {
            _image = GetComponentInChildren<Image>();
        }

        public void OnAgeUpgrade(Component sender, object data)
        {
            _age++;
            _image.sprite = _backgrounds[_age];
        }
    }
}