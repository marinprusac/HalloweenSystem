using System.Collections.Generic;
using System.Linq;
using System.Xml;
using HalloweenSystem.GameLogic.GameObjects;
using HalloweenSystem.GameLogic.Parsing;
using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Selectors.TagSelectors;
using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLogic.Utilities;

namespace HalloweenSystem.GameLogic.Selectors.PlayerSelectors;

/// <summary>
/// Represents a selector that selects players based on the presence of specific tags.
/// </summary>
/// <param name="tagSelector">The selector that evaluates to a collection of tags.</param>
/// <param name="playerSelector">The optional selector that evaluates to a collection of players. If not provided, all players are considered.</param>
public class HasTagGroupPlayerSelector(string groupName, ISelector<Player>? playerSelector = null)
	: ISelector<Player>, IParser<HasTagGroupPlayerSelector>
{
	private ISelector<Player>? _playerSelector = playerSelector;

	/// <summary>
	/// Evaluates the selector in the given context and returns a collection of players that have all the specified tags.
	/// </summary>
	/// <param name="context">The context in which to evaluate the selector.</param>
	/// <returns>A collection of players that have all the specified tags.</returns>
	public IEnumerable<Player> Evaluate(Context context)
	{
		_playerSelector ??= new AllSelector<Player>();

		var players = _playerSelector?.Evaluate(context) ?? new AllSelector<Player>().Evaluate(context);
		var tagGroup = context.Setting.TagGroups.Single(tg => tg.Name == groupName);
		
		var acceptedPlayers = players.Where(p => p.AssignedTags.Any(t => tagGroup.Tags.Contains(t.Name)));
		return acceptedPlayers;
	}

	public static HasTagGroupPlayerSelector Parse(XmlNode node)
	{

		var group = node.Attributes?["name"]?.Value ?? throw new XmlException("Expected a name attribute.");
		var playerSelector = node.HasChildNodes ? ListSelector<Player>.Parse(node) : null;

		return new HasTagGroupPlayerSelector(group, playerSelector);
	}
}