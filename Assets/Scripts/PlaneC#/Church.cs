using UnityEngine;
using System;
public class Church : WorkingBuilding
{
    private int _tickToPoduc = 60;
    float _timer;
    Citizen patient;
    Vector3Int _postition;
    protected override void StaticEventOnOnDoGameTick(object sender, EventArgs e)
    {
        if (patient == null)
        {
            LookForPatient();
        }
        else
        {
            _timer += GetProductionFactor();
            if (_timer >= _tickToPoduc)
            {
                _timer = 0;
                patient.Stat = Citizen.CitizenStat.Fine;
                patient.GetCured();
            }
        }
        base.StaticEventOnOnDoGameTick(sender, e);
    }
    void LookForPatient()
    {
        Citizen bestPatient = null;
        float bestDistance = Mathf.Infinity;
        foreach (Citizen citizen in StaticData.GetSickCitizen())
        {
            if (bestDistance > Vector3.Distance(cell.position, citizen.House.cell.position))
            {
                bestPatient = citizen;
                bestDistance = Vector3.Distance(cell.position, citizen.House.cell.position);
            }
        }
        if (bestPatient != null)
        {
            Debug.Log("found patient living in " + bestPatient.House.cell.position);
            patient = bestPatient;
            patient.Stat = Citizen.CitizenStat.Curring;
        }
    }
    [SerializeField] private int _foodStockAdded = 20;
    [SerializeField] private int _woodStockAdded = 30;



    //public override void OnRemove() {
    //    StaticData.ChangeFoodStockValue(-_foodStockAdded);
    //    StaticData.ChangeWoodStockValue(-_woodStockAdded);
    //    base.OnRemove();
    //}

    public override void AddCitizenToWork(Citizen citizen)
    {
        StaticData.ChangeFoodStockValue(_foodStockAdded);
        StaticData.ChangeWoodStockValue(_woodStockAdded);
        base.AddCitizenToWork(citizen);
    }

    public override void RemoveCitizenToWork(Citizen citizen)
    {
        StaticData.ChangeFoodStockValue(-_foodStockAdded);
        StaticData.ChangeWoodStockValue(-_woodStockAdded);
        base.RemoveCitizenToWork(citizen);
    }
}
