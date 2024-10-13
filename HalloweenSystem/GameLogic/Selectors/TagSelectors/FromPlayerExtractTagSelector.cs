using System.Collections.Generic;
using System.Linq;
using System.Xml;
using HalloweenSystem.GameLogic.GameObjects;
using HalloweenSystem.GameLogic.Parsing;
using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLogic.Utilities;

namespace HalloweenSystem.GameLogic.Selectors.TagSelectors;

/// <summary>
/// Represents a selector that extracts tags from players based on a specified tag type.
/// </summary>
/// <param name="tagType">The type of tag to extract from the players' assigned tags.</param>
/// <param name="playerSelector">The selector that evaluates to a collection of players.</param>
public class FromPlayerExtractTagSelector(string tagType, ISelector<Player> playerSelector)
	: ISelector<Tag>, IParser<FromPlayerExtractTagSelector>
{
	/// <summary>
	/// Evaluates the selector in the given context and returns a collection of tags extracted from the players.
	/// </summary>
	/// <param name="context">The context in which to evaluate the selector.</param>
	/// <returns>A collection of tags extracted from the players.</returns>
	public IEnumerable<Tag> Evaluate(Context context)
	{
		var players = playerSelector.Evaluate(context);
		var tags = players.Select(p => p.AssignedTags.AsEnumerable());
		var unionizedTags = GameObject.Union<Tag>(tags);
		var filtered = unionizedTags.Where(t => t.Name == tagType).Cast<Tag>();
		var extractedTypes = filtered.SelectMany(t => t.TagTypeParameters.AsEnumerable());
		var transformed = extractedTypes.Select(e => new Tag(e));
		var distinct = transformed.Distinct();
		return distinct;
	}

	public static FromPlayerExtractTagSelector Parse(XmlNode node)
	{
		var tagType = node.Attributes?["tag"]?.Value ?? throw new XmlException("Expected a tag attribute.");
		var playerSelectorNode = node.FirstChild;
		var playerSelector = playerSelectorNode == null
			? throw new XmlException("Expected a player selector.")
			: Parser.ParseSelector<Player>(playerSelectorNode);
		return new FromPlayerExtractTagSelector(tagType, playerSelector);
	}
}