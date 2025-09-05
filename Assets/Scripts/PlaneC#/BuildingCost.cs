using System;

[Serializable]
public struct BuildingCost {
    public Cell.TileType _type;
    public int WoodCost;
    public int GoldCost;

    public bool CanBeBuild() {
        if (StaticData.CurrentWood < WoodCost) return false;
        if (StaticData.Gold < GoldCost) return false;
        return true;
    }
}
