using UnityEngine;
using System;
using UnityEngine.Splines;
public class MerchantDocks : WorkingBuilding
{
    SplineAnimate arrival;
    SplineAnimate departure;
    int _tickToPoduc = 60;
    int tradeAmount = 10;
    float _timer;
    //float tradeMultiplier = 2f;
    bool isMerchantSick;
    
    protected override void CalculateSickness()
    {
        if (isMerchantSick)
        {
            sicknessPoints += 400;
            isMerchantSick = false;
        }
        base.CalculateSickness();
    }
    protected override void StaticEventOnOnDoGameTick(object sender, EventArgs e)
    {
        if (StaticData.CurrentSaison == StaticData.Saison.Winter) return;
        if (tradeType == StaticData.MerchantStat.DontTrade) return;
        _timer += GetProductionFactor();
        if (_timer >= _tickToPoduc - 10)
        {
            arrival.Play();
        }
        if (_timer >= _tickToPoduc)
        {
            if (UnityEngine.Random.Range(0.00f, 1.00f) <= StaticData.MERCHANTSICKNESCHANCE)
            {
                isMerchantSick = true;
            }
            StaticEvent.DoPlayCue(new StructCueInformation(new Vector2(cell.position.x, cell.position.y), StructCueInformation.CueType.Merchant, cell.type));
            _timer = 0;
            if(tradeType == StaticData.MerchantStat.FoodToGold)
            {
                StaticData.ChangeFoodValue(-tradeAmount);
                StaticData.ChangeGoldValue(tradeAmount);
            }
            else if (tradeType == StaticData.MerchantStat.WoodToGold)
            {
                StaticData.ChangeWoodValue(-tradeAmount);
                StaticData.ChangeGoldValue(tradeAmount);
            }
            else if (tradeType == StaticData.MerchantStat.GoldToFood)
            {
                StaticData.ChangeFoodValue(tradeAmount);
                StaticData.ChangeGoldValue(-tradeAmount);
            }
            else if (tradeType == StaticData.MerchantStat.GoldToWood)
            {
                StaticData.ChangeWoodValue(tradeAmount);
                StaticData.ChangeGoldValue(-tradeAmount);
            }
            //StaticData.ChangeFoodValue();
        }
        if (_timer >= _tickToPoduc + 10)
        {
            departure.Play();
        }
        base.StaticEventOnOnDoGameTick(sender, e);
    }
}
