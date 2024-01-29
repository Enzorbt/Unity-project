using Supinfo.Project.ScriptableObjects.Common;
using Supinfo.Project.Scripts.Common.Stats;
using UnityEngine;

namespace Supinfo.Project.ScriptableObjects.Unit
{
    [CreateAssetMenu(menuName = "ScriptableObject/Units/UnitMovementSo")]
    public class UnitMovementSo : MovementSo
    {
        private void OnEnable()
        {
            currentAge = 0;
        }
    }
    
    
}