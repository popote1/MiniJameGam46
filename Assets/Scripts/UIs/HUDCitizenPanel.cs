using TMPro;
using UnityEngine;

public class HUDCitizenPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _txtName;
    [SerializeField] private TMP_Text _txtStat;
    [SerializeField] private TMP_Text _txtHouseCoordonates;
    [SerializeField] private TMP_Text _txtWorkBuilding;


    public void DisplayCitizen(Citizen citizen) {
        gameObject.SetActive(true);
        _txtName.text = citizen.Name;
        _txtStat.text = citizen.Stat.ToString();
        _txtHouseCoordonates.text = citizen.House.transform.position.ToString();
        if (citizen.WorkingBuilding == null) _txtWorkBuilding.text = "NoWork";
        else {
            _txtWorkBuilding.text = citizen.WorkingBuilding.ToString() + " coordirante : " +
                                    citizen.WorkingBuilding.transform.position;
        }
    }

    public void HidePanel() {
        gameObject.SetActive(false);
    }

}