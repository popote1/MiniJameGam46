using UnityEngine;
using UnityEngine.UIElements;

public class Warehouse :WorkingBuilding {
    [SerializeField] private int _foodStockAdded = 20;
    [SerializeField] private int _woodStockAdded = 30;

    

    protected override void OnDestroy() {
        StaticData.ChangeFoodStockValue(-_foodStockAdded);
        StaticData.ChangeWoodStockValue(-_woodStockAdded);
        base.OnDestroy();
    }

    public override void AddCitizenToWork(Citizen citizen) {
        StaticData.ChangeFoodStockValue(_foodStockAdded);
        StaticData.ChangeWoodStockValue(_woodStockAdded);
        base.AddCitizenToWork(citizen);
    }

    public override void RemoveCitizenToWork(Citizen citizen) {
        StaticData.ChangeFoodStockValue(-_foodStockAdded);
        StaticData.ChangeWoodStockValue(-_woodStockAdded);
        base.AddCitizenToWork(citizen);
    }
}