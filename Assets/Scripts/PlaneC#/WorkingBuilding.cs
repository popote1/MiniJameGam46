using System;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class WorkingBuilding
{
    public StaticData.MerchantStat tradeType = StaticData.MerchantStat.DontTrade;
    protected int _workingCount =1;
    protected List<Citizen> _citizens = new List<Citizen>();
    //protected int _tickToPoduc = 40;

    public Cell cell;

    public float sicknessPoints;
    public float neighboringSicknessPoints;

    public List<Citizen> Workers { get => _citizens; }
    public int MaxWorker { get => _workingCount; }
    public bool IsLookingForWorker => _citizens.Count < _workingCount;
    
    protected void ChangeMaxWorkers( int newMax)
    {
        _workingCount = newMax;
    }

    public virtual float GetCurrentWorkProgess() {
        return 0;
    }
    
    public virtual void AddCitizenToWork(Citizen citizen)=> _citizens.Add(citizen);
    public virtual void RemoveCitizenToWork(Citizen citizen)=> _citizens.Remove(citizen);

    public virtual void OnCreate() {
        StaticData.AddWorkingBuilding(this);
        StaticEvent.OnDoGameTick+= StaticEventOnOnDoGameTick;
        StaticEvent.OnDoLateGameTick += StaticEventOnOnDoLateGameTick;
        StaticEvent.OnDoVeryLateGameTick += StaticEventOnOnDoVeryLateGameTick;
    }

    protected virtual void StaticEventOnOnDoGameTick(object sender, EventArgs e) {
        CalculateSickness();
    }

    protected void StaticEventOnOnDoLateGameTick(object sender, EventArgs e) {
        
        foreach (var neighbor in cell.gridManager.GetAdjacentCells(cell))
        {
            if (neighbor.currentBuilding != null)
            {
                neighboringSicknessPoints += neighbor.currentBuilding.sicknessPoints * StaticData.SICKNESSPREDFRACTION;
            }
            else if (neighbor.currentHouse != null)
            {
                neighboringSicknessPoints += neighbor.currentHouse.sicknessPoints * StaticData.SICKNESSPREDFRACTION;
            }
        }
    }

    protected virtual void StaticEventOnOnDoVeryLateGameTick(object sender, EventArgs e)
    {
        for ( int i = _citizens.Count; i > 0; i--)
        {
            _citizens[i - 1].AddSicknessLevel(sicknessPoints + neighboringSicknessPoints);
        }
        sicknessPoints = 0;
        neighboringSicknessPoints = 0;
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

    protected virtual void CalculateSickness()
    {
        if (StaticData.CurrentSaison == StaticData.Saison.Winter)
        {
            sicknessPoints++;
        }
        foreach (var citizen in _citizens)
        {
            if (citizen.Stat == Citizen.CitizenStat.Sick)
            {
                sicknessPoints++;
            }
        }

    }

    public virtual void OnRemove() {
        foreach (Citizen worker in _citizens) {
            worker.FireFromJobs();
        }
        StaticData.RemoveWorkingBuilding(this);
        StaticEvent.OnDoGameTick-= StaticEventOnOnDoGameTick;
        StaticEvent.OnDoLateGameTick-= StaticEventOnOnDoLateGameTick;
        StaticEvent.OnDoVeryLateGameTick-= StaticEventOnOnDoVeryLateGameTick;
    }
    
}