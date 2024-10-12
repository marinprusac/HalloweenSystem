using System.Collections.Generic;
using System.Linq;
using System.Xml;
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
public class HasTagsPlayerSelector(ISelector<Tag> tagSelector, ISelector<Player>? playerSelector = null)
	: ISelector<Player>, IParser<HasTagsPlayerSelector>
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
		
		var players = _playerSelector?.Evaluate(context) ?? [];
		var tags = tagSelector.Evaluate(context) ?? [];
		
		return players.Where(player => tags.All(tag => player.GetQueriedTag(tag, out _)));
	}

	public static HasTagsPlayerSelector Parse(XmlNode node)
	{
		throw new NotImplementedException();
	}
}