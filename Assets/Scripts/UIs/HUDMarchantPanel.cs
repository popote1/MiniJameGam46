using System;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HUDMarchantPanel : MonoBehaviour
{

    [SerializeField] private Toggle _toggleFoodToGold; 
    [SerializeField] private Toggle _toggleWoodToGold; 
    [SerializeField] private Toggle _toggleGoldToFood; 
    [SerializeField] private Toggle _toggleGoldToWood; 
    [SerializeField] private Toggle _toggleDontTrade;
    [SerializeField] private Button _bpClose;

    [SerializeField] private Transform _panelTransform;
    [SerializeField] private AudioElementSFX _audioElementSfxSlection;

    WorkingBuilding _targetMerchant;
    
    void Start() {
        StaticEvent.OnOpenMarchent+= StaticEventOnOnOpenMarchent;
        _bpClose.onClick.AddListener(UIClosePanel);
        _toggleFoodToGold.onValueChanged.AddListener(UIOnFoodToGoldSelected);
        _toggleWoodToGold.onValueChanged.AddListener(UIOnWoodToGoldSelected);
        _toggleGoldToFood.onValueChanged.AddListener(UIOnGoldToFoodSelected);
        _toggleGoldToWood.onValueChanged.AddListener(UIOnGoldToWoodSelected);
        _toggleDontTrade.onValueChanged.AddListener(UIOnDontTradeSelected);
    }

    private void StaticEventOnOnOpenMarchent(object sender, WorkingBuilding targetMerchant) {

        StaticData.MerchantStat e = targetMerchant.tradeType;
        _targetMerchant = targetMerchant;

        StaticData.ChangerGameStat(StaticData.GameStat.Stop);
        _panelTransform.gameObject.SetActive(true);

        

        switch (e) {
            case StaticData.MerchantStat.FoodToGold: _toggleFoodToGold.SetIsOnWithoutNotify( true); break;
            case StaticData.MerchantStat.WoodToGold:_toggleWoodToGold.SetIsOnWithoutNotify( true); break;
            case StaticData.MerchantStat.GoldToFood:_toggleGoldToFood.SetIsOnWithoutNotify( true); break;
            case StaticData.MerchantStat.GoldToWood:_toggleGoldToWood.SetIsOnWithoutNotify( true); break;
            case StaticData.MerchantStat.DontTrade:_toggleDontTrade.SetIsOnWithoutNotify( true); break;
            default:
                throw new ArgumentOutOfRangeException(nameof(e), e, null);
        }
    }

    private void UIClosePanel() {
        StaticData.ChangerGameStat(StaticData.GameStat.Playing);
        _panelTransform.gameObject.SetActive(false);
    }

    private void UIOnFoodToGoldSelected(bool value)
    {
        if( value)Debug.Log("Merchant Set On "); _targetMerchant.tradeType = StaticData.MerchantStat.FoodToGold;
        _audioElementSfxSlection.Play();
    }

    private void UIOnWoodToGoldSelected(bool value)
    {
        if( value)Debug.Log("Merchant Set On "); _targetMerchant.tradeType = StaticData.MerchantStat.WoodToGold;
        _audioElementSfxSlection.Play();
    }

    private void UIOnGoldToFoodSelected(bool value)
    {
        if( value)Debug.Log("Merchant Set On "); _targetMerchant.tradeType = StaticData.MerchantStat.GoldToFood;
        _audioElementSfxSlection.Play();
    }

    private void UIOnGoldToWoodSelected(bool value)
    {
        if( value)Debug.Log("Merchant Set On "); _targetMerchant.tradeType = StaticData.MerchantStat.GoldToWood;
        _audioElementSfxSlection.Play();
        
    }

    private void UIOnDontTradeSelected(bool value)
    {
        _audioElementSfxSlection.Play();
        if( value)Debug.Log("Merchant Set On "); _targetMerchant.tradeType = StaticData.MerchantStat.DontTrade;
    } 

}