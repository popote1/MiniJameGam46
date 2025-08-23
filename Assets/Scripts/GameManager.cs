using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private bool _playOnAwake;
    [SerializeField] private float _tickRate = 0.25f;

    [Space(10), Header("Taxes timer")] [SerializeField]
    private int _tickToTaxe =240;

    [Space(10), Header("Saison Timers")]
    [SerializeField] private int _winterTickDuration = 240;
    [SerializeField] private int _noWinterTickDuration = 480;
    [SerializeField] private StaticData.GameStat _currentGameStat;
    private float _timer ;
    private int _taxTimer;
    private int _saisonTimer;

    private void Awake()
    {
        StaticData.ClearAllData();
    }

    public void Start() {
        StaticEvent.OnDoGameTick+= StaticEventOnOnDoGameTick;
        StaticEvent.OnChangeGameStat += StaticEventOnOnChangeGameStat;
        if (_playOnAwake)StaticData.ChangerGameStat(StaticData.GameStat.Playing);
    }

    private void OnDestroy() {
        StaticEvent.OnDoGameTick-= StaticEventOnOnDoGameTick;
        StaticEvent.OnChangeGameStat -= StaticEventOnOnChangeGameStat;
    }

    private void StaticEventOnOnChangeGameStat(object sender, StaticData.GameStat newGameStat) {
        _currentGameStat = newGameStat;
    }

    private void StaticEventOnOnDoGameTick(object sender, EventArgs e) {
       ManagerTaxes();
       ManagerSaisonTimer();
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
        if (_currentGameStat!=StaticData.GameStat.Playing) return;
        _timer += Time.deltaTime;
        if (_timer > _tickRate) {
            StaticEvent.DoGameTick();
            StaticEvent.DoLateGameTick();
            StaticEvent.DoVeryLateGameTick();
            _timer -= _tickRate;
            //Debug.Log("Tick");
        }
    }

    private void ManagerSaisonTimer() {
        _saisonTimer++;
        if (StaticData.CurrentSaison == StaticData.Saison.Winter)
        {
            StaticData.SaisonProgress = (float) _saisonTimer / _winterTickDuration;
            if (_saisonTimer >= _winterTickDuration) {
                StaticData.ChangeSaison(StaticData.Saison.NoWinter);
                _saisonTimer = 0;
            }
        }
        if (StaticData.CurrentSaison == StaticData.Saison.NoWinter) {
            StaticData.SaisonProgress = (float) _saisonTimer / _noWinterTickDuration;
            if (_saisonTimer >= _noWinterTickDuration) {
                StaticData.ChangeSaison(StaticData.Saison.Winter);
                _saisonTimer = 0;
            }
        }
    }
    
    [ContextMenu("PassToTheNextSaison")]
    private void PassToNextSaison() {
        if (StaticData.CurrentSaison == StaticData.Saison.Winter) {
            StaticData.ChangeSaison(StaticData.Saison.NoWinter);
                _saisonTimer = 0;
        }else if (StaticData.CurrentSaison == StaticData.Saison.NoWinter) {
            StaticData.ChangeSaison(StaticData.Saison.Winter);
        }
        _saisonTimer = 0;
    }

    [ContextMenu("Debug ApplyCurrentGameStat")]
    private void DebugApplyCurrentGameStat() {
        StaticData.ChangerGameStat(_currentGameStat);
    }
    
}