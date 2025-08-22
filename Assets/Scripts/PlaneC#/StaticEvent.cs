using System;

public static class StaticEvent {
    public static event EventHandler OnDoGameTick;
    public static event EventHandler OnDoLateGameTick;

    public static void DoGameTick() {
        OnDoGameTick?.Invoke(null, EventArgs.Empty);
    }
    public static void DoLateGameTick() {
        OnDoLateGameTick?.Invoke(null, EventArgs.Empty);
    }
}