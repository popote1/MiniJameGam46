using UnityEngine;

public class Cell
{
    public enum TileType {
        Air, // can't build above
        Ground,
        LittleHouse, // can't build above
        BigHouse,
        Sawmill,
        Farm, // can't build above 
        Warehouse,
        MerchantDock, // can't build above
        Infirmary, //can't build
        FishDocks, // can't build above, can't build inland
        Church // can't build
    }

    public int[] position = { 0, 0 };
    public TileType type = TileType.Air;
    public bool canBuildAbove = true;
    public bool canBuildInland = true;
}
