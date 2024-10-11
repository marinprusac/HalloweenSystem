using System.Collections.Generic;
using System.Linq;

namespace HalloweenSystem.GameLogic.Settings;

/// <summary>
/// Represents the game setting, including tag groups, rules, and rule activation order.
/// </summary>
/// <param name="tagGroups">The collection of tag groups associated with the setting.</param>
/// <param name="rules">The collection of rules associated with the setting.</param>
/// <param name="ruleActivationOrder">The order in which rules should be activated.</param>
public class Setting(IEnumerable<TagGroup> tagGroups, IEnumerable<Rule> rules, List<string> ruleActivationOrder)
{
	/// <summary>
	/// Gets the collection of tag groups associated with the setting.
	/// </summary>
	public readonly IEnumerable<TagGroup> TagGroups = tagGroups;

	/// <summary>
	/// Gets the collection of rules associated with the setting.
	/// </summary>
	public readonly IEnumerable<Rule> Rules = rules;

	/// <summary>
	/// Gets the collection of tags from all tag groups.
	/// </summary>
	public IEnumerable<string> Tags => TagGroups.SelectMany(tg => tg.Tags);

	/// <summary>
	/// Runs the setting with the specified player names, creating a context and evaluating rules in the specified order.
	/// </summary>
	/// <param name="playerNames">The collection of player names.</param>
	/// <returns>The context after evaluating the rules.</returns>
	public Context Run(IEnumerable<string> playerNames)
	{
		var players = playerNames.Select(playerName => new Player(playerName)).ToList();
		var context = new Context(this, players);
		ruleActivationOrder.ForEach(ruleName => Rules.FirstOrDefault(rule => rule.Name == ruleName)?.Evaluate(context));
		return context;
	}
}