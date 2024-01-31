using Supinfo.Project.ScriptableObjects.Common;
using UnityEngine;

namespace ScriptableObjects.Turret
{
    [CreateAssetMenu(fileName = "TurretSpawnSo", menuName = "ScriptableObject/Turrets/TurretSpawnSo")]
    public class TurretSpawnSo : SpawnSo
    {
        private void OnEnable()
        {
            currentAge = 0;
        }
    }
}