using System;
using UnityEngine;

public class FishDocks : WorkingBuilding
{
    int _tickToPoduc = 12;
    float _timer = 0f;
    int _productionAmount = 24;
    protected override void CalculateSickness()
    {
        sicknessPoints += 2;
        base.CalculateSickness();
    }
    protected override void StaticEventOnOnDoGameTick(object sender, EventArgs e)
    {
        _timer += GetProductionFactor();
        if (_timer >= _tickToPoduc)
        {
            _timer = 0;
            StaticEvent.DoPlayCue(new StructCueInformation(new Vector2(cell.position.x, cell.position.y), StructCueInformation.CueType.ProdFish, cell.type));
            StaticData.ChangeFoodValue(_productionAmount);
        }
        base.StaticEventOnOnDoGameTick(sender, e);
    }
}
