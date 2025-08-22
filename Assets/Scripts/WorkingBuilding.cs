using System.Collections.Generic;
using UnityEngine;

public class WorkingBuilding:MonoBehaviour
{
    [SerializeField] protected int WorkingCount =1;
    [SerializeField] protected List<Citizen> _citizens = new List<Citizen>();

    public bool IsLookingForWorker => _citizens.Count < WorkingCount;
    public virtual void AddCitizenToWork(Citizen citizen)=> _citizens.Add(citizen);
    public virtual void RemoveCitizenToWork(Citizen citizen)=> _citizens.Remove(citizen);

    protected virtual void Start() {
        StaticData.AddWorkingBuilding(this);
    }
    protected virtual float GetProductionFactor() {
        float productivity = 0;
        foreach (var citizen in _citizens) {
            switch (citizen.Stat) {
                case Citizen.CitizenStat.Fine: productivity += 1; break;
                case Citizen.CitizenStat.Sick: productivity += 0.5f; break;
                case Citizen.CitizenStat.Curring: productivity += 0; break;
                case Citizen.CitizenStat.Dead: productivity += 0; break;
            }
        }
        return productivity / WorkingCount;
    }
    protected virtual void OnDestroy() {
        StaticData.RemoveWorkingBuilding(this);
    }
}