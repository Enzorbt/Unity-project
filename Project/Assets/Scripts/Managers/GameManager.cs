using System;
using UnityEngine;

namespace Supinfo.Project.Scripts.Managers
{
    public enum BaseIdentifier
    {
        BaseAllies,
        BaseEnemies
    }

    public enum GameSpeed
    {
        Stop,
        Slow,
        Play,
        Fast,
        Faster
    }
    
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject winPanel;

        public void OnCastleDeath(Component sender, object data)
        {
            if (data is not BaseIdentifier baseId) return;

            // display win panel
            winPanel.SetActive(true);
            
            // pause the game
            
            // get the text element to change
            
            // get the color (red/blue) element to change
            
            switch (baseId)
            {
                case BaseIdentifier.BaseAllies:
                    // change panel to match winner (color, text)
                    break;
                case BaseIdentifier.BaseEnemies:
                    // same here
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void OnGameSpeedChange(Component sender, object data)
        {
            if (data is not GameSpeed gameSpeed) return;

            Time.timeScale = gameSpeed switch
            {
                GameSpeed.Stop => 0,
                GameSpeed.Slow => 0.5f,
                GameSpeed.Play => 1,
                GameSpeed.Fast => 2,
                GameSpeed.Faster => 3,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}