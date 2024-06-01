using System.Globalization;
using Supinfo.Project.Scripts.Events;
using TMPro;
using UnityEngine;

namespace Supinfo.Project.UI.Scripts
{
    /// <summary>
    /// This class manages the display of the player's gold in the UI.
    /// </summary>
    public class GoldTextUi : MonoBehaviour
    {
        /// <summary>
        /// The GameEvent that is triggered when the player's gold changes.
        /// </summary>
        [SerializeField] private GameEvent onGoldChange;

        /// <summary>
        /// This method is called when the player's gold changes.
        /// It updates the text displaying the player's gold.
        /// </summary>
        /// <param name="sender">The component that sent the event.</param>
        /// <param name="data">The data associated with the event.</param>
        public void OnGoldChange(Component sender, object data)
        {
            // If the data is not a float, return
            if (data is not float gold) return;

            // Try to get the TextMeshProUGUI component from the transform
            transform.TryGetComponent(out TextMeshProUGUI textMeshPro);

            // If the TextMeshProUGUI component is null, return
            if (textMeshPro is null) return;

            // Update the text displaying the player's gold
            textMeshPro.text = gold.ToString(CultureInfo.CurrentCulture);
        }
    }
}
