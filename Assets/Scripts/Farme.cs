using System;
using UnityEngine;

public class Farme : WorkingBuilding {
    [SerializeField] private int _productionAmout = 3;
    private float _timer;

    protected override void StaticEventOnOnDoGameTick(object sender, EventArgs e) {
        _timer+=GetProductionFactor();
        if (_timer >= _tickToPoduc) {
            _timer = 0;
            StaticData.ChangeFoodValue(_productionAmout);
        }
    }
}