using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLogic.Utilities.Iterators;

namespace HalloweenSystem.GameLoop;

public struct Context(Setting setting, IEnumerable<Player> players)
{
	public Setting Setting { get;} = setting;
	public IEnumerable<Player> Players { get; } = players;
	public Player? CurrentPlayer { get; set; }
	public readonly List<string> IteratingOrder = [];
	public readonly Dictionary<string, GameObject> IteratingObjects = new();
	
	public T GetIteratingObject<T>(string name) where T : GameObject, new()
	{
		var obj = IteratingObjects[name];
		if (obj is not T t)
		{
			throw new ArgumentException($"Object {name} is not of type {typeof(T)}");
		}
		return t;
	}
	
	public override string ToString()
	{
		var ret = "Context {\n";
		foreach (var player in Players)
		{
			ret += player.ToString() + "\n";
		}
		
		ret += "}";

		return ret;

	}

}