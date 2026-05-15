using UnityEngine.Tilemaps;
using UnityEngine;

[System.Serializable]
public class Building {
    public string name;
    public TileBase tile;
    public int woodCost;
    public int goldCost;
    public bool canBuild;
    public bool canBuildAbove;
    public bool requiresWater;
    public Cell.TileType type;

    public void SetUpCell(Cell cell) {
        cell.type = type;
        cell.canBuildAbove = canBuildAbove;
        Debug.Log("Building Set  ="+ type);
        switch (cell.type) {
            case Cell.TileType.Warehouse: cell.currentBuilding = new Warehouse(cell); break;
            case Cell.TileType.Farm: cell.currentBuilding = new Farme(cell); break;
            case Cell.TileType.Sawmill: cell.currentBuilding = new Sawmill(cell); break;
            case Cell.TileType.Church: cell.currentBuilding = new Church(cell); break;
            case Cell.TileType.MerchantDock: cell.currentBuilding = new MerchantDocks(cell); break;
            case Cell.TileType.FishDocks: cell.currentBuilding = new FishDocks(cell); break;
            case Cell.TileType.Infirmary: cell.currentBuilding = new Infirmary(cell); break;
            case Cell.TileType.BigHouse: cell.currentHouse = new House ( cell, 3,4 ); break;
            case Cell.TileType.LittleHouse: cell.currentHouse = new House(cell); break;
            default: break;
        }
    }
}
