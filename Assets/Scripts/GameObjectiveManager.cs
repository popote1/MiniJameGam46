using System;
using UnityEngine;

public class GameObjectiveManager : MonoBehaviour {
    [SerializeField] protected bool _playDialogueAtStart = true;
    [SerializeField, TextArea] protected string _description; 
    
    protected virtual void Start() {
        StaticEvent.OnDoGameTick += StaticEventOnOnDoGameTick;
        if( _playDialogueAtStart)PlayDialogue();
    }

    protected virtual void OnDestroy() {
        StaticEvent.OnDoGameTick -= StaticEventOnOnDoGameTick;
    }

    protected virtual void StaticEventOnOnDoGameTick(object sender, EventArgs e) {
        
    }

    protected virtual void PlayWin(string message) {
        StaticData.ChangerGameStat(StaticData.GameStat.Stop);
        StaticEvent.DoEndGame(new EndGameMessage(true , message));
    }

    protected virtual void PlayLose(string message) {
        StaticData.ChangerGameStat(StaticData.GameStat.Stop);
        StaticEvent.DoEndGame(new EndGameMessage(false , message));
    }

    protected virtual void PlayDialogue() {
        StaticEvent.DoDialogue(_description);
        StaticData.ChangerGameStat(StaticData.GameStat.Stop);
    }
}