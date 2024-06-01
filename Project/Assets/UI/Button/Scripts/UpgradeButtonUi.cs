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
    /// <summary>
    /// Handles the behavior of an upgrade button in the UI.
    /// </summary>
    public class UpgradeButtonUi : MonoBehaviour
    {
        /// <summary>
        /// The variable that keeps track of how many upgrades were bought.
        /// </summary>
        private int _count;
        
        /// <summary>
        /// The list of medals to be displayed upon purchasing upgrades.
        /// </summary>
        [SerializeField] private List<Transform> medals;
        
        /// <summary>
        /// Event invoked when an upgrade is purchased.
        /// </summary>
        [SerializeField] private GameEvent onUpgrade;
        
        /// <summary>
        ///  Event invoked when the gold count changes.
        /// </summary>
        [SerializeField] private GameEvent onGoldChange;

        /// <summary>
        /// The type of upgrade associated with this button.
        /// </summary>
        [SerializeField] private UpgradeType upgradeType;

        /// <summary>
        /// The ScriptableObject containing upgrade prices.
        /// </summary>
        [SerializeField] private UpgradePricesSo upgradePricesSo;

        /// <summary>
        /// The current gold count.
        /// </summary>
        private float _goldCount;

        /// <summary>
        /// Initializes the button to be deactivated.
        /// </summary>
        private void Awake()
        {
            SetActiveButton(false);
        }

        /// <summary>
        /// Called when the button is clicked.
        /// </summary>
        public void OnClick()
        {
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
            
            SetActiveButton(_goldCount >= upgradePricesSo.GetPrice(upgradeType, _count));
        }

        /// <summary>
        /// Called when the gold count changes.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="data">The new gold count.</param>
        public void OnGoldCountChange(Component sender, object data)
        {
            if (data is not float goldCount) return;
            _goldCount = goldCount;
            
            if (_count >= 2) return;
            SetActiveButton(_goldCount >= upgradePricesSo.GetPrice(upgradeType, _count));
        }

        /// <summary>
        /// Sets the button's activation state based on the game speed.
        /// </summary>
        /// <param name="sender">The sender of the event.</param>
        /// <param name="data">The game speed.</param>
        public void OnGameSpeedChange(Component sender, object data)
        {
            if (data is not GameSpeed gameSpeed) return;
            SetActiveButton(gameSpeed == GameSpeed.Stop ? false : _goldCount >= upgradePricesSo.GetPrice(upgradeType, _count) && _count < 2);
        }

        /// <summary>
        /// Sets the activation state of the button.
        /// </summary>
        /// <param name="state">The activation state.</param>
        private void SetActiveButton(bool state)
        {
            gameObject.GetComponentInChildren<UnityEngine.UI.Button>().enabled = state;
        }
    }
}
