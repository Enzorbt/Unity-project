using System;
using System.Collections.Generic;
using Supinfo.Project.Scripts;
using Supinfo.Project.Scripts.Events;
using Supinfo.Project.Scripts.Managers;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Supinfo.Project.UI.Button.Scripts
{
    public class UpgradeButtonUi : MonoBehaviour
    {
        // the variable that keeps track of how many upgrades were bought
        private int _count;
        
        [SerializeField]
        private List<Transform> medals;
        
        [SerializeField]
        private GameEvent onUpgrade;
        
        [SerializeField]
        private GameEvent onGoldChange;

        [SerializeField]
        private UpgradeType upgradeType;

        [SerializeField] [Header("3 upgrade costs")]
        private List<float> upgradeCosts;

        private float _goldCount;

        private void Awake()
        {
            // initialize button to be deactivated
            SetActiveButton(false);
        }

        public void OnClick()
        {
            // raise the upgrade event and the gold change event (to update the gold count)
            if (onUpgrade is null) return;
            
            onUpgrade.Raise(this, upgradeType);

            if (onGoldChange is null) return;
            
            onGoldChange.Raise(this, - upgradeCosts[_count]);
            
            medals[_count].gameObject.SetActive(true);
            _count++;
            
            if (_count >= 3)
            {
                SetActiveButton(false);
            }
        }

        public void OnGoldCountChange(Component sender, object data)
        {
            if (data is not float goldCount) return;
            
            _goldCount = goldCount;
            
            // do nothing if all upgrades bought
            if (_count >= 3) return;
            
            // activate the button
            SetActiveButton(_goldCount >= upgradeCosts[_count]);
        }

        private void SetActiveButton(bool state)
        {
            gameObject.GetComponentInChildren<UnityEngine.UI.Button>().enabled = state;
        }
        
        public void OnGameSpeedChange(Component sender, object data)
        {
            if (data is not GameSpeed gameSpeed) return;
            
            SetActiveButton(gameSpeed == GameSpeed.Stop ? false : _goldCount >= upgradeCosts[_count] && _count < 3);
        }

    }
}