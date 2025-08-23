using TMPro;
using UnityEngine;

public class HUDWorkingPlaceToolTips : MonoBehaviour
{
    [SerializeField] private TMP_Text _txtBuildingName;
    [SerializeField] private TMP_Text _textCoordiates;
    [SerializeField] private TMP_Text _txtWorkerCount;
    [SerializeField] private TMP_Text _txtProductionFactor;
    [SerializeField] private HUDCitizenPanel[] _citizenPanel;
    
    public void DisplayHouseInfo(WorkingBuilding building) {
        gameObject.SetActive(true);
        _txtBuildingName.text = building.name;
        _textCoordiates.text = building.transform.position.ToString();
        _txtWorkerCount.text = building.Workers.Count + "/" + building.MaxWorker;
        _txtProductionFactor.text = building.GetProductionFactor().ToString();
        for (int i = 0; i < _citizenPanel.Length; i++) {
            if (building.Workers.Count > i && building.Workers[i] != null) {
                _citizenPanel[i].DisplayCitizen(building.Workers[i]);
            }
            else _citizenPanel[i].HidePanel();
        }
    }

    public void HidePanel() {
        gameObject.SetActive(false);
    }
}