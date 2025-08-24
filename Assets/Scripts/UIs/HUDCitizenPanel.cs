using System;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class HUDCitizenPanel : MonoBehaviour
{
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


    public void DisplayCitizen(Citizen citizen) {
        gameObject.SetActive(true);
        _txtName.text = citizen.Name;
        switch (citizen.Stat)
        {
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
        _txtStat.text = citizen.GetSicknessvalue.ToString();
        if (citizen.WorkingBuilding == null) _txtWorkBuilding.text = "NoWork";
        else {
            _txtWorkBuilding.text = citizen.WorkingBuilding.ToString();
        }
    }

    public void HidePanel() {
        gameObject.SetActive(false);
    }

}