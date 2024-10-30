using System.Xml;
using HalloweenSystem.GameLogic.GameObjects;
using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLogic.Utilities;

namespace HalloweenSystem.GameLogic.Selectors.PlayerSelectors;

/// <summary>
/// Represents a selector that extracts tags from players based on a specified tag type.
/// </summary>
/// <param name="tagType">The type of tag to extract from the players' assigned tags.</param>
/// <param name="playerSelector">The selector that evaluates to a collection of players.</param>
public class FromPlayerExtractPlayerSelector(string tagType, ISelector<Player>? playerSelector)
	: ISelector<Player>, IParser<FromPlayerExtractPlayerSelector>
{
	/// <summary>
	/// Evaluates the selector in the given context and returns a collection of tags extracted from the players.
	/// </summary>
	/// <param name="context">The context in which to evaluate the selector.</param>
	/// <returns>A collection of tags extracted from the players.</returns>
	public IEnumerable<Player> Evaluate(Context context)
	{
		var players = playerSelector!=null ? playerSelector.Evaluate(context) : new AllSelector<Player>().Evaluate(context);
		var tags = players.Select(p => p.AssignedTags.AsEnumerable());
		var unionizedTags = GameObject.Union<Tag>(tags);
		var filtered = unionizedTags.Where(t => t.Name == tagType).Cast<Tag>();
		var extractedPlayers = filtered.SelectMany(t => t.playerParameters.AsEnumerable());
		var distinct = extractedPlayers.Distinct();
		return distinct;
	}

	public static FromPlayerExtractPlayerSelector Parse(XmlNode node)
	{
		var tagType = node.Attributes?["tag"]?.Value ?? throw new XmlException("Expected a tag attribute.");
		
		var playerSelector = node.HasChildNodes ? ListSelector<Player>.Parse(node) : null;
		return new FromPlayerExtractPlayerSelector(tagType, playerSelector);
	}
}