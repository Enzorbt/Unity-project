using Supinfo.Project.Scripts.Events;
using UnityEngine;

namespace Supinfo.Project.Scripts.Managers
{
    public class GoldManager : MonoBehaviour
    {
        [SerializeField] private GameEvent onGoldCountChange;

        private int _gold;

        public void OnGoldChange(Component sender, object data)
        {
            if (data is not int gold) return;
            
            // add gold to total
            _gold += gold;
            
            onGoldCountChange.Raise(this, _gold);
        }
    }
}