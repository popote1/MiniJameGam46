using System;
using UnityEngine;

public class GameObjectLevel2Manager : GameObjectiveManager
{

    [SerializeField] private int FoodToGet = 600;
    [SerializeField, TextArea] private string _winMessage;

    protected override void StaticEventOnOnDoGameTick(object sender, EventArgs e) {
        if (StaticData.CurrentFood >= FoodToGet) {
            PlayWin(_winMessage);
        }
        base.StaticEventOnOnDoGameTick(sender, e);
    }
}