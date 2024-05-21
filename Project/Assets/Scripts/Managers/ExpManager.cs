using System;
using Supinfo.Project.Scripts.Events;
using Supinfo.Project.Scripts.ScriptableObjects.Experience;
using UnityEngine;

namespace Supinfo.Project.Scripts.Managers
{
    public class ExpManager : MonoBehaviour
    {
        [SerializeField]
        private GameEvent onCanEvolve;

        [SerializeField]
        private GameEvent onExpRatioChange;
        
        [SerializeField]
        private GameEvent onExpCountChange;

        [SerializeField]
        private ExperienceStatSo experienceStatSo;

        [SerializeField]
        private GameEvent onXpMaxChange; 
        
        private int _age;

        private float _expCount;

        private void Awake()
        {
            _age = 0;
            _expCount = 0;
        }

        private void Start()
        {
            onXpMaxChange.Raise(this, experienceStatSo.ExperienceLevel[_age]);
        }
        
        // listener age upgrade
        public void UpgradeAge(Component sender, object data)
        {
            _age++;
            _expCount = 0;
            
            // raise exp change for exp bar and capacity buttons
            onExpRatioChange.Raise(this, _expCount / experienceStatSo.ExperienceLevel[_age]);
            onXpMaxChange.Raise(this, experienceStatSo.ExperienceLevel[_age]);
        }
        
        // listener exp recovery
        public void ReceiveExp(Component sender, object data)
        {
            // do nothing if no more ages
            if (_age >= experienceStatSo.ExperienceLevel.Count) return;
            
            // raise Can evolve event (for evolution button)
            if (data is not float expGain) return;
            if (_expCount < experienceStatSo.ExperienceLevel[_age] || expGain < 0)
            {
                _expCount += expGain;
                
            }
            if (_expCount >= experienceStatSo.ExperienceLevel[_age])
            {
                _expCount = experienceStatSo.ExperienceLevel[_age];
            }
            // raise exp change for exp bar and capacity buttons
            onExpRatioChange.Raise(this, _expCount / experienceStatSo.ExperienceLevel[_age]);
            onExpCountChange.Raise(this, _expCount);
            onCanEvolve.Raise(this, _expCount >= experienceStatSo.ExperienceLevel[_age]);
        }
    }
}