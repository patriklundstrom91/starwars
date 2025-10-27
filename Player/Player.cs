namespace Player;

public class Player : IPlayer
{
    public string Name { get; private set; }
    public int Score { get; private set; }
    private Player(string name)
    {
        Name = name;
        Score = 0;
    }

    public abstract class PlayerFactory
    {
        public abstract IPlayer CreatePlayer(string name);

    }
}
