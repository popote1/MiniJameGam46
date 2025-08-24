using System;
using System.Collections.Generic;
using UnityEngine;

public class House {
    public int CitizenCount = 2;
    private List<Citizen> _citizens = new List<Citizen>();
    public int _taxeByCitizens = 2;
    private int _tickBeforeFood = 8;

    private int _foodTimer;

    public float sicknessPoints;
    float neighborSicknessPoints;

    public Cell cell;
    
    public List<Citizen> GetCitizens { get => _citizens; }
    public void OnCreate() {
        StaticEvent.OnDoGameTick+= StaticEventOnOnDoGameTick;
        StaticEvent.OnDoLateGameTick+= StaticEventOnOnDoLateGameTick;
        StaticEvent.OnDoVeryLateGameTick+= StaticEventOnOnDoVeryLateGameTick;
        StaticEvent.OnTimeToTax+= StaticEventOnOnTimeToTax;
        
        for (int i = 0; i < CitizenCount; i++) {
            CreateNewCitizen();
        }
    }

    private void StaticEventOnOnTimeToTax(object sender, EventArgs e) {
        int taxegain = 0;
        foreach (var citizen in _citizens) {
            if (citizen == null) continue;
            if (citizen.Stat == Citizen.CitizenStat.Dead) continue;
            taxegain += _taxeByCitizens;
        }
        StaticData.ChangeGoldValue(taxegain);
    }

    private void StaticEventOnOnDoGameTick(object sender, EventArgs e) {
        _foodTimer++;
        if (_foodTimer >= _tickBeforeFood) {
            int foodNeed = 0;
            foreach (var citizen in _citizens) {
                if (citizen == null) continue;
                if (citizen.Stat == Citizen.CitizenStat.Dead) continue;
                foodNeed ++;
            }
            
            foreach (var citizen in _citizens) {
                citizen.IsMalnourish = StaticData.CurrentFood < foodNeed;
            }
            StaticData.ChangeFoodValue(-foodNeed);
            _foodTimer=0;
        }
        CalculateSickness();
    }
    private void StaticEventOnOnDoLateGameTick(object sender, EventArgs e)
    {
        //Debug.Log("hello");
        foreach (var neighbor in cell.gridManager.GetAdjacentCells(cell))
        {
            //Debug.Log(cell.type);
            if (neighbor.currentBuilding != null)
            {
                neighborSicknessPoints += neighbor.currentBuilding.sicknessPoints * StaticData.SICKNESSPREDFRACTION;
            }
            else if (neighbor.currentHouse != null)
            {
                
                neighborSicknessPoints += neighbor.currentHouse.sicknessPoints * StaticData.SICKNESSPREDFRACTION;
            }
        }
        Debug.Log(neighborSicknessPoints);
    }
    protected virtual void StaticEventOnOnDoVeryLateGameTick(object sender, EventArgs e)
    {
        for (int i = _citizens.Count; i > 0; i--)
        {
            _citizens[i - 1].AddSicknessLevel(sicknessPoints + neighborSicknessPoints);
        }
        sicknessPoints = 0;
        neighborSicknessPoints = 0;
    }
    private void CalculateSickness()
    {
        if (StaticData.CurrentSaison == StaticData.Saison.Winter)
        {
            sicknessPoints++;
        }
        foreach (var citizen in _citizens)
        {
            if (citizen.IsMalnourish)
            {
                sicknessPoints++;
            }
            if (citizen.Stat == Citizen.CitizenStat.Sick)
            {
                sicknessPoints++;
            }
            if (citizen.Stat == Citizen.CitizenStat.Dead)
            {
                sicknessPoints += 2;
            }
        }
        //Debug.Log(sicknessPoints);
    }
    private void CreateNewCitizen() {
        Citizen citizen = new Citizen(this);
        _citizens.Add(citizen);
        StaticData.AddCitizen(citizen);
    }

    public void RemoveCitizenFromHouse(Citizen citizen) {
        _citizens.Remove(citizen);
    }
    
    public void OnResidentSick()
    {

    }
    public void OnResidentDead()
    {

    }
    public void OnResidentCuring()
    {

    }
    public void OnResidantCured()
    {

    }

    public void OnRemove()
    {
        for (int i = _citizens.Count; i > 0; i--)
        {
            _citizens[i].OnRemoveCitizen();
        }

        StaticEvent.OnDoGameTick -= StaticEventOnOnDoGameTick;
        StaticEvent.OnDoLateGameTick -= StaticEventOnOnDoLateGameTick;
        StaticEvent.OnDoVeryLateGameTick -= StaticEventOnOnDoVeryLateGameTick;
        StaticEvent.OnTimeToTax -= StaticEventOnOnTimeToTax;
    }
}