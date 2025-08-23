using UnityEngine;
using System;

public class MerchantDocks : WorkingBuilding
{
    float _timer;
    float tradeMultiplier = 2f;
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
        _timer += GetProductionFactor();
        if (_timer >= _tickToPoduc)
        {
            _timer = 0;
            //StaticData.ChangeFoodValue();
        }
        base.StaticEventOnOnDoGameTick(sender, e);
    }
}
