using System.Collections.Generic;
using System.Linq;
using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.Selectors.PlayerSelectors;

/// <summary>
/// Represents a selector that removes the current player from the evaluated collection of players.
/// </summary>
/// <param name="playerSelector">The selector that evaluates to a collection of players.</param>
public class RemoveCurrentPlayerSelector(Selector<Player> playerSelector) : Selector<Player>
{
    /// <summary>
    /// Evaluates the selector in the given context and returns a collection of players excluding the current player.
    /// </summary>
    /// <param name="context">The context in which to evaluate the selector.</param>
    /// <returns>A collection of players excluding the current player.</returns>
    public override IEnumerable<Player> Evaluate(Context context)
    {
        var currentPLayer = context.CurrentPlayer;
        var players = playerSelector.Evaluate(context);
        var result = players.Where(player => player != currentPLayer);
        return result;
    }
}