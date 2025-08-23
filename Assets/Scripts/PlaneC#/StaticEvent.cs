using System;

public static class StaticEvent
{
    public static event EventHandler<StaticData.GameStat> OnChangeGameStat; 
    public static event EventHandler OnDoGameTick;
    public static event EventHandler OnDoLateGameTick;
    public static event EventHandler OnTimeToTax;
    public static event EventHandler<StaticData.Saison> OnSaisonChange;
    public static event EventHandler<EndGameMessage> OnEndGame;
    public static event EventHandler<String> OnPlayDialogue; 

    public static void DoGameTick() {
        OnDoGameTick?.Invoke(null, EventArgs.Empty);
    }
    public static void DoLateGameTick() {
        OnDoLateGameTick?.Invoke(null, EventArgs.Empty);
    }
    public static void DoTimeToTax() {
        OnTimeToTax?.Invoke(null, EventArgs.Empty);
    }
    public static void DoSaisonChange(StaticData.Saison newSaison) {
        OnSaisonChange?.Invoke(null,newSaison);
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


}