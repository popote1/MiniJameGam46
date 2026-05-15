using System;
using UnityEngine;

public class Infirmary : WorkingBuilding
{
    int _tickToPoduc = 40;
    float _timer;
    Citizen patient;
    Vector3Int _postition;

    public Infirmary(Cell cell) : base(cell)
    {
        _cell = cell;
    }

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
                patient = null;
            }
        }
        base.StaticEventOnOnDoGameTick(sender, e);
    }
    public override float GetCurrentWorkProgess() {
        return _timer / _tickToPoduc;
    }
    void LookForPatient()
    {
        Citizen bestPatient = null;
        float bestDistance = Mathf.Infinity;
        foreach (Citizen citizen in StaticData.GetSickCitizen())
        {
            if (bestDistance > Vector3.Distance(Cell.position, citizen.House.Cell.position))
            {
                bestPatient = citizen;
                bestDistance = Vector3.Distance(Cell.position, citizen.House.Cell.position);
            }
        }
        if (bestPatient != null)
        {
            Debug.Log("found patient living in " + bestPatient.House.Cell.position);
            patient = bestPatient;
            patient.Stat = Citizen.CitizenStat.Curring;
        }
    }
}
