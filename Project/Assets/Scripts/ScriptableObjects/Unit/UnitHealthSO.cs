using System;
using ScriptableObjects.Common;
using Supinfo.Project.Scripts.Stats;
using UnityEngine;

namespace ScriptableObjects.Unit
{
    [CreateAssetMenu(menuName = "ScriptableObject/Units/UnitHealthSO")]
    [Serializable]
    public class UnitHealthSO : HealthSO
    {
        //--------- Rewards upon killing ---------//
        [Header("Rewards upon killing")]
        
        [SerializeField] private Stat goldGiven;
        public Stat GoldGiven => goldGiven;
        
        [SerializeField] private Stat experienceGiven;
        public Stat ExperienceGiven => experienceGiven;
    }
}