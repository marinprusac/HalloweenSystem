using HalloweenSystem.GameLoop;

namespace HalloweenSystem.GameLogic.Settings;

public class TagGroup(string name, IEnumerable<TagType> tags)
{
	public string Name { get; init; } = name;
	public IEnumerable<TagType> Tags = tags;
}