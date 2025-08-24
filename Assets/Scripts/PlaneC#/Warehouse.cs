using UnityEngine;
using UnityEngine.UIElements;


public class Warehouse : WorkingBuilding {

    [SerializeField] private int _foodStockAdded = 20;
    [SerializeField] private int _woodStockAdded = 30;

    

    //public override void OnRemove() {
    //    StaticData.ChangeFoodStockValue(-_foodStockAdded);
    //    StaticData.ChangeWoodStockValue(-_woodStockAdded);
    //    base.OnRemove();
    //}

    public override void AddCitizenToWork(Citizen citizen) {
        StaticData.ChangeFoodStockValue(_foodStockAdded);
        StaticData.ChangeWoodStockValue(_woodStockAdded);
        base.AddCitizenToWork(citizen);
    }

    public override void RemoveCitizenToWork(Citizen citizen) {
        int foodDiff = (StaticData.FoodStock - _foodStockAdded) - StaticData.CurrentFood;
        if (foodDiff < 0) 
        {
            StaticData.ChangeFoodValue(foodDiff);
        }
        int woodDiff = (StaticData.WoodStock - _woodStockAdded) - StaticData.CurrentWood;
        if (woodDiff < 0)
        {
            StaticData.ChangeFoodValue(woodDiff);
        }

        StaticData.ChangeFoodStockValue(-_foodStockAdded);
        StaticData.ChangeWoodStockValue(-_woodStockAdded);
        base.AddCitizenToWork(citizen);
    }
}