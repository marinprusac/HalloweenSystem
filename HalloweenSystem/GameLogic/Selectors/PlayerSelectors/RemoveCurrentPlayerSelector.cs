using System.Collections.Generic;
using System.Linq;
using System.Xml;
using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLogic.Utilities;

namespace HalloweenSystem.GameLogic.Selectors.PlayerSelectors;

/// <summary>
/// Represents a selector that removes the current player from the evaluated collection of players.
/// </summary>
/// <param name="playerSelector">The selector that evaluates to a collection of players.</param>
public class RemoveCurrentPlayerSelector(ISelector<Player> playerSelector) : ISelector<Player>, IParser<RemoveCurrentPlayerSelector>
{
    /// <summary>
    /// Evaluates the selector in the given context and returns a collection of players excluding the current player.
    /// </summary>
    /// <param name="context">The context in which to evaluate the selector.</param>
    /// <returns>A collection of players excluding the current player.</returns>
    public IEnumerable<Player> Evaluate(Context context)
    {
        var currentPLayer = context.CurrentPlayer;
        var players = playerSelector.Evaluate(context);
        var result = players.Where(player => player != currentPLayer);
        return result;
    }

    public static RemoveCurrentPlayerSelector Parse(XmlNode node)
    {
        throw new NotImplementedException();
    }
}