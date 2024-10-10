namespace HalloweenSystem.GameLogic.Settings;

public class Setting(IEnumerable<TagGroup> tagGroups, IEnumerable<Rule> rules, List<string> ruleActivationOrder)
{
	public readonly IEnumerable<TagGroup> TagGroups = tagGroups;
	public readonly IEnumerable<Rule> Rules = rules;
	public IEnumerable<string> Tags => TagGroups.SelectMany(tg => tg.Tags);

	public Context Run(IEnumerable<string> playerNames)
	{
		var players = playerNames.Select(playerName => new Player(playerName)).ToList();
		var context = new Context(this, players);
		ruleActivationOrder.ForEach( ruleName => Rules.FirstOrDefault(rule => rule.Name == ruleName)?.Evaluate(context));
		return context;
	}
}