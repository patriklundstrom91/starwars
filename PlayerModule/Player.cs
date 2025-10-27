namespace PlayerModule;

public class Player
{
    private static int _nextId = 1;
    public int PlayerId { get; private set; }
    public string Name { get; private set; }
    public int Score { get; private set; }
    private Player()
    {
        PlayerId = _nextId++;
        Name = $"Player {PlayerId}";
        Score = 0;
    }
    private Player(string name)
    {
        PlayerId = _nextId++;
        Name = name;
        Score = 0;
    }
    public void SetScore(int score)
    {
        Score = score;
    }

    public static class PlayerFactory
    {
        public static Player CreatePlayer()
        {
            return new Player();
        }
        public static Player CreatePlayer(string name)
        {
            return new Player(name);
        }

    }
}
