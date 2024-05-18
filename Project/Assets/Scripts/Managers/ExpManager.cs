using Supinfo.Project.Scripts.Events;
using Supinfo.Project.Scripts.ScriptableObjects.Experience;
using UnityEngine;

namespace Supinfo.Project.Scripts.Managers
{
    public class ExpManager : MonoBehaviour
    {
        [SerializeField] private GameEvent onCanEvolve;

        [SerializeField] private GameEvent onExpRatioChange;

        [SerializeField] private ExperienceStatSo experienceStatSo;
        
        private int _age;

        private float _expCount;

        private void Awake()
        {
            _age = 0;
            _expCount = 0;
        }

        // listener age upgrade
        public void UpgradeAge(Component sender, object data)
        {
            _age++;
            _expCount = 0;
            
            // raise exp change for exp bar and capacity buttons
            onExpRatioChange.Raise(this, _expCount / experienceStatSo.ExperienceLevel[_age]);
        }
        
        // listener exp recovery
        public void ReceiveExp(Component sender, object data)
        {
            // do nothing if no more ages
            if (_age >= experienceStatSo.ExperienceLevel.Count) return;
            // do nothing if data not a float
            if (data is not float expGain) return;
            _expCount += expGain;
            // raise Can evolve event (for evolution button)
            if (_expCount > experienceStatSo.ExperienceLevel[_age])
            {
                if (_age == experienceStatSo.ExperienceLevel.Count - 1) return;
                onCanEvolve.Raise(this, true);
            }

            // raise exp change for exp bar and capacity buttons
            onExpRatioChange.Raise(this, _expCount / experienceStatSo.ExperienceLevel[_age]);
        }
    }
}