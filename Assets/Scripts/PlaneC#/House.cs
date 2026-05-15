using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class House {
    
    private List<Citizen> _citizens = new List<Citizen>();
    public int TaxByCitizens { get => _taxByCitizens; }
    private int _tickBeforeFood = 8;
    private int _citizenCount = 2;
    private int _foodTimer;

    public float sicknessPoints;
    float neighborSicknessPoints;

    public Cell Cell { get => _cell; }

    private Cell _cell;
    private int _taxByCitizens;
    
    public List<Citizen> GetCitizens { get => _citizens; }

    public House(Cell cell, int taxByCitizens = -1, int citizenCount = -1) {
        if( taxByCitizens!= -1) _taxByCitizens = taxByCitizens;
        if( citizenCount!=-1) _citizenCount = citizenCount;
        _cell = cell;
        OnCreate();
    }
    public void OnCreate() {
        StaticEvent.OnDoGameTick+= StaticEventOnOnDoGameTick;
        StaticEvent.OnDoLateGameTick+= StaticEventOnOnDoLateGameTick;
        StaticEvent.OnDoVeryLateGameTick+= StaticEventOnOnDoVeryLateGameTick;
        StaticEvent.OnTimeToTax+= StaticEventOnOnTimeToTax;
        
        for (int i = 0; i < _citizenCount; i++) {
            CreateNewCitizen();
        }
    }

    private void StaticEventOnOnTimeToTax(object sender, EventArgs e) {
        int taxegain = 0;
        foreach (var citizen in _citizens) {
            if (citizen == null) continue;
            if (citizen.Stat == Citizen.CitizenStat.Dead) continue;
            taxegain += _taxByCitizens;
        }
        StaticData.ChangeGoldValue(taxegain);
        StaticEvent.DoPlayCue(new StructCueInformation(new Vector2(Cell.position.x, Cell.position.y), StructCueInformation.CueType.Gold, Cell.type));
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
        foreach (var neighbor in GridMangaer.Instance.GetAdjacentCells(Cell))
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
        //Debug.Log(neighborSicknessPoints);
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
        if (_citizens[0].IsMalnourish) {
            sicknessPoints++;
        }

        if ((StaticData.CurrentSaison == StaticData.Saison.NoWinter) && !_citizens[0].IsMalnourish) {
            sicknessPoints--;
        }
        foreach (var citizen in _citizens)
        {
            
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
        StaticEvent.DoPlayCue(new StructCueInformation(new Vector2(Cell.position.x, Cell.position.y) , StructCueInformation.CueType.Sick, Cell.type));
    }
    public void OnResidentDead()
    {
        StaticEvent.DoPlayCue(new StructCueInformation(new Vector2(Cell.position.x, Cell.position.y), StructCueInformation.CueType.Dead, Cell.type));
    }
    public void OnResidentCuring()
    {
        
    }
    public void OnResidantCured()
    {
        StaticEvent.DoPlayCue(new StructCueInformation(new Vector2(Cell.position.x, Cell.position.y), StructCueInformation.CueType.Cure, Cell.type));
    }

    public void OnRemove()
    {
        for (int i = _citizens.Count-1; i >= 0; i--)
        {
            _citizens[i].OnRemoveCitizen();
        }

        StaticEvent.OnDoGameTick -= StaticEventOnOnDoGameTick;
        StaticEvent.OnDoLateGameTick -= StaticEventOnOnDoLateGameTick;
        StaticEvent.OnDoVeryLateGameTick -= StaticEventOnOnDoVeryLateGameTick;
        StaticEvent.OnTimeToTax -= StaticEventOnOnTimeToTax;
    }
}