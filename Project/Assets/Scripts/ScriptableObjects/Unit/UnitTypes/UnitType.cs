using System;
using UnityEngine;

namespace Supinfo.Project.Scripts.ScriptableObjects.UnitTypes
{
    [CreateAssetMenu(fileName = "UnitType", menuName = "ScriptableObject/Units/UnitType", order = 2)]
    [Serializable]
    public class UnitType : ScriptableObject
    {
        [Header("UnitType")] 
        [SerializeField] private string _name;
        
        [Header("UnitTypes it is strong against")] 
        [SerializeField] private UnitType[] _strongAgainst;
        public UnitType[] StrongAgainst => _strongAgainst;


    }
}