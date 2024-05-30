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

        private float _expMax;

        private void Awake()
        {
            _age = 0;
            _expCount = 1000;
            _expMax = experienceStatSo.ExperienceLevel[_age];
        }

        private void Start()
        {
            onXpMaxChange.Raise(this, _expMax);
            onExpCountChange.Raise(this, _expCount);
            onExpRatioChange.Raise(this, _expCount/_expMax);
        }

        private void Update()
        {
            if ( Input.GetKeyDown(KeyCode.X))
            {
                ReceiveExp(this, 500f);
            }
        }

        // listener age upgrade
        public void UpgradeAge(Component sender, object data)
        {
            _expCount -= experienceStatSo.ExperienceLevel[_age];
            
            _age++;
            
            if (_age <= experienceStatSo.ExperienceLevel.Count)
            {
                _expMax = experienceStatSo.ExperienceLevel[_age];
            }
            
            // raise exp change for exp bar and capacity buttons
            onXpMaxChange.Raise(this, _expMax);
            onExpRatioChange.Raise(this, _expCount / _expMax);
            
        }
        
        // listener exp recovery
        public void ReceiveExp(Component sender, object data)
        {
            // do nothing if no more ages
            if (_age >= experienceStatSo.ExperienceLevel.Count) return;
            
            // raise Can evolve event (for evolution button)
            if (data is not float expGain) return;
            
            _expCount += expGain;
            
            // raise exp change for exp bar and capacity buttons
            onExpRatioChange.Raise(this, _expCount / _expMax);
            onExpCountChange.Raise(this, _expCount);
            onCanEvolve.Raise(this, _expCount >= _expMax);
        }
    }
}