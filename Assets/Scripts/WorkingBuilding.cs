using System;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class WorkingBuilding
{
    [SerializeField] protected int _workingCount =1;
    [SerializeField] protected List<Citizen> _citizens = new List<Citizen>();
    [SerializeField] protected int _tickToPoduc = 40;

    public Cell cell;
    
    
    public List<Citizen> Workers { get => _citizens; }
    public int MaxWorker { get => _workingCount; }
    public bool IsLookingForWorker => _citizens.Count < _workingCount;
    
    
    public virtual void AddCitizenToWork(Citizen citizen)=> _citizens.Add(citizen);
    public virtual void RemoveCitizenToWork(Citizen citizen)=> _citizens.Remove(citizen);

    public virtual void OnCreate() {
        StaticData.AddWorkingBuilding(this);
        StaticEvent.OnDoGameTick+= StaticEventOnOnDoGameTick;
    }

    protected virtual void StaticEventOnOnDoGameTick(object sender, EventArgs e) {
        
    }

    public virtual float GetProductionFactor() {
        float productivity = 0;
        foreach (var citizen in _citizens) {
            switch (citizen.Stat) {
                case Citizen.CitizenStat.Fine: productivity += 1; break;
                case Citizen.CitizenStat.Sick: productivity += 0.5f; break;
                case Citizen.CitizenStat.Curring: productivity += 0; break;
                case Citizen.CitizenStat.Dead: productivity += 0; break;
            }
        }
        return productivity / _workingCount;
    }
    public virtual void OnRemove() {
        StaticData.RemoveWorkingBuilding(this);
        StaticEvent.OnDoGameTick-= StaticEventOnOnDoGameTick;
    }
}