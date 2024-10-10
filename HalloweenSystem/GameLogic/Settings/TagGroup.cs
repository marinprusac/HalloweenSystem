namespace HalloweenSystem.GameLogic.Settings;

public class TagGroup(string name, IEnumerable<string> tags)
{
	public string Name { get; init; } = name;
	public readonly IEnumerable<string> Tags = tags;
}