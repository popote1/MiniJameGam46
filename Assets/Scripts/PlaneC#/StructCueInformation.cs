using UnityEngine;

public struct StructCueInformation
{
    public Vector2 TargetPosition;
    public CueType Type;
    public Cell.TileType CellType;

    public enum CueType {
        Building, Destroy, Sick, Dead, Cure, ProdFram, ProdFish, ProdWoof, Gold, Merchant
    }

    public StructCueInformation(Vector2 pos, CueType type, Cell.TileType cellType) {
        TargetPosition = pos;
        Type = type;
        CellType = cellType;
    }
}
