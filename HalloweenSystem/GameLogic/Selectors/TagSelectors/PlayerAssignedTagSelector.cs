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
/// Represents a selector that selects tags based on the players and a given tag selector.
/// </summary>
/// <param name="tagSelector">The selector that evaluates to a collection of tags.</param>
/// <param name="playerSelector">The optional selector that evaluates to a collection of players. If not provided, all players are considered.</param>
public class PlayerAssignedTagSelector(ISelector<Tag> tagSelector, ISelector<Player>? playerSelector = null)
	: ISelector<Tag>, IParser<PlayerAssignedTagSelector>
{
	private ISelector<Player>? _playerSelector = playerSelector;

	/// <summary>
	/// Evaluates the selector in the given context and returns a collection of tags that match the players and the given tag selector.
	/// </summary>
	/// <param name="context">The context in which to evaluate the selector.</param>
	/// <returns>A collection of tags that match the players and the given tag selector.</returns>
	public IEnumerable<Tag> Evaluate(Context context)
	{
		_playerSelector ??= new AllSelector<Player>();

		var players = _playerSelector.Evaluate(context);

		var tags = tagSelector.Evaluate(context);
		var playerTags = players.Select(player => player.AssignedTags.AsEnumerable());
		var joinedTags = GameObject.Union<Tag>(playerTags).Cast<Tag>();

		var result = joinedTags.Where(jt => tags.Any(jt.Covers));
		return result.Cast<Tag>();
	}

	public static PlayerAssignedTagSelector Parse(XmlNode node)
	{
		var playerSelectorNode = node.SelectSingleNode("players/*");
		var tagSelectorNode = node.SelectSingleNode("tags/*");

		var playerSelector = playerSelectorNode == null ? null : Parser.ParseSelector<Player>(playerSelectorNode);
		var tagSelector = tagSelectorNode == null
			? throw new XmlException("Expected a tag selector.")
			: Parser.ParseSelector<Tag>(tagSelectorNode);

		return new PlayerAssignedTagSelector(tagSelector, playerSelector);
	}
}