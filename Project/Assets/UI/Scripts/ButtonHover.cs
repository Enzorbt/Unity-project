using UnityEngine;
using UnityEngine.EventSystems;

namespace Supinfo.Project.UI.Scripts
{
    public class ButtonHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public GameObject hoverImage;  
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            hoverImage.SetActive(true);
        }
        
        public void OnPointerExit(PointerEventData eventData)
        {
            hoverImage.SetActive(false);
        }
    }
}