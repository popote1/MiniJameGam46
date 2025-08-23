using System;
using UnityEngine;
public class Sawmill : WorkingBuilding {
    

    [SerializeField] private int _productionAmout = 3;
    private float _timer;

    protected override void StaticEventOnOnDoGameTick(object sender, EventArgs e) {
        _timer+=GetProductionFactor();
        if (_timer >= _tickToPoduc) {
            _timer = 0;
            StaticData.ChangeWoodValue(_productionAmout);
        }
    }
}