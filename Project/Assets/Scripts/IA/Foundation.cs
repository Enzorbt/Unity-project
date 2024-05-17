using Interfaces.IA;
using ScriptableObjects.Unit;
using Supinfo.Project.Scripts;
using Supinfo.Project.Scripts.Events;
using UnityEngine;

namespace Supinfo.Project.IA
{
    public class Foundation : MonoBehaviour, IFoundation
    {
        [SerializeField] private GameEvent onLaunchCapacity;
        
        [SerializeField] private GameEvent onSpawnUnit;
        

        public void SpawnUnit(UnitStatSo unitStatSo)
        {
            onSpawnUnit.Raise(this, unitStatSo);
            throw new System.NotImplementedException();
        }

        public void UseCapacity()
        {
            throw new System.NotImplementedException();
        }

        public void Upgrade(UpgradeType upgradeType)
        {
            throw new System.NotImplementedException();
        }

        public void SpawnTurret()
        {
            throw new System.NotImplementedException();
        }

        public void BuyTurret(int pTurretID)
        {
            throw new System.NotImplementedException();
        }
    }
}