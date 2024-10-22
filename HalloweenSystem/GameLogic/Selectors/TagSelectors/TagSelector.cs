using System.Collections.Generic;
using System.Linq;
using System.Xml;
using HalloweenSystem.GameLogic.GameObjects;
using HalloweenSystem.GameLogic.Parsing;
using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Selectors.PlayerSelectors;
using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLogic.Utilities;

namespace HalloweenSystem.GameLogic.Selectors.TagSelectors;

/// <summary>
/// Represents a selector that selects tags based on a specified type, player selector, and tag selector.
/// </summary>
/// <param name="type">The type of tag to be selected.</param>
/// <param name="playerSelector">The optional selector that evaluates to a collection of players. If not provided, an empty selector is used.</param>
/// <param name="tagSelector">The optional selector that evaluates to a collection of tags. If not provided, an empty selector is used.</param>
public class TagSelector(string type, ISelector<Player>? playerSelector = null, ISelector<Tag>? tagSelector = null)
	: ISelector<Tag>, IParser<TagSelector>
{
	private readonly ISelector<Player> _playerSelector = playerSelector ?? new EmptySelector<Player>();
	private readonly ISelector<Tag> _tagSelector = tagSelector ?? new EmptySelector<Tag>();

	/// <summary>
	/// Evaluates the selector in the given context and returns a collection of tags based on the specified type, player selector, and tag selector.
	/// </summary>
	/// <param name="context">The context in which to evaluate the selector.</param>
	/// <returns>A collection of tags based on the specified type, player selector, and tag selector.</returns>
	public IEnumerable<Tag> Evaluate(Context context)
	{
		var players = _playerSelector.Evaluate(context);
		var tags = _tagSelector.Evaluate(context);
		var types = tags.Select(t => t.Name);

		return [new Tag(type, players, types)];
	}

	public static TagSelector Parse(XmlNode node)
	{
		if(node.Attributes?["name"] == null) throw new XmlException("Expected 'name' attribute.");
		var type = node.Attributes["name"]!.Value;
		
		var playerSelectorNode = node.SelectSingleNode("players");
		var tagSelectorNode = node.SelectSingleNode("tags");
		
		var playerSelector = playerSelectorNode != null
			? ListSelector<Player>.Parse(playerSelectorNode)
			: null;
		
		var tagSelector = tagSelectorNode != null 
			? ListSelector<Tag>.Parse(tagSelectorNode)
			: null;
		
		return new TagSelector(type, playerSelector, tagSelector);

	}
}