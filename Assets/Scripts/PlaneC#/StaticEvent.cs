using System;
using System.Net.NetworkInformation;

public static class StaticEvent
{
    public static event EventHandler<StaticData.GameStat> OnChangeGameStat;
    public static event EventHandler OnDoGameTick;
    public static event EventHandler OnDoLateGameTick;
    public static event EventHandler OnDoVeryLateGameTick;
    public static event EventHandler OnTimeToTax;
    public static event EventHandler<StaticData.Saison> OnSaisonChange;
    public static event EventHandler<EndGameMessage> OnEndGame;
    public static event EventHandler<String> OnPlayDialogue;
    public static event EventHandler<StaticData.MerchantStat> OnOpenMarchent;
    public static event EventHandler<StructCueInformation> OnPlayCue; 

    public static void DoGameTick() {
        OnDoGameTick?.Invoke(null, EventArgs.Empty);
    }
    public static void DoLateGameTick() {
        OnDoLateGameTick?.Invoke(null, EventArgs.Empty);
    }
    public static void DoVeryLateGameTick()
    {
        OnDoVeryLateGameTick?.Invoke(null, EventArgs.Empty);
    }

    public static void DoTimeToTax() {
        OnTimeToTax?.Invoke(null, EventArgs.Empty);
    }
    public static void DoSaisonChange(StaticData.Saison newSaison) {
        OnSaisonChange?.Invoke(null, newSaison);
    }
    public static void DoChangeGameStat(StaticData.GameStat newStat) {
        OnChangeGameStat?.Invoke(null, newStat);
    }
    public static void DoEndGame(EndGameMessage message) {
        OnEndGame?.Invoke(null, message);
    }

    public static void DoDialogue(string dialogue) {
        OnPlayDialogue?.Invoke(null, dialogue);
    }

    public static void DoOpenMerchant(StaticData.MerchantStat data) => OnOpenMarchent?.Invoke(null, data);
    public static void DoPlayCue(StructCueInformation cue) => OnPlayCue?.Invoke(null, cue);
    public static void DoOpenMerchant( WorkingBuilding targetMerchant) => OnOpenMarchent?.Invoke(null,  targetMerchant );


}