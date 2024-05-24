

using System;
using Supinfo.Project.Scripts.Managers;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

namespace Supinfo.Project.UI.Scripts
{
    public class EndGameManager : MonoBehaviour
    {
        [SerializeField] private GameObject winPanel;
        private TextMeshProUGUI _text;
        private Image _image;

        private void Awake()
        {
            _text = winPanel.GetComponentInChildren<TextMeshProUGUI>();
            _image = winPanel.GetComponentInChildren<Image>();
        }

        public void OnCastleDeath(Component sender, object data)
        {
            if (data is not BaseIdentifier baseId) return;
            winPanel.SetActive(true);
            switch (baseId)
            {
                case BaseIdentifier.BaseEnemies:
                    EnableWinPanel(Color.white, "Victory");
                    break;
                case BaseIdentifier.BaseAllies:
                    EnableWinPanel(Color.red, "Defeat");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void EnableWinPanel(Color color, String title)
        {
            _text.text = title;
            _image.color = color;
        }
    }
}