using UnityEngine;

public class Cell
{
    public enum TileType {
        Air, // can't build and can't build above
        Ground, // can't build
        LittleHouse, // can't build above
        BigHouse,
        Sawmill,
        Farm, // can't build above 
        Warehouse,
        MerchantDock, // can't build above
        Infirmary, //can't build above
        FishDocks, // can't build above or away from water
        Church, // can't build and can't build above
        PlaceableSquare // can't build and can't build
    }

    public Vector3Int position = new Vector3Int(0, 0 );
    public TileType type = TileType.Air;
    public bool canBuildAbove = true;
    public WorkingBuilding currentBuilding;
    public House currentHouse;
    public GridMangaer gridManager;


}
