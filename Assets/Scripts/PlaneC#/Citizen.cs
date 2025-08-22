using System;
using UnityEngine;

[Serializable] 
public class Citizen {
    public enum CitizenStat {
        Fine, Sick, Curring , Dead
    }

    public string Name;
    public CitizenStat Stat;
    
    private float _sicknessLevel;
    private WorkingBuilding _workPlace;
    private House _house;
    
    public void AddSicknessLevel(float value) {
        _sicknessLevel += value;
        if(_sicknessLevel>StaticData.DEADTHREASHOLD)ChangeCitizenStat(CitizenStat.Dead);
        if(_sicknessLevel>StaticData.SICKTHREASHOLD)ChangeCitizenStat(CitizenStat.Sick);
        if(_sicknessLevel<StaticData.SICKTHREASHOLD)ChangeCitizenStat(CitizenStat.Fine);
    }

    private void ChangeCitizenStat(CitizenStat newStat) {
        if (newStat == CitizenStat.Dead) {
            if (_workPlace != null) {
                _workPlace.RemoveCitizenToWork(this);
            }
            Stat = newStat;
        }
    }

    public Citizen(House house) {
        _house = house;
        Name = StaticData.GetRandomName();
        StaticEvent.OnDoGameTick+= StaticEventOnOnDoGameTick;
    }

    private void StaticEventOnOnDoGameTick(object sender, EventArgs e) {
        if( _workPlace==null){ManagerLookingForJobs();}
    }

    private void ManagerLookingForJobs() {
        Vector2 pos = _house.transform.position;
        float bestdistance = Mathf.Infinity;
        WorkingBuilding bestbuilding = null;
        foreach (var testedBuilding in StaticData.GetWorkingBuildingsLookingForWorkers()) {
            if (Vector2.Distance(testedBuilding.transform.position, pos) < bestdistance) {
                bestdistance = Vector2.Distance(testedBuilding.transform.position, pos);
                bestbuilding = testedBuilding;
            }
        }
        if (bestbuilding != null) {
            bestbuilding.AddCitizenToWork(this);
            _workPlace = bestbuilding;
            Debug.Log("Job found at "+ _workPlace);
        }
    }

    private void OnRemoveCitizen() {
        if (_workPlace!=null) _workPlace.RemoveCitizenToWork(this);
        _house.RemoveCitizenFromHouse(this);
        StaticData.RemoveCitizen(this);
    }
    
}