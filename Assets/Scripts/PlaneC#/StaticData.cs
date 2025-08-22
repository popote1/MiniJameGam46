using UnityEngine;

public static class StaticData {

    public const int MAXSTOCKVALUE = 20000; 
    
    public static int CurrentWood;
    public static int CurrentFood;

    public static int WoodStock=10;
    public static int FoodStock=10;

    public static void ChangeFoodValue(int value)=> CurrentFood = Mathf.Clamp(CurrentFood + value,0,FoodStock);
    public static void ChangeWoodValue(int value) => CurrentWood = Mathf.Clamp(CurrentWood + value,0,WoodStock);

    public static void ChangeWoodStockValue(int value) {
        WoodStock = Mathf.Clamp(WoodStock + value,0,MAXSTOCKVALUE);
        CurrentWood = Mathf.Clamp(CurrentWood ,0,WoodStock);
    }
    public static void ChangeFoodStockValue(int value) {
        FoodStock = Mathf.Clamp(FoodStock + value,0,MAXSTOCKVALUE);
        CurrentFood = Mathf.Clamp(CurrentFood ,0,FoodStock);
    }
    
}
