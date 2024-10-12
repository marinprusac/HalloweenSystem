using System.Collections.Generic;
using System.Xml;
using HalloweenSystem.GameLogic.Utilities;

namespace HalloweenSystem.GameLogic.Settings;

/// <summary>
/// Represents a group of tags with a specific name.
/// </summary>
/// <param name="name">The name of the tag group.</param>
/// <param name="tags">The collection of tags associated with the group.</param>
public class TagGroup(string name, IEnumerable<string> tags) : IParser<TagGroup>
{
	/// <summary>
	/// Gets the name of the tag group.
	/// </summary>
	public string Name { get; } = name;

	/// <summary>
	/// Gets the collection of tags associated with the group.
	/// </summary>
	public readonly IEnumerable<string> Tags = tags;

	public static TagGroup Parse(XmlNode node)
	{
		if(node.Attributes?["name"] == null) throw new XmlException("Expected 'name' attribute.");
		var name = node.Attributes["name"]!.Value;
		if(node.HasChildNodes == false) throw new XmlException("Expected at least one tag.");
		var tagNodes = node.SelectNodes("tags/*")!.Cast<XmlNode>().ToList();
		if(tagNodes.Any(tn => tn.Attributes?["name"] == null)) throw new XmlException("Expected all tags to have a 'name' attribute.");
		var tags = tagNodes.Select(tn => tn.Attributes!["name"]!.Value);
		return new TagGroup(name, tags);
	}
}