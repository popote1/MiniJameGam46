using TMPro;
using UnityEngine;

public class HUDHouseToolTips : MonoBehaviour
{
    [SerializeField] private TMP_Text _txtHouseName;
    [SerializeField] private TMP_Text _txtGoldIncome;
    [SerializeField] private HUDCitizenPanel[] _citizenPanel;

    public void DisplayHouseInfo(House house) {
        gameObject.SetActive(true);
        _txtHouseName.text = house.cell.type.ToString();
        int tax = 0;
        foreach (var citizen in house.GetCitizens) {
            if (citizen.Stat == Citizen.CitizenStat.Dead) continue;
            tax += house._taxeByCitizens;
        }
        _txtGoldIncome.text = tax.ToString();
        for (int i = 0; i < _citizenPanel.Length; i++) {
            if (house.GetCitizens.Count > i && house.GetCitizens[i] != null) {
                _citizenPanel[i].DisplayCitizen(house.GetCitizens[i]);
            }
            else _citizenPanel[i].HidePanel();
        }
    }

    public void HidePanel() {
        gameObject.SetActive(false);
    }
}