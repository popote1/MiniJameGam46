using System;
using UnityEngine;
using UnityEngine.UI;

public class HUDPauseManager: MonoBehaviour {
    [SerializeField] private HUDPauseMenu _hudPauseMenu;
    [SerializeField] private Button _bpPauseMenu;

    public void Awake() {
        StaticEvent.OnChangeGameStat += StaticEventOnOnChangeGameStat;
        _bpPauseMenu.onClick.AddListener(UiPauseButton);

        _hudPauseMenu.OnPanelCLose+= HudPauseMenuOnOnPanelCLose;
    }

    private void OnDestroy() {
        StaticEvent.OnChangeGameStat -= StaticEventOnOnChangeGameStat;
        _hudPauseMenu.OnPanelCLose-= HudPauseMenuOnOnPanelCLose;
    }

    private void HudPauseMenuOnOnPanelCLose(object sender, EventArgs e) {
        StaticData.ChangerGameStat(StaticData.GameStat.Playing);
    }

    private void UiPauseButton() {
        if (StaticData.CurrentGameStat == StaticData.GameStat.Playing) {
            StaticData.ChangerGameStat(StaticData.GameStat.Paused);
        }
    }

    private void StaticEventOnOnChangeGameStat(object sender, StaticData.GameStat newGameStat) {
        if (newGameStat == StaticData.GameStat.Paused) {
            _hudPauseMenu.OpenPanel();
        }
    }
}