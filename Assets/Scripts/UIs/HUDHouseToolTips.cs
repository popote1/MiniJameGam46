using TMPro;
using UnityEngine;

public class HUDHouseToolTips : MonoBehaviour
{
    [SerializeField] private TMP_Text _txtHouseName;
    [SerializeField] private TMP_Text _textCoordiates;
    [SerializeField] private HUDCitizenPanel[] _citizenPanel;

    public void DisplayHouseInfo(House house) {
        gameObject.SetActive(true);
        _txtHouseName.text = house.name;
        _textCoordiates.text = house.transform.position.ToString();
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