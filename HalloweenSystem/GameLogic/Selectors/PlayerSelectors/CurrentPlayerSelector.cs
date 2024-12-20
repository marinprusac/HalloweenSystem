using System.Collections.Generic;
using System.Xml;
using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLogic.Utilities;

namespace HalloweenSystem.GameLogic.Selectors.PlayerSelectors;

/// <summary>
/// Represents a selector that selects the current player from the context.
/// </summary>
public class CurrentPlayerSelector : ISelector<Player>, IParser<CurrentPlayerSelector>
{
	/// <summary>
	/// Evaluates the selector in the given context and returns a collection containing the current player.
	/// </summary>
	/// <param name="context">The context in which to evaluate the selector.</param>
	/// <returns>A collection containing the current player if one exists; otherwise, an empty collection.</returns>
	public IEnumerable<Player> Evaluate(Context context)
	{
		return context.CurrentPlayer == null ? [] : [context.CurrentPlayer];
	}

	public static CurrentPlayerSelector Parse(XmlNode node)
	{
		return new CurrentPlayerSelector();
	}
}