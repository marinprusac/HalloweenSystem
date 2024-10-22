namespace HalloweenSystem.GameLogic.Settings;

/// <summary>
/// Represents the context in which the game logic operates.
/// </summary>
/// <param name="setting">The game setting.</param>
/// <param name="players">The collection of players in the game.</param>
public class Context(Setting setting, IEnumerable<Player> players)
{
	/// <summary>
	/// Gets the game setting.
	/// </summary>
	public Setting Setting { get; } = setting;

	/// <summary>
	/// Gets the collection of players in the game.
	/// </summary>
	public IEnumerable<Player> Players { get; } = players;

	/// <summary>
	/// Gets or sets the current player.
	/// </summary>
	public Player? CurrentPlayer { get; set; }

	/// <summary>
	/// A dictionary of parameters associated with the context.
	/// </summary>
	public readonly Dictionary<string, GameObject> Parameters = new();

	/// <summary>
	/// Retrieves a parameter by name and casts it to the specified type.
	/// </summary>
	/// <typeparam name="T">The type to cast the parameter to.</typeparam>
	/// <param name="name">The name of the parameter.</param>
	/// <returns>The parameter cast to the specified type.</returns>
	/// <exception cref="ArgumentException">Thrown if the parameter is not of the specified type.</exception>
	public T GetParameter<T>(string name) where T : GameObject, new()
	{
		var obj = Parameters[name];
		if (obj is not T t)
		{
			throw new ArgumentException($"Object {name} is not of type {typeof(T)}");
		}
		return t;
	}

	/// <summary>
	/// Returns a string representation of the context.
	/// </summary>
	/// <returns>A string representation of the context.</returns>
	public override string ToString()
	{
		var ret = Players.Aggregate("\n", (current, player) => current + (player + "\n----------------------------------------------\n"));


		return ret;
	}
}