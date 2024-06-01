using System.Collections.Generic;
using UnityEngine;

namespace Supinfo.Project.UI.Scripts
{
    public class FloorManager : MonoBehaviour
    {
        [SerializeField] private List<Sprite> sprites;
        private SpriteRenderer[] _images;
        private int _age;

        private void Awake()
        {
            _age = 0;
            _images = GetComponentsInChildren<SpriteRenderer>();
            ChangeSprites();
        }

        public void OnAgeUpgrade(Component sender, object data)
        {
            _age++;
            
            if (_age >= sprites.Count) return;
            
            ChangeSprites();
        }

        private void ChangeSprites()
        {
            foreach (var image in _images)
            {
                image.sprite = sprites[_age];
            }
        }
    }
}