using System;
using Supinfo.Project.Common;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Supinfo.Project.UI.Scripts
{
    /// <summary>
    /// This enum represents the different IA choices available to the player.
    /// </summary>
    public enum IAChoice
    {
        /// <summary>
        /// The IA will make random decisions.
        /// </summary>
        Random,

        /// <summary>
        /// The IA will be easy to beat.
        /// </summary>
        Easy,

        /// <summary>
        /// The IA will have a moderate difficulty.
        /// </summary>
        Medium,

        /// <summary>
        /// The IA will be hard to beat.
        /// </summary>
        Hard
    }

    /// <summary>
    /// This class manages the player's IA choice and sets the corresponding IA behavior in the game.
    /// </summary>
    public class IAManager : MonoBehaviour
    {
        /// <summary>
        /// The BrainWithDelay component for the random IA behavior.
        /// </summary>
        [SerializeField] private BrainWithDelay randomIA;

        /// <summary>
        /// The BrainWithDelay component for the easy IA behavior.
        /// </summary>
        [SerializeField] private BrainWithDelay easyIA;

        /// <summary>
        /// The BrainWithDelay component for the medium IA behavior.
        /// </summary>
        [SerializeField] private BrainWithDelay mediumIA;

        /// <summary>
        /// The BrainWithDelay component for the hard IA behavior.
        /// </summary>
        [SerializeField] private BrainWithDelay hardIA;

        /// <summary>
        /// The currently selected IA behavior.
        /// </summary>
        private BrainWithDelay _iaChoice;

        /// <summary>
        /// This method is called when the script instance is being loaded.
        /// It subscribes to the scene loaded event and sets the object to not be destroyed on load.
        /// </summary>
        private void Start()
        {
            // Subscribe to the scene loaded event
            SceneManager.sceneLoaded += OnSceneLoaded;

            // Set the object to not be destroyed on load
            DontDestroyOnLoad(this);
        }

        /// <summary>
        /// This method is called when the player makes an IA choice.
        /// It sets the corresponding IA behavior and loads the game scene.
        /// </summary>
        /// <param name="sender">The component that sent the event.</param>
        /// <param name="data">The data associated with the event.</param>
        public void OnIAChoice(Component sender, object data)
        {
            // If the data is not an IAChoice, return
            if (data is not IAChoice iaChoice) return;

            // Set the corresponding IA behavior
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

            // Load the game scene
            SceneManager.LoadScene(1);
        }

        /// <summary>
        /// This method is called when a scene is loaded.
        /// It sets the IA behavior in the game.
        /// </summary>
        /// <param name="scene">The scene that was loaded.</param>
        /// <param name="mode">The 	extbf{LoadSceneMode} used to load the scene.</param>
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            // Log a message to the console
            Debug.Log("Scene loaded");

            // Find the game object with the "IA" tag
            var ia = GameObject.FindGameObjectWithTag("IA");

            // Try to get the ThinkerWithDelay component from the game object
            ia.TryGetComponent(out ThinkerWithDelay thinker);

            // Log a message to the console
            Debug.Log("thinker found");

            // Set the IA behavior in the game
            thinker.Brain = _iaChoice;
        }
    }
}
