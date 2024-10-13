
using System.Xml;
using HalloweenSystem.GameLogic.GameObjects;
using HalloweenSystem.GameLogic.Utilities;

namespace HalloweenSystem.GameLogic.Settings;

/// <summary>
/// Represents the game setting, including tag groups, rules, and rule activation order.
/// </summary>
/// <param name="tagGroups">The collection of tag groups associated with the setting.</param>
/// <param name="ruleNames">The collection of rules associated with the setting.</param>
/// <param name="ruleActivationOrder">The order in which rules should be activated.</param>
public class Setting(IEnumerable<TagGroup> tagGroups, IEnumerable<string> ruleNames, List<string> ruleActivationOrder) : IParser<Setting>
{
	/// <summary>
	/// Gets the collection of tag groups associated with the setting.
	/// </summary>
	public readonly IEnumerable<TagGroup> TagGroups = tagGroups;

	public readonly IEnumerable<string> RuleNames = ruleNames;
	
	/// <summary>
	/// Gets the collection of rules associated with the setting.
	/// </summary>
	public IEnumerable<Rule> Rules { get; private set; } = new List<Rule>();
	
	
	

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
	
	public Context RunWithTags(Dictionary<string, List<string>> playerTags)
	{
		
		var players = playerTags.Select(pt => new Player(pt.Key, pt.Value.Select( tagName => new Tag(tagName)))).ToList();
		var context = new Context(this, players);
		ruleActivationOrder.ForEach(ruleName => Rules.FirstOrDefault(rule => rule.Name == ruleName)?.Evaluate(context));
		return context;
	}

	public void LoadRules(XmlNode[] ruleNodes)
	{
		var rules = ruleNodes.Select(Rule.Parse).ToList();
		Rules = rules;
	}

	public static Setting Parse(XmlNode node)
	{
		
		var tagGroupsNode = node.SelectSingleNode("tag_groups");
		if(tagGroupsNode == null) throw new XmlException("Expected 'tagGroups' node.");
		if(tagGroupsNode.HasChildNodes == false) throw new XmlException("Expected at least one tag group.");
		var tagGroups = tagGroupsNode.SelectNodes("tag_group")!.Cast<XmlNode>();
		var tagGroupsList = tagGroups.Select(TagGroup.Parse).ToList();
		
		var rulesNode = node.SelectSingleNode("rules");
		if(rulesNode == null) throw new XmlException("Expected 'rules' node.");
		if(rulesNode.HasChildNodes == false) throw new XmlException("Expected at least one rule.");
		
		
		
		var rules = rulesNode.SelectNodes("rule")!.Cast<XmlNode>().ToList();
		if(rules.Any(r => r.Attributes?["name"] == null)) throw new XmlException("Expected all rules to have a 'name' attribute.");
		var ruleNames = rules.Select(r => r.Attributes?["name"]?.Value!).ToList();
		
		
		
		var ruleActivationOrderNode = node.SelectSingleNode("order");
		if(ruleActivationOrderNode == null) throw new XmlException("Expected 'ruleActivationOrder' node.");
		if(ruleActivationOrderNode.HasChildNodes == false) throw new XmlException("Expected at least one rule name.");
		var ruleActivationOrderNodes = ruleActivationOrderNode.SelectNodes("rule")!.Cast<XmlNode>().ToList();
		if(ruleActivationOrderNodes.Any(ran => ran.Attributes?["name"] == null)) throw new XmlException("Expected all rule names to have a 'name' attribute.");
		var ruleActivationOrder = ruleActivationOrderNodes.Select(ran => ran.Attributes!["name"]!.Value).ToList();
		
		return new Setting(tagGroupsList, ruleNames, ruleActivationOrder);
	}
}