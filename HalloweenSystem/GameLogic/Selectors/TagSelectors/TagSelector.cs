using System.Collections.Generic;
using System.Linq;
using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Selectors.PlayerSelectors;
using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.Selectors.TagSelectors;

/// <summary>
/// Represents a selector that selects tags based on a specified type, player selector, and tag selector.
/// </summary>
/// <param name="type">The type of tag to be selected.</param>
/// <param name="playerSelector">The optional selector that evaluates to a collection of players. If not provided, an empty selector is used.</param>
/// <param name="tagSelector">The optional selector that evaluates to a collection of tags. If not provided, an empty selector is used.</param>
public class TagSelector(string type, Selector<Player>? playerSelector = null, Selector<Tag>? tagSelector = null) : Selector<Tag>
{
    private readonly Selector<Player> _playerSelector = playerSelector ?? new EmptySelector<Player>();
    private readonly Selector<Tag> _tagSelector = tagSelector ?? new EmptySelector<Tag>();

    /// <summary>
    /// Evaluates the selector in the given context and returns a collection of tags based on the specified type, player selector, and tag selector.
    /// </summary>
    /// <param name="context">The context in which to evaluate the selector.</param>
    /// <returns>A collection of tags based on the specified type, player selector, and tag selector.</returns>
    public override IEnumerable<Tag> Evaluate(Context context)
    {
        var players = _playerSelector.Evaluate(context);
        var tags = _tagSelector.Evaluate(context);
        var types = tags.Select(t => t.Name);

        return [new Tag(type, players, types)];
    }
}