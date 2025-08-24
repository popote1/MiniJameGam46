using System;
using UnityEngine;
public class Sawmill : WorkingBuilding {

    private int _tickToPoduc = 20;
    private int _productionAmout = 3;
    private float _timer;

    protected override void StaticEventOnOnDoGameTick(object sender, EventArgs e) {
        _timer+=GetProductionFactor();
        if (_timer >= _tickToPoduc) {
            _timer = 0;
            StaticData.ChangeWoodValue(_productionAmout);
        }
        base.StaticEventOnOnDoGameTick(sender, e);
    }
}