using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
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
    public static int CurrentWood;
    public static int CurrentFood;

    public static int WoodStock=10;
    public static int FoodStock=10;
    
    private static List<Citizen> _citizens = new List<Citizen>();
    private static List<WorkingBuilding> _workingBuildings = new List<WorkingBuilding>();
    
    
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