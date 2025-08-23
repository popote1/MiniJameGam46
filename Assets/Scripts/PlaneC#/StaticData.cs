using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class StaticData {

    //-------------------------Constants------------------------------------//
    public const int MAXSTOCKVALUE = 20000;
    public const int SICKTHREASHOLD = 100;
    public const int DEADTHREASHOLD = 200;

    public static string[] CitizenNames = new[] {
        "Anskar", "Adalgrimus", "Sigebert", "Asselin", "Eberulf", "Notker", "Feroardus", "Suidger", "Rigunth", "Hubert",
        "Wolbodo", "Meginhard", "Willehad", "Grifo", "Ludger", "Remi", "Jocelin", "Ragenardus", "Gouzlim", "Nibelung",
        "Berthaire", "Griffon", "Trutgaudus", "Chlotar", "Pancras", "Remi", "Magneric", "Munderic", "Erard",
        "Angilbert", "Heletradana", "Fordola", "Rosamund", "Gerlent", "Hildelana", "Moschia", "Folcrada", "Folsuuendis",
        "Auina", "Teudsindis", "Heleuuidis", "Hildeberga", "Errictruda", "Magnatrude", "Radlia", "Bertruda", "Frouuin",
        "Geruuara", "Gerlinda", "Erchembrog", "Gundobad", "Griffon", "Samson", "Trutgaudus", "Fridolin", "Enurchus",
        "Ansovald", "Walaric", "Gereon", "Richomeres"
    };
    
    //-----------------------StaticData------------------------------------//
    private static int _currentWood;
    private static int _currentFood;
    private static int _currentGold;

    public static int _woodStock=10;
    public static int _foodStock=10;

    public static float TaxesProgress;
    
    private static List<Citizen> _citizens = new List<Citizen>();
    private static List<WorkingBuilding> _workingBuildings = new List<WorkingBuilding>();
    
    public static int Gold => _currentGold;
    public static int CurrentWood => _currentWood;
    public static int CurrentFood=> _currentFood;
    public static int WoodStock=>_woodStock;
    public static int FoodStock=>_foodStock;
    
    public static void ChangeFoodValue(int value)=> _currentFood = Mathf.Clamp(_currentFood + value,0,FoodStock);
    public static void ChangeWoodValue(int value) => _currentWood = Mathf.Clamp(_currentWood + value,0,WoodStock);
    public static void ChangeGoldValue(int value) => _currentWood = Mathf.Max(_currentWood + value,0);
    public static void ChangeWoodStockValue(int value) {
        _woodStock = Mathf.Clamp(_woodStock + value,0,MAXSTOCKVALUE);
        _currentWood = Mathf.Clamp(_currentWood ,0,_woodStock);
    }
    public static void ChangeFoodStockValue(int value) {
        _foodStock = Mathf.Clamp(_foodStock + value,0,MAXSTOCKVALUE);
        _currentFood = Mathf.Clamp(_currentFood ,0,_foodStock);
    }
    public static int GetCitizenCount { get => _citizens.Count; }
    public static void AddCitizen(Citizen citizen) => _citizens.Add(citizen);
    public static void RemoveCitizen(Citizen citizen) => _citizens.Remove(citizen);
    public static void AddWorkingBuilding(WorkingBuilding building) => _workingBuildings.Add(building);
    public static void RemoveWorkingBuilding(WorkingBuilding building) => _workingBuildings.Remove(building);
    public static List<WorkingBuilding> GetWorkingBuildingsLookingForWorkers() {
        List<WorkingBuilding> buildings = new List<WorkingBuilding>();
        foreach (var workingBuilding in _workingBuildings) {
            if( workingBuilding.IsLookingForWorker)buildings.Add(workingBuilding);
        }
        return buildings;
    }

    public static string GetRandomName() {
        return CitizenNames[Random.Range(0, CitizenNames.Length)];
    }
}