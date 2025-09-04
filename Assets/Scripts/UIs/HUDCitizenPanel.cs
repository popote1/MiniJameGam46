using System;
using TMPro;
using UnityEngine;
using Image = UnityEngine.UI.Image;

public class HUDCitizenPanel : MonoBehaviour
{

    [SerializeField] private GameObject _goCitizenInfos;
    [SerializeField] private GameObject _goEmpty;
    [Space(10)]
    [SerializeField] private TMP_Text _txtName;
    [SerializeField] private TMP_Text _txtStat;
    [SerializeField] private TMP_Text _txtWorkBuilding;
    [SerializeField] private Image _imgStatus;

    [SerializeField] private Sprite _spriteFine;
    [SerializeField] private Sprite _spriteSick;
    [SerializeField] private Sprite _spriteDead;
    [SerializeField] private Sprite _spriteCure;
    [SerializeField] private Color _colorFine;
    [SerializeField] private Color _colorSick;
    [SerializeField] private Color _colorDead;
    [SerializeField] private Color _colorCure;
    [SerializeField] private Image _imgsickness;

    private Citizen _currentcitizen;

    public void DisplayCitizen(Citizen citizen) {
        gameObject.SetActive(true);

        if (citizen == null) {
            _goEmpty.SetActive(true);
            _goCitizenInfos.SetActive(false);
            _currentcitizen = null;
            return;
        }
        
        _goEmpty.SetActive(false);
        _goCitizenInfos.SetActive(true);
        _currentcitizen = citizen;
        
        _txtName.text = citizen.Name;
        switch (citizen.Stat) {
            case Citizen.CitizenStat.Fine:
                _imgStatus.sprite = _spriteFine;
                _imgsickness.color = _colorFine;
                break;
            case Citizen.CitizenStat.Sick:
                _imgStatus.sprite = _spriteSick;
                _imgsickness.color = _colorSick;
                break;
            case Citizen.CitizenStat.Curring:
                _imgStatus.sprite = _spriteCure;
                _imgsickness.color = _colorCure;
                break;
            case Citizen.CitizenStat.Dead:
                _imgStatus.sprite = _spriteDead;
                _imgsickness.color = _colorDead;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        _imgsickness.fillAmount = (float)citizen.GetSicknessvalue / 1000;
        _txtStat.enabled = citizen.Stat != Citizen.CitizenStat.Dead;
        _txtStat.text = citizen.GetSicknessvalue.ToString();
        if (citizen.WorkingBuilding == null) _txtWorkBuilding.text = "NoWork";
        else {
            _txtWorkBuilding.text = citizen.WorkingBuilding.ToString();
        }
    }

    private void Update()
    {
        if (_currentcitizen == null) return;
        DisplayCitizen(_currentcitizen);
    }

    public void HidePanel() {
        gameObject.SetActive(false);
        _currentcitizen = null;
    }

}