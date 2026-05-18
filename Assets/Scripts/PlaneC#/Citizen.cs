using System;
using UnityEngine;

[Serializable] 
public class Citizen {
    public enum CitizenStat {
        Fine, Sick, Curring , Dead
    }

    public string Name;
    public CitizenStat _stat;
    public bool IsMalnourish;
    
    private float _sicknessLevel;
    private WorkingBuilding _workPlace;
    private House _house;
    
    public House House { get => _house; }
    public WorkingBuilding WorkingBuilding { get => _workPlace; }
    public float GetSicknessvalue { get => _sicknessLevel; }
    public CitizenStat Stat {
        get => _stat;
    }
    
    public Citizen(House house) {
        _house = house;
        Name = StaticData.GetRandomName();
        StaticEvent.OnDoGameTick+= StaticEventOnOnDoGameTick;
    }

    public void GetCured() {
        _sicknessLevel = 0;
        ChangeCitizenStat(CitizenStat.Fine);
        House.OnResidantCured();
    }

    public void AddSicknessLevel(float value) {
        if (_stat != CitizenStat.Curring) {
            _sicknessLevel = Mathf.Clamp(_sicknessLevel += value,0,StaticData.DEADTHREASHOLD);
            if (_sicknessLevel >= StaticData.DEADTHREASHOLD) ChangeCitizenStat(CitizenStat.Dead);
            else if (_sicknessLevel > StaticData.SICKTHREASHOLD) ChangeCitizenStat(CitizenStat.Sick);
            else if (_sicknessLevel < StaticData.SICKTHREASHOLD) ChangeCitizenStat(CitizenStat.Fine);
        }
    }

    public void ChangeCitizenStat(CitizenStat newStat) {
       
        
        if (newStat != _stat) {
            Debug.Log ("citizen Change Stat To "+ newStat);
            _stat = newStat;
            switch(_stat) {
                case CitizenStat.Dead: 
                    _house.OnResidentDead();
                    if (_workPlace != null) _workPlace.RemoveCitizenToWork(this);
                    break;
                case CitizenStat.Fine: _house.OnResidantCured(); break;
                case CitizenStat.Sick: _house.OnResidentSick(); break;
                case CitizenStat.Curring: _house.OnResidentCuring(); break;
                default: throw new Exception("Citizen stat broke :c");
            }
        }

    }

    private void StaticEventOnOnDoGameTick(object sender, EventArgs e) {
        if( _workPlace==null){ManagerLookingForJobs();}
    }

    private void ManagerLookingForJobs() {
        Vector3 pos = _house.Cell.position;
        float bestdistance = Mathf.Infinity;
        WorkingBuilding bestbuilding = null;
        foreach (var testedBuilding in StaticData.GetWorkingBuildingsLookingForWorkers()) {
            Debug.Log(testedBuilding.Cell.type.ToString());
            if (Vector3.Distance(testedBuilding.Cell.position, pos) < bestdistance) {
                bestdistance = Vector3.Distance(testedBuilding.Cell.position, pos);
                bestbuilding = testedBuilding;
            }
        }
        if (bestbuilding != null) {
            bestbuilding.AddCitizenToWork(this);
            _workPlace = bestbuilding;
            Debug.Log("Job found at "+ _workPlace);
        }
    }

    public void OnRemoveCitizen() {
        if (_workPlace!=null) _workPlace.RemoveCitizenToWork(this);
        StaticEvent.OnDoGameTick-= StaticEventOnOnDoGameTick;
        _house.RemoveCitizenFromHouse(this);
        StaticData.RemoveCitizen(this);
    }

    public void FireFromJobs() {
        _workPlace =null;
    }
    
}