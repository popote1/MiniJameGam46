using UnityEngine;
using UnityEngine.UIElements;


public class Warehouse : WorkingBuilding {

    //public Warehouse(GridMangaer gridManager)
    //{
    //    grid = gridManager;
    //}

    [SerializeField] private int _foodStockAdded = 20;
    [SerializeField] private int _woodStockAdded = 30;

    

    public override void OnRemove() {
        StaticData.ChangeFoodStockValue(-_foodStockAdded);
        StaticData.ChangeWoodStockValue(-_woodStockAdded);
        base.OnRemove();
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