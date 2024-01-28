using Supinfo.Project.ScriptableObjects.Common;
using UnityEngine;

namespace Supinfo.Project.ScriptableObjects.Base
{
    [CreateAssetMenu(menuName = "ScriptableObject/Base/BaseHealthSO")]
    public class BaseHealthSo : HealthSo
    {
        private void OnEnable()
        {
            currentAge = 0;
            currentHealthUpgrade = 0;
        }
    }
}