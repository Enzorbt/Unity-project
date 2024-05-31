using System;
using Supinfo.Project.Common;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Supinfo.Project.UI.Scripts
{
    public enum IAChoice
    {
        Random,
        Easy,
        Medium,
        Hard
    }
    public class IAManager : MonoBehaviour
    {
        [SerializeField] private BrainWithDelay randomIA;
        [SerializeField] private BrainWithDelay easyIA;
        [SerializeField] private BrainWithDelay mediumIA;
        [SerializeField] private BrainWithDelay hardIA;

        private BrainWithDelay _iaChoice;

        private void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            DontDestroyOnLoad(this);
        }

        public void OnIAChoice(Component sender, object data)
        {
            if (data is not IAChoice iaChoice) return;

            switch (iaChoice)
            {
                case IAChoice.Random:
                    _iaChoice = randomIA;
                    break;
                case IAChoice.Easy:
                    _iaChoice = easyIA;
                    break;
                case IAChoice.Medium:
                    _iaChoice = mediumIA;
                    break;
                case IAChoice.Hard:
                    _iaChoice = hardIA;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            SceneManager.LoadScene(1);
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            Debug.Log("Scene loaded");
            var ia = GameObject.FindGameObjectWithTag("IA");

            ia.TryGetComponent(out ThinkerWithDelay thinker);
            Debug.Log("thinker found");

            thinker.Brain = _iaChoice;
        }
    }
}