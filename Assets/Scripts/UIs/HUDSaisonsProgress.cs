using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDSaisonsProgress : MonoBehaviour {
    [SerializeField] private Image _fillImage;
    [SerializeField] private TMP_Text _txtLable;
    [SerializeField] private Color _winterColor ;
    [SerializeField] private Color _sunnyDaysColor;
    private void Update() {
        _fillImage.fillAmount = StaticData.SaisonProgress;
        if (StaticData.CurrentSaison == StaticData.Saison.Winter) {
            _txtLable.text = "Winter";
            _fillImage.color = _winterColor;
        }
        else {
            _txtLable.text = "Sunny Days";
            _fillImage.color = _sunnyDaysColor;
        }
    }
}