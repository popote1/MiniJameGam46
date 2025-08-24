using System;
using UnityEngine;

[Serializable] 
public class Citizen {
    public enum CitizenStat {
        Fine, Sick, Curring , Dead
    }

    public string Name;
    public CitizenStat Stat;
    public bool IsMalnourish;
    
    private float _sicknessLevel;
    private WorkingBuilding _workPlace;
    private House _house;
    
    public House House { get => _house; }
    public WorkingBuilding WorkingBuilding { get => _workPlace; }

    public void AddSicknessLevel(float value) {
        if (Stat != CitizenStat.Curring)
        {
            _sicknessLevel += value;
            if (_sicknessLevel > StaticData.DEADTHREASHOLD) ChangeCitizenStat(CitizenStat.Dead);
            else if (_sicknessLevel > StaticData.SICKTHREASHOLD) ChangeCitizenStat(CitizenStat.Sick);
            else if (_sicknessLevel < StaticData.SICKTHREASHOLD) ChangeCitizenStat(CitizenStat.Fine);
        }
    }

    private void ChangeCitizenStat(CitizenStat newStat) {
        if (newStat == CitizenStat.Dead) {
            if (_workPlace != null) {
                _workPlace.RemoveCitizenToWork(this);
            }
        }
        if (newStat != Stat)
        {
            Stat = newStat;
            switch(Stat)
            {
                case CitizenStat.Dead:
                    _house.OnResidentDead();
                    break;
                case CitizenStat.Fine:
                    _house.OnResidantCured();
                    break;
                case CitizenStat.Sick:
                    _house.OnResidentSick();
                    break;
                case CitizenStat.Curring:
                    _house.OnResidentCuring();
                    break;
                default:
                    throw new Exception("Citizen stat broke :c");
            }
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
        Vector3 pos = _house.cell.position;
        float bestdistance = Mathf.Infinity;
        WorkingBuilding bestbuilding = null;
        foreach (var testedBuilding in StaticData.GetWorkingBuildingsLookingForWorkers()) {
            Debug.Log(testedBuilding.cell.type.ToString());
            if (Vector3.Distance(testedBuilding.cell.position, pos) < bestdistance) {
                bestdistance = Vector3.Distance(testedBuilding.cell.position, pos);
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
    
}