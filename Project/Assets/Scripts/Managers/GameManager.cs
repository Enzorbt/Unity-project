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
        public void OnGameSpeedChange(Component sender, object data)
        {
            if (data is not GameSpeed gameSpeed) return;
            Debug.Log(gameSpeed);
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