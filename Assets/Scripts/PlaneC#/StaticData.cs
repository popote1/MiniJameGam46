using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class StaticData {

    //-------------------------Constants------------------------------------//
    public const int MAXSTOCKVALUE = 20000;
    public const int SICKTHREASHOLD = 400;
    public const int DEADTHREASHOLD = 1000;
    // threshHold in 100%
    public const float THRESHHOLDSICKTOLOSE = 80;
    public const float THRESHHOLDEADTOLOSE = 20;
    
    public const float SICKNESSPREDFRACTION = 0.5f;
    public const float MERCHANTSICKNESCHANCE = 0.4f; 
    

    public static string[] CitizenNames = new[] {
        "Anskar", "Adalgrimus", "Sigebert", "Asselin", "Eberulf", "Notker", "Feroardus", "Suidger", "Rigunth", "Hubert",
        "Wolbodo", "Meginhard", "Willehad", "Grifo", "Ludger", "Remi", "Jocelin", "Ragenardus", "Gouzlim", "Nibelung",
        "Berthaire", "Griffon", "Trutgaudus", "Chlotar", "Pancras", "Remi", "Magneric", "Munderic", "Erard",
        "Angilbert", "Heletradana", "Fordola", "Rosamund", "Gerlent", "Hildelana", "Moschia", "Folcrada", "Folsuuendis",
        "Auina", "Teudsindis", "Heleuuidis", "Hildeberga", "Errictruda", "Magnatrude", "Radlia", "Bertruda", "Frouuin",
        "Geruuara", "Gerlinda", "Erchembrog", "Gundobad", "Griffon", "Samson", "Trutgaudus", "Fridolin", "Enurchus",
        "Ansovald", "Walaric", "Gereon", "Richomeres"
    };
    
    //-----------------------Enums------------------------------------//
    
    public enum Saison {
        NoWinter, Winter
    }

    public enum GameStat {
        Stop, Playing, Paused
    }

    public enum MerchantStat {
        FoodToGold, WoodToGold, GoldToFood, GoldToWood, DontTrade
    }
    
   //--------------------------StaticData--------------------------------//
    private static Saison _currentSaison;
    private static GameStat _currentgameStat;
    private static int _currentWood;
    private static int _currentFood;
    private static int _currentGold;

    public static int _woodStock=10;
    public static int _foodStock=10;

    public static float TaxesProgress;
    public static float SaisonProgress;

    private static List<Citizen> _citizens = new List<Citizen>();
    private static List<WorkingBuilding> _workingBuildings = new List<WorkingBuilding>();

    public static GameStat CurrentGameStat => _currentgameStat; 
    public static int Gold => _currentGold;
    public static int CurrentWood => _currentWood;
    public static int CurrentFood=> _currentFood;
    public static int WoodStock=>_woodStock;
    public static int FoodStock=>_foodStock;
    public static Saison CurrentSaison => _currentSaison;
    
    public static void ChangeFoodValue(int value)=> _currentFood = Mathf.Clamp(_currentFood + value,0,FoodStock);
    public static void ChangeWoodValue(int value) => _currentWood = Mathf.Clamp(_currentWood + value,0,WoodStock);
    public static void ChangeGoldValue(int value) => _currentGold = Mathf.Max(_currentGold + value,0);
    public static void ChangeWoodStockValue(int value) {
        _woodStock = Mathf.Clamp(_woodStock + value,0,MAXSTOCKVALUE);
        _currentWood = Mathf.Clamp(_currentWood ,0,_woodStock);
    }
    public static void ChangeFoodStockValue(int value) {
        _foodStock = Mathf.Clamp(_foodStock + value,0,MAXSTOCKVALUE);
        _currentFood = Mathf.Clamp(_currentFood ,0,_foodStock);
    }
    
    public static void ChangeSaison(Saison saison) {
        _currentSaison = saison;
        StaticEvent.DoSaisonChange( _currentSaison);
    }

    public static void ChangerGameStat(GameStat stat) {
        _currentgameStat = stat;
        StaticEvent.DoChangeGameStat(stat);
    }

    public static int GetCitizenCount { get => _citizens.Count; }
    public static void AddCitizen(Citizen citizen) => _citizens.Add(citizen);
    public static void RemoveCitizen(Citizen citizen) => _citizens.Remove(citizen);
    public static void AddWorkingBuilding(WorkingBuilding building) => _workingBuildings.Add(building);
    public static void RemoveWorkingBuilding(WorkingBuilding building) => _workingBuildings.Remove(building);

    public static List<Citizen> GetSickCitizen() {
        List<Citizen> sickCitizens = new List<Citizen>();
        foreach (var citizen in _citizens) {
            if( citizen.Stat== Citizen.CitizenStat.Sick) sickCitizens.Add(citizen);
        }
        return sickCitizens;
    }
    public static List<Citizen> GetDeadCitizen() {
        List<Citizen> DeadCitizens = new List<Citizen>();
        foreach (var citizen in _citizens) {
            if( citizen.Stat== Citizen.CitizenStat.Dead) DeadCitizens.Add(citizen);
        }
        return DeadCitizens;
    }
    public static List<Citizen> GetCurringCitizen() {
        List<Citizen> curringCitizens = new List<Citizen>();
        foreach (var citizen in _citizens) {
            if( citizen.Stat== Citizen.CitizenStat.Curring) curringCitizens.Add(citizen);
        }
        return curringCitizens;
    }
    public static List<WorkingBuilding> GetWorkingBuildingsLookingForWorkers() {
        List<WorkingBuilding> buildings = new List<WorkingBuilding>();
        foreach (var workingBuilding in _workingBuildings) {
            if( workingBuilding.IsLookingForWorker)buildings.Add(workingBuilding);
        }
        return buildings;
    }
    
    
    public static void ClearAllData() {
        _currentFood = 0;
        _currentWood = 0;
        _foodStock = 10;
        _woodStock = 10;
        _currentSaison = Saison.NoWinter;
        _currentgameStat = GameStat.Paused;
        _citizens.Clear();
        for ( int i = _workingBuildings.Count-1; i >= 0; i--)
        {
            _workingBuildings[i].OnRemove();
        }
        _workingBuildings.Clear();
    }

    public static string GetRandomName() {
        return CitizenNames[Random.Range(0, CitizenNames.Length)];
    }
}