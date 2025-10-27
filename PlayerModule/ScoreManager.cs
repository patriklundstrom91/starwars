namespace PlayerModule;

public class ScoreManager
{
    public static void AddScore(Player player, int score)
    {
        player.SetScore(player.Score + score);
    }
    public static int GetScore(Player player) => player.Score;
    public static IEnumerable<Player> TopScore(IEnumerable<Player> players)
        => players.OrderByDescending(p => p.Score);

    public static IEnumerable<Player> PointsOverHundred(IEnumerable<Player> players)
        => players.Where(p => p.Score > 100);
}