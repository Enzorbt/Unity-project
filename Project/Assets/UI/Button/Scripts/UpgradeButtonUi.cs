using System;
using System.Collections.Generic;
using Supinfo.Project.Scripts;
using Supinfo.Project.Scripts.Events;
using Supinfo.Project.Scripts.Managers;
using Supinfo.Project.Scripts.ScriptableObjects.Upgrades;
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

        [SerializeField] private UpgradePricesSo upgradePricesSo;

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
            
            _goldCount -= upgradePricesSo.GetPrice(upgradeType, _count);
            onGoldChange.Raise(this, - upgradePricesSo.GetPrice(upgradeType, _count));
            
            medals[_count].gameObject.SetActive(true);
            _count++;
            
            if (_count >= 2)
            {
                SetActiveButton(false);
            }
            
            // activate the button
            SetActiveButton(_goldCount >= upgradePricesSo.GetPrice(upgradeType, _count));
        }

        public void OnGoldCountChange(Component sender, object data)
        {
            if (data is not float goldCount) return;
            
            _goldCount = goldCount;
            
            // do nothing if all upgrades bought
            if (_count >= 2) return;
            
            // activate the button
            SetActiveButton(_goldCount >= upgradePricesSo.GetPrice(upgradeType, _count));
        }

        private void SetActiveButton(bool state)
        {
            gameObject.GetComponentInChildren<UnityEngine.UI.Button>().enabled = state;
        }
        
        public void OnGameSpeedChange(Component sender, object data)
        {
            if (data is not GameSpeed gameSpeed) return;
            
            SetActiveButton(gameSpeed == GameSpeed.Stop ? false : _goldCount >= upgradePricesSo.GetPrice(upgradeType, _count) && _count < 2);
        }

    }
}