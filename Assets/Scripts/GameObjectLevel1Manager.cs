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

    protected override void OnDestroy()
    {
        StaticEvent.OnSaisonChange-= StaticEventOnOnSaisonChange;
        base.OnDestroy();
    }

    private void StaticEventOnOnSaisonChange(object sender, StaticData.Saison e) {
        SaisonCount++;
        Debug.Log("Count saison", this);
        if( SaisonCount>=_saisonsToSurvive) PlayWin(_winMessage);
    }
}