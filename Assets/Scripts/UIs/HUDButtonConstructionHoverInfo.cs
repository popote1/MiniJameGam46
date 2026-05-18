using UnityEngine;
using UnityEngine.EventSystems;

public class HUDButtonConstructionHoverInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    [SerializeField] private Building _building;
    public void OnPointerEnter(PointerEventData eventData)
    {
        StaticEvent.DoStartBuilding(_building);
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (GridMangaer.Instance.InBuildingMode) return;
        StaticEvent.DoStartBuilding(null);
        
    }
}