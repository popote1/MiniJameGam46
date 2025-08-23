using System;
using System.Collections.Generic;
using UnityEngine;

public class House {
    public int CitizenCount = 2;
    [SerializeField] private List<Citizen> _citizens = new List<Citizen>();
    public int _taxeByCitizens = 2;
    [SerializeField] private int _tickBeforeFood = 8;

    private int _foodTimer;

    public Cell cell;
    
    public List<Citizen> GetCitizens { get => _citizens; }
    public void OnCreate() {
        StaticEvent.OnDoGameTick+= StaticEventOnOnDoGameTick;
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
    }

    private void CreateNewCitizen() {
        Citizen citizen = new Citizen(this);
        _citizens.Add(citizen);
        StaticData.AddCitizen(citizen);
    }

    public void RemoveCitizenFromHouse(Citizen citizen) {
        _citizens.Remove(citizen);
    }
    
    public void OnRemove()
    {

    }
}