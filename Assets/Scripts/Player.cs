public class Player
{
    public string playerName;
    public string playerPlayfabId;
    public int score;
    public int coin;

    public Player(string playerName, string playerPlayfabId, int score, int coin)
    {
        this.playerName = playerName;
        this.playerPlayfabId = playerPlayfabId;
        this.score = score;
        this.coin = coin;
    }

    public string PlayerName { get { return playerName; } }
    public string PlayerPlayfabId { get { return playerPlayfabId; } }
    public int Score { get { return score; } }
    public int Coin { get { return coin; } }
}
