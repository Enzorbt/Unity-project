using System.Globalization;
using Supinfo.Project.Scripts.Events;
using TMPro;
using UnityEngine;

namespace Supinfo.Project.UI.Scripts
{
    public class GoldTextUi : MonoBehaviour
    {
        
        [SerializeField]
        private GameEvent onGoldChange;
        
        public void OnGoldChange(Component sender, object data)
        {
            
            if (data is not float gold) return;
            transform.TryGetComponent(out TextMeshProUGUI textMeshPro);
            if (textMeshPro is null) return;
            textMeshPro.text = gold.ToString(CultureInfo.CurrentCulture);
        }
    }
}