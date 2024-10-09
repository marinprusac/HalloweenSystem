namespace HalloweenSystem.GameLoop;

public class Context(IEnumerable<Player> players)
{
	public IEnumerable<Player> Players { get; } = players;

}