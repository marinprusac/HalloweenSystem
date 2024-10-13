using System.Linq;
using System.Xml;
using HalloweenSystem.GameLogic.GameObjects;
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
public class AssignAction(bool assignTogether, ListSelector<Player> playerSelector, ListSelector<Tag> tagSelector)
	: IAction, IParser<AssignAction>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AssignAction"/> class with the specified player and tag selectors.
    /// </summary>
    /// <param name="playerSelector">The selector that evaluates to a collection of players.</param>
    /// <param name="tagSelector">The selector that evaluates to a collection of tags.</param>
	public AssignAction(ListSelector<Player> playerSelector, ListSelector<Tag> tagSelector) : this(false, playerSelector, tagSelector)
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
		
		if(node.Attributes?["together"] != null)
		{
			var togetherValue = node.Attributes["together"]!.Value.ToLower();
			assignTogether = togetherValue switch
			{
				"yes" => true,
				"no" => false,
				_ => throw new XmlException("Invalid value for 'together' attribute.")
			};
		}
		
		var playerSelectorNodes = node.SelectNodes("players/*");
		var tagSelectorNodes = node.SelectNodes("tags/*");
		
		if(playerSelectorNodes == null) throw new XmlException("Expected player selector.");
		if(tagSelectorNodes == null) throw new XmlException("Expected tag selector.");


		var playerSelectors = (from XmlNode selectorNode in playerSelectorNodes select Parser.ParseSelector<Player>(selectorNode)).ToList();
		var playerList = new ListSelector<Player>(playerSelectors);
		
		var tagSelectors = (from XmlNode selectorNode in tagSelectorNodes select Parser.ParseSelector<Tag>(selectorNode)).ToList();
		var tagList = new ListSelector<Tag>(tagSelectors);
		
		return new AssignAction(assignTogether, playerList, tagList);
	}
}