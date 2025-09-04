using System;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HUDWorkingPlaceToolTips : MonoBehaviour
{
    [SerializeField] private TMP_Text _txtBuildingName;
    [SerializeField] private TMP_Text _txtWorkerCount;
    [SerializeField] private Image _imgProgression;
    [SerializeField] private Image _imgRessourceProd;
    [SerializeField] private TMP_Text _txtProductionFactor;
    [SerializeField] private HUDCitizenPanel[] _citizenPanel;
    [Space(10)] 
    [SerializeField] private Sprite _spriteWeed;
    [SerializeField] private Sprite _spriteFish;
    [SerializeField] private Sprite _spriteWood;
    [SerializeField] private Sprite _spriteballon;
    [SerializeField] private Sprite _spriteMedic;
    
    private WorkingBuilding _currentbuilding;
    
    public void DisplayHouseInfo(WorkingBuilding building) {
        gameObject.SetActive(true);
        _currentbuilding = building;
        _txtBuildingName.text = building.cell.type.ToString();
        _txtWorkerCount.text = building.Workers.Count + "/" + building.MaxWorker;
        for (int i = 0; i < _citizenPanel.Length; i++) {
            if (building.Workers.Count > i && building.Workers[i] != null) {
                _citizenPanel[i].DisplayCitizen(building.Workers[i]);
            }
            else if (i<building.MaxWorker) {
                _citizenPanel[i].DisplayCitizen(null);
            }
            else _citizenPanel[i].HidePanel();
        }

        if (building is Farme) {
            _imgRessourceProd.sprite = _spriteWeed;
            _imgProgression.enabled = true;
        }
        if (building is FishDocks) {
            _imgRessourceProd.sprite = _spriteFish;
            _imgProgression.enabled = true;
        }
        if (building is Sawmill) {
            _imgRessourceProd.sprite = _spriteWood;
            _imgProgression.enabled = true;
        }
        if (building is MerchantDocks) {
            _imgRessourceProd.sprite = _spriteballon;
            _imgProgression.enabled = true;
        }
        if (building is Infirmary) {
            _imgRessourceProd.sprite = _spriteMedic;
            _imgProgression.enabled = true;
        }

        if (building is Warehouse) {
            _imgProgression.enabled = false;
        }
        _imgProgression.fillAmount = building.GetCurrentWorkProgess();
    }

    public void Update()
    {
        if (_currentbuilding != null)
        {
            DisplayHouseInfo(_currentbuilding);
        }
    }


    public void HidePanel() {
        gameObject.SetActive(false);
        _currentbuilding = null;
    }
}