using System.Xml;
using HalloweenSystem.GameLogic.GameObjects;
using HalloweenSystem.GameLogic.RuleActions;
using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Selectors.HandoutSelectors;
using HalloweenSystem.GameLogic.Selectors.PlayerSelectors;
using HalloweenSystem.GameLogic.Selectors.RuleSelectors;
using HalloweenSystem.GameLogic.Selectors.TagSelectors;
using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.Parsing;

public static class Parser
{
	private static List<string> GetRuleFiles(string ruleDirPath)
	{
		var ruleFiles = Directory.GetFiles(ruleDirPath).ToList();

		foreach (var d in Directory.GetDirectories(ruleDirPath))
		{
			var newRules = GetRuleFiles(d);
			ruleFiles.AddRange(newRules);
		}

		return ruleFiles;
	}

	public static Setting LoadGame(string path)
	{
		var settingPath = path + "/setting.xml";
		var rulesPath = path + "/rules";

		var settingDoc = new XmlDocument();
		settingDoc.Load(settingPath);

		if (settingDoc.DocumentElement == null) throw new XmlException("Invalid setting file.");

		var setting = Setting.Parse(settingDoc.DocumentElement);

		var ruleFiles = GetRuleFiles(rulesPath);


		var ruleNodes = ruleFiles.Select(file =>
		{
			var doc = new XmlDocument();
			doc.Load(file);
			return doc;
		}).ToList();

		var ruleDocuments = ruleNodes.Where(doc => doc.DocumentElement != null);
		var validRules = ruleDocuments.Select(XmlNode (doc) => doc.DocumentElement!)
			.Where(node => node.Attributes?["name"] != null)
			.ToArray();

		if (ruleNodes.Count != validRules.Length) throw new XmlException("Invalid rule file.");

		setting.LoadRules(validRules);

		return setting;
	}

	public static ISelector<T> ParseSelector<T>(XmlNode node) where T : GameObject, new()
	{
		return node.Name switch
		{
			"all_if_any" => AllIfAnySelector<T>.Parse(node),
			"all" => AllSelector<T>.Parse(node),
			"chance" => ChanceSelector<T>.Parse(node),
			"complement" => ComplementSelector<T>.Parse(node),
			"empty" => EmptySelector<T>.Parse(node),
			"intersect" => IntersectSelector<T>.Parse(node),
			"iterate" => ParseIteratingSelector<T>(node),
			"list" => ListSelector<T>.Parse(node),
			"parameter" => ParameterSelector<T>.Parse(node),
			"random" => RandomSelector<T>.Parse(node),
			"shuffle" => ShuffleSelector<T>.Parse(node),
			"union" => UnionSelector<T>.Parse(node),
			"join" => (ISelector<T>)JoinHandoutSelector.Parse(node),
			"text" => (ISelector<T>)TextHandoutSelector.Parse(node),
			"transform" => ParseTransformSelector<T>(node),
			"current_player" => (ISelector<T>)CurrentPlayerSelector.Parse(node),
			"from_player_extract_player" => (ISelector<T>)FromPlayerExtractPlayerSelector.Parse(node),
			"from_tag_extract_player" => (ISelector<T>)FromTagExtractPlayerSelector.Parse(node),
			"has_group" => (ISelector<T>)HasTagGroupPlayerSelector.Parse(node),
			"has_any_tag" => (ISelector<T>)HasAnyTagPlayerSelector.Parse(node),
			"has_tag" => (ISelector<T>)HasTagPlayerSelector.Parse(node),
			"has_type" => (ISelector<T>)HasTagTypePlayerSelector.Parse(node),
			"remove_current_player" => (ISelector<T>)RemoveCurrentPlayerSelector.Parse(node),
			"rule" => (ISelector<T>)RuleSelector.Parse(node),
			"from_player_extract_tag" => (ISelector<T>)FromPlayerExtractTagSelector.Parse(node),
			"from_tag_extract_tag" => (ISelector<T>)FromTagExtractTagSelector.Parse(node),
			"group_tag" => (ISelector<T>)GroupTagSelector.Parse(node),
			"player_assigned_tag" => (ISelector<T>)PlayerAssignedTagSelector.Parse(node),
			"tag" => (ISelector<T>)TagSelector.Parse(node),
			"type_filter" => (ISelector<T>)TypeFilterTagSelector.Parse(node),
			"used_tag" => (ISelector<T>)UsedTagSelector.Parse(node),

			_ => throw new XmlException($"Unknown selector: {node.Name}")
		};
	}

	public static IAction ParseAction(XmlNode node)
	{
		return node.Name switch
		{
			"assign" => AssignAction.Parse(node),
			"activate" => ActivateAction.Parse(node),
			"handout" => HandoutAction.Parse(node),
			_ => throw new XmlException($"Unknown action: {node.Name}")
		};
	}

	private static ISelector<T> ParseIteratingSelector<T>(XmlNode node) where T : GameObject, new()
	{
		if (node.Attributes?["type"] == null) throw new XmlException("Expected 'type' attribute.");
		var parameterType = node.Attributes["type"]!.Value;
		return parameterType switch
		{
			"player" => IteratingSelector<Player, T>.Parse(node),
			"tag" => IteratingSelector<Tag, T>.Parse(node),
			"rule" => IteratingSelector<Rule, T>.Parse(node),
			"handout" => IteratingSelector<Handout, T>.Parse(node),
			_ => throw new XmlException($"Unknown parameter type: {parameterType}")
		};
	}

	private static ISelector<T> ParseTransformSelector<T>(XmlNode node) where T : GameObject, new()
	{
		if (node.Attributes?["type"] == null) throw new XmlException("Expected 'type' attribute.");
		var parameterType = node.Attributes["type"]!.Value;
		return parameterType switch
		{
			"player" => (ISelector<T>)TransformHandoutSelector<Player>.Parse(node),
			"tag" => (ISelector<T>)TransformHandoutSelector<Tag>.Parse(node),
			"rule" => (ISelector<T>)TransformHandoutSelector<Rule>.Parse(node),
			"handout" => (ISelector<T>)TransformHandoutSelector<Handout>.Parse(node),
			_ => throw new XmlException($"Unknown parameter type: {parameterType}")
		};
	}
}