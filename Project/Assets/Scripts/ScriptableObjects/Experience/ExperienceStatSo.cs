using System.Collections.Generic;
using UnityEngine;

namespace Supinfo.Project.Scripts.ScriptableObjects.Experience
{
    /// <summary>
    /// ExperienceStatSo is a ScriptableObject used for storing experience level data.
    /// </summary>
    [CreateAssetMenu(menuName = "scriptableObject/stat", order = 0)]
    public class ExperienceStatSo : ScriptableObject
    {
        /// <summary>
        /// List of experience levels.
        /// </summary>
        [SerializeField] private List<float> experienceLevels;

        /// <summary>
        /// Gets the list of experience levels.
        /// </summary>
        public List<float> ExperienceLevel => experienceLevels;
    }
}