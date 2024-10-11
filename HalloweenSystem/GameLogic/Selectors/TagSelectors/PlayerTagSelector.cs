using System.Collections.Generic;
using System.Linq;
using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Selectors.PlayerSelectors;
using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.Selectors.TagSelectors;

/// <summary>
/// Represents a selector that selects tags based on the players and a given tag selector.
/// </summary>
/// <param name="tagSelector">The selector that evaluates to a collection of tags.</param>
/// <param name="playerSelector">The optional selector that evaluates to a collection of players. If not provided, all players are considered.</param>
public class PlayerTagSelector(Selector<Tag> tagSelector, Selector<Player>? playerSelector=null) : Selector<Tag>
{
    private Selector<Player>? _playerSelector = playerSelector;

    /// <summary>
    /// Evaluates the selector in the given context and returns a collection of tags that match the players and the given tag selector.
    /// </summary>
    /// <param name="context">The context in which to evaluate the selector.</param>
    /// <returns>A collection of tags that match the players and the given tag selector.</returns>
    public override IEnumerable<Tag> Evaluate(Context context)
    {
        _playerSelector ??= new AllSelector<Player>();
        
        var players =  _playerSelector.Evaluate(context);
        
        var tags = tagSelector.Evaluate(context);
        var playerTags = players.Select(player => player.AssignedTags.AsEnumerable());
        var joinedTags = GameObject.Union<Tag>(playerTags).Cast<Tag>();

        var result = joinedTags.Where(jt => tags.Any(jt.Covers));
        return result.Cast<Tag>();
    }
}