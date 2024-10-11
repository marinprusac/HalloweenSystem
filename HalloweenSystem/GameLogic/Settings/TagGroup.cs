using System.Collections.Generic;

namespace HalloweenSystem.GameLogic.Settings;

/// <summary>
/// Represents a group of tags with a specific name.
/// </summary>
/// <param name="name">The name of the tag group.</param>
/// <param name="tags">The collection of tags associated with the group.</param>
public class TagGroup(string name, IEnumerable<string> tags)
{
	/// <summary>
	/// Gets the name of the tag group.
	/// </summary>
	public string Name { get; } = name;

	/// <summary>
	/// Gets the collection of tags associated with the group.
	/// </summary>
	public readonly IEnumerable<string> Tags = tags;
}