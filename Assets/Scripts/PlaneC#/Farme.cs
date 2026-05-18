using System;
using UnityEngine;

public class Farme : WorkingBuilding {
    [SerializeField] private int _productionAmout = 20;
    private float _timer;
    private int _tickToPoduc = 24;
    int _maxWorkers = 2;

    public Farme(Cell cell) : base(cell)
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

    

    protected override void StaticEventOnOnDoGameTick(object sender, EventArgs e) {
        if (StaticData.CurrentSaison == StaticData.Saison.Winter) return;
        _timer+=GetProductionFactor();
        if (_timer >= _tickToPoduc) {
            _timer = 0;
            StaticData.ChangeFoodValue(_productionAmout);
            StaticEvent.DoPlayCue(new StructCueInformation(new Vector2(Cell.position.x, Cell.position.y), StructCueInformation.CueType.ProdFram, Cell.type));
        }
        base.StaticEventOnOnDoGameTick(sender, e);
    }
    
}