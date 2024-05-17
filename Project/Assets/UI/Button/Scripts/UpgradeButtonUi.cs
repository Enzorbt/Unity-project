using System;
using System.Collections.Generic;
using Supinfo.Project.Scripts;
using Supinfo.Project.Scripts.Events;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace Supinfo.Project.UI.Button.Scripts
{
    public class UpgradeButtonUi : MonoBehaviour
    {
        private int _count;
        
        [SerializeField]
        private List<Transform> medals;
        
        [SerializeField]
        private GameEvent onUpgrade;

        [SerializeField]
        private UpgradeType upgradeType;
            
        
        public void OnClick()
        {
            onUpgrade?.Raise(this, upgradeType);
            medals[_count].gameObject.SetActive(true);
            _count++;
            
            
            if (_count == 3)
            {
                gameObject.GetComponentInChildren<UnityEngine.UI.Button>().enabled = false;
            }
        }
        
    }
}