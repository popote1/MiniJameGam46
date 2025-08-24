using System;
using UnityEngine;
public class Sawmill : WorkingBuilding {

    private int _tickToPoduc = 20;
    private int _productionAmout = 3;
    private float _timer;
    private int _maxWorkers = 3;

    public override void OnCreate () 
    {
        ChangeMaxWorkers(_maxWorkers);
        base.OnCreate ();
    }

    protected override void StaticEventOnOnDoGameTick(object sender, EventArgs e) {
        _timer+=GetProductionFactor();
        if (_timer >= _tickToPoduc) {
            _timer = 0;
            StaticData.ChangeWoodValue(_productionAmout);
            StaticEvent.DoPlayCue(new StructCueInformation(new Vector2(cell.position.x, cell.position.y), StructCueInformation.CueType.ProdWoof, cell.type));
        }
        base.StaticEventOnOnDoGameTick(sender, e);
    }
}