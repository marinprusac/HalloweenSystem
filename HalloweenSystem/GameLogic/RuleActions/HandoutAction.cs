using System.Xml;
using HalloweenSystem.GameLogic.GameObjects;
using HalloweenSystem.GameLogic.Parsing;
using HalloweenSystem.GameLogic.Selectors;
using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Selectors.PlayerSelectors;
using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLogic.Utilities;

namespace HalloweenSystem.GameLogic.RuleActions;

/// <summary>
/// Represents an action that hands out items to players based on specified selectors.
/// </summary>
/// <param name="handoutTogether">Indicates whether to hand out all items to all players together.</param>
/// <param name="playerSelector">The selector that evaluates to a collection of players.</param>
/// <param name="handoutSelector">The selector that evaluates to a collection of handouts.</param>
public class HandoutAction(bool handoutTogether, ListSelector<Player> playerSelector, ListSelector<Handout> handoutSelector)
	: IAction, IParser<HandoutAction>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="HandoutAction"/> class with the specified player and handout selectors.
    /// </summary>
    /// <param name="playerSelector">The selector that evaluates to a collection of players.</param>
    /// <param name="handoutSelector">The selector that evaluates to a collection of handouts.</param>
	public HandoutAction(ListSelector<Player> playerSelector, ListSelector<Handout> handoutSelector) : this(false, playerSelector, handoutSelector)
	{
	}

    /// <summary>
    /// Evaluates the action in the given context by handing out items to players.
    /// </summary>
    /// <param name="context">The context in which to evaluate the action.</param>
	public void Evaluate(Context context)
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

	public static HandoutAction Parse(XmlNode node)
	{
		var handoutTogether = node.Attributes?["together"]?.Value == "true";
		
		var playerSelectorNode = node.SelectSingleNode("players");
		var handoutSelectorNode = node.SelectSingleNode("handouts");
		
		if (playerSelectorNode == null) throw new XmlException("Expected a players element.");
		if (handoutSelectorNode == null) throw new XmlException("Expected a handouts element.");
		
		var playerList = ListSelector<Player>.Parse(playerSelectorNode);
		var handoutList = ListSelector<Handout>.Parse(handoutSelectorNode);
		
		return new HandoutAction(handoutTogether, playerList, handoutList);
	}
}