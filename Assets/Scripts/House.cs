using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour {
    [SerializeField] private int CitizenCount = 2;
    [SerializeField] private List<Citizen> _citizens = new List<Citizen>();

    private void Start() {
        for (int i = 0; i < CitizenCount; i++) {
            CreateNewCitizen();
        }
    }

    private void CreateNewCitizen() {
        Citizen citizen = new Citizen(this);
        StaticData.AddCitizen(citizen);
    }

    public void RemoveCitizenFromHouse(Citizen citizen) {
        _citizens.Remove(citizen);
    }
    
}