using Supinfo.Project.Scripts.Events;
using Supinfo.Project.Scripts.ScriptableObjects.Experience;
using UnityEngine;

namespace Supinfo.Project.Scripts.Managers
{
    /// <summary>
    /// Manages experience points (XP) and evolution in the game.
    /// </summary>
    public class ExpManager : MonoBehaviour
    {
        /// <summary>
        /// Event triggered when evolution is possible.
        /// </summary>
        [SerializeField] private GameEvent onCanEvolve;

        /// <summary>
        /// Event triggered when the experience ratio changes.
        /// </summary>
        [SerializeField] private GameEvent onExpRatioChange;
        
        /// <summary>
        /// Event triggered when the experience count changes.
        /// </summary>
        [SerializeField] private GameEvent onExpCountChange;

        /// <summary>
        /// ScriptableObject holding experience levels.
        /// </summary>
        [SerializeField] private ExperienceStatSo experienceStatSo; 

        /// <summary>
        /// Event triggered when the maximum XP changes.
        /// </summary>
        [SerializeField] private GameEvent onXpMaxChange;
        
        /// <summary>
        /// // Current age or level of the player.
        /// </summary>
        private int _age;

        /// <summary>
        /// Current experience points.
        /// </summary>
        private float _expCount;
        
        /// <summary>
        /// Maximum experience points for the current age.
        /// </summary>
        private float _expMax; 
        
        private void Awake()
        {
            _age = 0;  // Initialize age to 0.
            _expCount = 1000;  // Initialize experience count.
            _expMax = experienceStatSo.ExperienceLevel[_age];  // Set the maximum experience for the current age.
        }
        
        private void Start()
        {
            onXpMaxChange.Raise(this, _expMax);  // Raise event for XP max change.
            onExpCountChange.Raise(this, _expCount);  // Raise event for XP count change.
            onExpRatioChange.Raise(this, _expCount / _expMax);  // Raise event for XP ratio change.
        }

        /// <summary>
        /// Update is called once per frame.
        /// </summary>
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                ReceiveExp(this, 500f);  // Grant 500 XP when the 'X' key is pressed.
            }
        }

        /// <summary>
        /// Listener for age upgrade event.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="data">Event data.</param>
        public void UpgradeAge(Component sender, object data)
        {
            _expCount -= experienceStatSo.ExperienceLevel[_age];  // Subtract current level's XP from total XP.
            
            _age++;  // Increment age.
            
            if (_age <= experienceStatSo.ExperienceLevel.Count)
            {
                _expMax = experienceStatSo.ExperienceLevel[_age];  // Set new maximum XP for the new age.
            }
            
            // Raise events for XP changes
            onXpMaxChange.Raise(this, _expMax);
            onExpRatioChange.Raise(this, _expCount / _expMax);
        }
        
        /// <summary>
        /// Listener for experience gain event.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="data">Event data.</param>
        public void ReceiveExp(Component sender, object data)
        {
            // Do nothing if no more ages
            if (_age >= experienceStatSo.ExperienceLevel.Count) return;
            
            // Raise Can Evolve event (for evolution button)
            if (data is not float expGain) return;
            
            _expCount += expGain;  // Add gained experience to total XP.
            
            // Raise events for XP changes
            onExpRatioChange.Raise(this, _expCount / _expMax);
            onExpCountChange.Raise(this, _expCount);
            onCanEvolve.Raise(this, _expCount >= _expMax);
        }
    }
}
