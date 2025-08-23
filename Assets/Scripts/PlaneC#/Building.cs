using UnityEngine.Tilemaps;
using UnityEngine;

[System.Serializable]
public class Building
{
    public string name;
    public TileBase tile;
    public int woodCost;
    public int foodCost;
    public int goldCost;
    public bool canBuild;
    public bool canBuildAbove;
    public bool requiresWater;
    public Cell.TileType type;
}
