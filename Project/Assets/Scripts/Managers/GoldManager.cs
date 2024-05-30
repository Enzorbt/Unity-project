using System;
using System.Collections;
using Supinfo.Project.Scripts.Events;
using UnityEngine;

namespace Supinfo.Project.Scripts.Managers
{
    public class GoldManager : MonoBehaviour
    {
        [SerializeField] private GameEvent onGoldCountChange;

        private float _gold = 2500;
        
        [SerializeField] private float goldGain;
        [SerializeField] private float goldGainCooldown;

        private bool _isGainingGold;

        private void Start()
        {
            onGoldCountChange.Raise(this, _gold);
        }

        private void Update()
        {
            if (!_isGainingGold)
            {
                StartCoroutine(GainGold());
            }
        }

        private IEnumerator GainGold()
        {
            _isGainingGold = true;
            _gold += goldGain;
            onGoldCountChange.Raise(this, _gold);
            yield return new WaitForSeconds(goldGainCooldown);
            _isGainingGold = false;

        }

        public void OnGoldChange(Component sender, object data)
        {
            if (data is not float gold) return;
            
            // add gold to total
            _gold += gold;
            
            onGoldCountChange.Raise(this, _gold);
        }
    }
}