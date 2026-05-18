using System;
using UnityEngine;

public class FishDocks : WorkingBuilding
{
    int _tickToPoduc = 12;
    float _timer = 0f;
    int _productionAmount = 24;
    int _maxWorkers = 3;

    public FishDocks(Cell cell) : base(cell)
    {
        _cell = cell;
    }

    public override void OnCreate()
    {
        ChangeMaxWorkers(_maxWorkers);
        base.OnCreate();
    }

    public override float GetCurrentWorkProgess() {
        return _timer / _tickToPoduc;
    }
    protected override void CalculateSickness()
    {
        sicknessPoints += StaticData.SICKNESSMOD_FICHINGDOCK;
        base.CalculateSickness();
    }
    protected override void StaticEventOnOnDoGameTick(object sender, EventArgs e)
    {
        _timer += GetProductionFactor();
        if (_timer >= _tickToPoduc)
        {
            _timer = 0;
            StaticEvent.DoPlayCue(new StructCueInformation(new Vector2(Cell.position.x, Cell.position.y), StructCueInformation.CueType.ProdFish, Cell.type));
            StaticData.ChangeFoodValue(_productionAmount);
        }
        base.StaticEventOnOnDoGameTick(sender, e);
    }
}
