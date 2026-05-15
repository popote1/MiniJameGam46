using UnityEngine;
using System;
using UnityEngine.Rendering;

public class MerchantDocks : WorkingBuilding
{
    int _tickToPoduc = 120;
    int tradeAmount = 10;
    float _timer;
    //float tradeMultiplier = 2f;
    bool isMerchantSick;

    public MerchantDocks(Cell cell) : base(cell)
    {
        _cell = cell;
    }

    public override float GetCurrentWorkProgess() {
        return _timer / _tickToPoduc;
    }
    
    protected override void CalculateSickness()
    {
        if (isMerchantSick)
        {
            sicknessPoints += 400;
            isMerchantSick = false;
        }
        base.CalculateSickness();
    }

    public void SetMerchantSick() => isMerchantSick = true; 
    protected override void StaticEventOnOnDoGameTick(object sender, EventArgs e)
    {
        //if (StaticData.CurrentSaison == StaticData.Saison.Winter) return;
        //if (tradeType == StaticData.MerchantStat.DontTrade) return;
        _timer += GetProductionFactor();
       
        
        if (_timer >= _tickToPoduc)
        {
            
            StaticEvent.DoPlayCue(new StructCueInformation(new Vector2(Cell.position.x, Cell.position.y), StructCueInformation.CueType.Merchant, Cell.type));
            _timer = 0;
            StaticEvent.DoMerchantCall(this);
            
           /* 
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
            */
        }
       
       
       
       
        base.StaticEventOnOnDoGameTick(sender, e);
    }
}
