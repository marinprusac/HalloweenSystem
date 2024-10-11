using System.Linq;
using HalloweenSystem.GameLogic.Selectors;
using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Selectors.PlayerSelectors;
using HalloweenSystem.GameLogic.Selectors.TagSelectors;
using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.RuleActions;

/// <summary>
/// Represents an action that assigns tags to players based on specified selectors.
/// </summary>
/// <param name="assignTogether">Indicates whether to assign all tags to all players together.</param>
/// <param name="playerSelector">The selector that evaluates to a collection of players.</param>
/// <param name="tagSelector">The selector that evaluates to a collection of tags.</param>
public class AssignAction(bool assignTogether, Selector<Player> playerSelector, Selector<Tag> tagSelector)
	: RuleAction
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AssignAction"/> class with the specified player and tag selectors.
    /// </summary>
    /// <param name="playerSelector">The selector that evaluates to a collection of players.</param>
    /// <param name="tagSelector">The selector that evaluates to a collection of tags.</param>
	public AssignAction(Selector<Player> playerSelector, Selector<Tag> tagSelector) : this(false, playerSelector, tagSelector)
	{
	}
	
    /// <summary>
    /// Evaluates the action in the given context by assigning tags to players.
    /// </summary>
    /// <param name="context">The context in which to evaluate the action.</param>
	public override void Evaluate(Context context)
	{
		var players = playerSelector.Evaluate(context);

		if (assignTogether)
		{
            // Assign all tags to all players together
			var tags = tagSelector.Evaluate(context).ToList();
			foreach (var player in players)
			{
				player.AssignTags(tags);
			}
		}
		else
		{
            // Assign tags to each player individually
			foreach (var player in players)
			{
				context.CurrentPlayer = player;
				var tags = tagSelector.Evaluate(context);
				player.AssignTags(tags);
			}
		}
	}
}