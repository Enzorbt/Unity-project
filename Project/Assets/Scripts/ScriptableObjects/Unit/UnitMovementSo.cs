using Supinfo.Project.Scripts.Common.Stats;
using UnityEngine;

namespace Supinfo.Project.ScriptableObjects.Unit
{
    [CreateAssetMenu(menuName = "ScriptableObject/Units/UnitMovementSo")]
    public class UnitMovementSo : ScriptableObject
    {
        /// <summary>
        /// Walking speed of the unit.
        /// </summary>
        [SerializeField] private Stat walkSpeed;
        public Stat WalkSpeed => walkSpeed; 
    }
    
    
}