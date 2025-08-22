using System;
using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDRessourcePanelt : MonoBehaviour {
    [SerializeField]private Image _imageFood;
    [SerializeField] private TMP_Text _txtFood;
    
    [SerializeField]private Image _imageWood;
    [SerializeField] private TMP_Text _txtWood;


    private void Update() {
        DisplayRessource();
    }

    private void DisplayRessource() {
        
        _imageFood.fillAmount = (float)StaticData.CurrentFood / StaticData.FoodStock;
        _txtFood.text = StaticData.CurrentFood +"/"+ StaticData.FoodStock;
        
        _imageWood.fillAmount =(float) StaticData.CurrentWood / StaticData.WoodStock;
        _txtWood.text = StaticData.CurrentWood+"/"+ StaticData.WoodStock;
        
    }
}