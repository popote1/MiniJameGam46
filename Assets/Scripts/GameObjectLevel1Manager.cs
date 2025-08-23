using UnityEngine;

public class GameObjectLevel1Manager : GameObjectiveManager
{

    [SerializeField] private int _saisonsToSurvive = 6;
    private int SaisonCount = 0;
    [SerializeField, TextArea] private string _winMessage;

    protected override void Start() {
        StaticEvent.OnSaisonChange+= StaticEventOnOnSaisonChange;
        base.Start();
    }

    private void StaticEventOnOnSaisonChange(object sender, StaticData.Saison e) {
        SaisonCount++;
        if( SaisonCount>=_saisonsToSurvive) PlayWin(_winMessage);
    }
}