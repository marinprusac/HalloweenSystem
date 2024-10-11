using System.Collections.Generic;
using System.Linq;
using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.Selectors.PlayerSelectors;

/// <summary>
/// Represents a selector that selects players based on the presence of a specific tag type.
/// </summary>
/// <param name="type">The type of tag to match in the players' assigned tags.</param>
/// <param name="playerSelector">The optional selector that evaluates to a collection of players. If not provided, all players are considered.</param>
public class HasTagTypePlayerSelector(string type, Selector<Player>? playerSelector=null) : Selector<Player>
{
    private Selector<Player>? _playerSelector = playerSelector;

    /// <summary>
    /// Evaluates the selector in the given context and returns a collection of players that have the specified tag type.
    /// </summary>
    /// <param name="context">The context in which to evaluate the selector.</param>
    /// <returns>A collection of players that have the specified tag type.</returns>
    public override IEnumerable<Player> Evaluate(Context context)
    {
        _playerSelector ??= new AllSelector<Player>();
        
        var players = _playerSelector.Evaluate(context);
        var filtered = players.Where(p => p.AssignedTags.Any(t => t.Name == type));
        return filtered;
    }
}