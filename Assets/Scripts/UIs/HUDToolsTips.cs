using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class HUDToolsTips : MonoBehaviour
{
    [SerializeField] private HUDHouseToolTips _panelHouse;
    [SerializeField] private HUDWorkingPlaceToolTips _panelWorkingBuilding;
    private void Start() {
        StaticEvent.OnHoverCell+= StaticEventOnOnHoverCell;
    }

    private void OnDestroy() {
        StaticEvent.OnHoverCell-= StaticEventOnOnHoverCell;
    }

    private void StaticEventOnOnHoverCell(object sender, Cell e)
    {
        if (e == null) {
            _panelHouse.HidePanel();
            _panelWorkingBuilding.HidePanel();
            return;
        }

        if (e.type == Cell.TileType.LittleHouse || e.type == Cell.TileType.BigHouse) {
            _panelHouse.DisplayHouseInfo(e.currentHouse);
            _panelWorkingBuilding.HidePanel();
            return;
        }

        if (e.type == Cell.TileType.Church
            || e.type == Cell.TileType.Farm
            || e.type == Cell.TileType.Infirmary
            || e.type == Cell.TileType.Sawmill
            || e.type == Cell.TileType.Warehouse
            || e.type == Cell.TileType.FishDocks
            || e.type == Cell.TileType.MerchantDock)
        {
            _panelWorkingBuilding.DisplayHouseInfo(e.currentBuilding);
            _panelHouse.HidePanel();
            return;
        }
        _panelHouse.HidePanel();
        _panelWorkingBuilding.HidePanel();
    }

    // Update is called once per frame
    void Update()
    {
        if (_panelHouse.gameObject.activeSelf) _panelHouse.transform.position = Mouse.current.position.value;
        if (_panelWorkingBuilding.gameObject.activeSelf) _panelWorkingBuilding.transform.position = Mouse.current.position.value;
    }
}