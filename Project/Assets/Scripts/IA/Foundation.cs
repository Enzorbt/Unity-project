using Interfaces.IA;
using Supinfo.Project.Scripts.Events;
using UnityEngine;
using Supinfo.Project.Interfaces.Capacity;
using Supinfo.Project.

namespace Supinfo.Project.IA
{
    public class Foundation : IFoundation
    {
        [SerializeField] private GameEvent LaunchCapacity;
        
        [SerializeField] private GameEvent ;
        
        

        public void SpawnUnit()
        {
            
            throw new System.NotImplementedException();
        }

        public void UseCapacity()
        {
            throw new System.NotImplementedException();
        }

        public void GetAmelioration(int pAmeliorationID)
        {
            throw new System.NotImplementedException();
        }

        public void BuyTurretEmplacement()
        {
            throw new System.NotImplementedException();
        }

        public void BuyTurret(int pTurretID)
        {
            throw new System.NotImplementedException();
        }
    }
}