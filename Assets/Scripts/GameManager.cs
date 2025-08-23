using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float _tickRate = 0.25f;
    [SerializeField] private bool _doTicks = true;

    [Space(10), Header("Taxes timer")] [SerializeField]
    private int _tickToTaxe =240;

    private float _timer ;
    private int _taxTimer;

    public void Start()
    {
        StaticEvent.OnDoGameTick+= StaticEventOnOnDoGameTick;
    }

    private void StaticEventOnOnDoGameTick(object sender, EventArgs e) {
       ManagerTaxes();
    }

    private void ManagerTaxes()
    {
        _taxTimer++;
        StaticData.TaxesProgress = (float)_taxTimer / _tickToTaxe;
        if (_taxTimer >= _tickToTaxe) {
            _taxTimer = 0;
            StaticEvent.DoTimeToTax();
        }
    }

    private void Update() {
        ManagerTick();
    }

    private void ManagerTick() {
        if (!_doTicks) return;
        _timer += Time.deltaTime;
        if (_timer > _tickRate) {
            StaticEvent.DoGameTick();
            StaticEvent.DoLateGameTick();
            _timer -= _tickRate;
            Debug.Log("Tick");
        }
    }
    
}