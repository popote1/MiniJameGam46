using System;
using UnityEngine;

public class GameObjectLevel3Manager : GameObjectiveManager {

    [SerializeField] private int GoldToGet = 600;
    [SerializeField, TextArea] private string _winMessage;

    protected override void StaticEventOnOnDoGameTick(object sender, EventArgs e) {
        if (StaticData.Gold >= GoldToGet) {
            PlayWin(_winMessage);
        }
        base.StaticEventOnOnDoGameTick(sender, e);
    }
}