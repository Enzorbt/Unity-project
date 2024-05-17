using System.Collections.Generic;
using UnityEngine;

namespace Supinfo.Project.Scripts.ScriptableObjects.Experience
{
    [CreateAssetMenu(menuName = "scriptableObject/stat", order = 0)]
    public class ExperienceStatSo : ScriptableObject
    {
        [SerializeField] private List<float> experienceLevels;
        public List<float> ExperienceLevel => experienceLevels;
    }
}