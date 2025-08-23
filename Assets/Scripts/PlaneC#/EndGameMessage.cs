
public struct EndGameMessage
{
    public bool IsWin;
    public string TxtEndGameMessage;

    public EndGameMessage(bool isWin, string txtEndGameMessage) {
        IsWin = isWin;
        TxtEndGameMessage = txtEndGameMessage;
    }
}
