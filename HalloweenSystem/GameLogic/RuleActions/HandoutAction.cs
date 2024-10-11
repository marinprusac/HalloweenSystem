using HalloweenSystem.GameLogic.GameObjects;
using HalloweenSystem.GameLogic.Selectors;
using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Selectors.PlayerSelectors;
using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.RuleActions;

/// <summary>
/// Represents an action that hands out items to players based on specified selectors.
/// </summary>
/// <param name="handoutTogether">Indicates whether to hand out all items to all players together.</param>
/// <param name="playerSelector">The selector that evaluates to a collection of players.</param>
/// <param name="handoutSelector">The selector that evaluates to a collection of handouts.</param>
public class HandoutAction(bool handoutTogether, Selector<Player> playerSelector, Selector<Handout> handoutSelector)
	: RuleAction
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HandoutAction"/> class with the specified player and handout selectors.
    /// </summary>
    /// <param name="playerSelector">The selector that evaluates to a collection of players.</param>
    /// <param name="handoutSelector">The selector that evaluates to a collection of handouts.</param>
	public HandoutAction(Selector<Player> playerSelector, Selector<Handout> handoutSelector) : this(false, playerSelector, handoutSelector)
	{
	}

    /// <summary>
    /// Evaluates the action in the given context by handing out items to players.
    /// </summary>
    /// <param name="context">The context in which to evaluate the action.</param>
	public override void Evaluate(Context context)
	{
		context.CurrentPlayer = null;
		var players = playerSelector.Evaluate(context);

		if (handoutTogether)
		{
            // Hand out all items to all players together
			var handouts = handoutSelector.Evaluate(context).ToList();
			foreach (var player in players)
			{
				player.Handouts.AddRange(handouts);
			}
		}
		else
		{
            // Hand out items to each player individually
			foreach (var player in players)
			{
				context.CurrentPlayer = player;
				var handouts = handoutSelector.Evaluate(context).ToList();
				player.Handouts.AddRange(handouts);
			}
		}
	}
}