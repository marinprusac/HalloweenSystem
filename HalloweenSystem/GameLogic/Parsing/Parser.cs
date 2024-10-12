using System.Xml;
using HalloweenSystem.GameLogic.GameObjects;
using HalloweenSystem.GameLogic.RuleActions;
using HalloweenSystem.GameLogic.Selectors.GenericSelectors;
using HalloweenSystem.GameLogic.Selectors.RuleSelectors;
using HalloweenSystem.GameLogic.Selectors.TagSelectors;
using HalloweenSystem.GameLogic.Settings;

namespace HalloweenSystem.GameLogic.Parsing;

public static class Parser
{
	public static Setting LoadGame(string path)
	{
		var settingPath = path + "/setting.xml";
		var rulesPath = path + "/rules";

		var settingDoc = new XmlDocument();
		settingDoc.Load(settingPath);

		if (settingDoc.DocumentElement == null) throw new XmlException("Invalid setting file.");

		var setting = Setting.Parse(settingDoc.DocumentElement);

		var ruleFiles = Directory.GetFiles(rulesPath).ToList();


		var ruleNodes = ruleFiles.Select(file =>
		{
			var doc = new XmlDocument();
			doc.Load(file);
			return doc;
		});

		var validRules = ruleNodes.Where(doc => doc.DocumentElement != null)
			.Select(XmlNode (doc) => doc.DocumentElement!).Where(node => node.Attributes?["name"] != null)
			.Where(node => setting.RuleNames.Contains(node.Attributes!["name"]!.Value)).ToArray();
		
		if(setting.RuleNames.Count() != validRules.Length) throw new XmlException("Invalid rule file.");
		
		setting.LoadRules(validRules);

		return setting;
	}

	public static ISelector<T> ParseSelector<T>(XmlNode node) where T : GameObject, new()
	{
		return node.Name switch
		{
			"all" => AllSelector<T>.Parse(node),
			"union" => UnionSelector<T>.Parse(node),
			"chance" => ChanceSelector<T>.Parse(node),
			"complement" => ComplementSelector<T>.Parse(node),
			"random" => RandomSelector<T>.Parse(node),
			"tag" => (ISelector<T>)TagSelector.Parse(node),
			"rule" => (ISelector<T>)RuleSelector.Parse(node),
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
}