using System.Linq;
using System.Xml;
using HalloweenSystem.GameLogic.Parsing;
using HalloweenSystem.GameLogic.Selectors;
using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Selectors.PlayerSelectors;
using HalloweenSystem.GameLogic.Selectors.TagSelectors;
using HalloweenSystem.GameLogic.Settings;
using HalloweenSystem.GameLogic.Utilities;

namespace HalloweenSystem.GameLogic.RuleActions;

/// <summary>
/// Represents an action that assigns tags to players based on specified selectors.
/// </summary>
/// <param name="assignTogether">Indicates whether to assign all tags to all players together.</param>
/// <param name="playerSelector">The selector that evaluates to a collection of players.</param>
/// <param name="tagSelector">The selector that evaluates to a collection of tags.</param>
public class AssignAction(bool assignTogether, ISelector<Player> playerSelector, ISelector<Tag> tagSelector)
	: IAction, IParser<AssignAction>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AssignAction"/> class with the specified player and tag selectors.
    /// </summary>
    /// <param name="playerSelector">The selector that evaluates to a collection of players.</param>
    /// <param name="tagSelector">The selector that evaluates to a collection of tags.</param>
	public AssignAction(ISelector<Player> playerSelector, ISelector<Tag> tagSelector) : this(false, playerSelector, tagSelector)
	{
	}
	
    /// <summary>
    /// Evaluates the action in the given context by assigning tags to players.
    /// </summary>
    /// <param name="context">The context in which to evaluate the action.</param>
	public void Evaluate(Context context)
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

	public static AssignAction Parse(XmlNode node)
	{
		var assignTogether = false;
		
		if(node.Attributes?["assignTogether"] != null)
		{
			assignTogether = bool.Parse(node.Attributes["assignTogether"]!.Value);
		}
		
		var playerSelectorNode = node.SelectSingleNode("players/*");
		var tagSelectorNode = node.SelectSingleNode("tags/*");
		
		if(playerSelectorNode == null) throw new XmlException("Expected player selector.");
		if(tagSelectorNode == null) throw new XmlException("Expected tag selector.");
		
		return new AssignAction(assignTogether,
			Parser.ParseSelector<Player>(playerSelectorNode),
			Parser.ParseSelector<Tag>(tagSelectorNode));
	}
}