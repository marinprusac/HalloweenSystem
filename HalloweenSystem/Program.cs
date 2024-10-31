using HalloweenSystem.GameLogic.Parsing;
using HalloweenSystem.GameLogic.Utilities;


const string path = "/home/marin/Development/HalloweenSystem/HalloweenSystem/example";

var setting = Parser.LoadGame(path);

var playerTags = new Dictionary<string, List<string>>()
{
	{ "Colin Douglass", ["Old", "Social", "Commoner", "Tavern keeper"] },
	{ "Webster", ["Loner", "Foreigner", "Commoner", "Librarian"] },
	{ "Vyncis Lannister", ["Young", "Scary", "Aristocrat", "Army general"] },
	{ "'Crazy Eyes'", ["Old", "Scary", "Commoner", "Merchant"] },
	{ "Marina Ivanova", ["Social", "Foreigner", "Commoner", "Soldier"] },
	{ "Percival", ["Smart", "Foreigner", "Aristocrat", "Court mage"] },
	{ "Adeline Pandora Cavendish", ["Social", "Smart", "Aristocrat", "Inheritor"] },
	{ "Eleanor Charity Godwin", ["Social", "Romantic", "Aristocrat", "Inheritor"] },
	{ "Cedric Sulyvahn Lothric", ["Social", "Romantic", "Aristocrat", "Spymaster"] },
	{ "Queen Victoria", ["Queen"] },
	{ "Ezra", ["Young", "Loner", "Commoner", "Executioner"] },
	{ "Colonel Grimwood", ["Old", "Scary", "Commoner", "Soldier"] },
	{ "Josephine McCrea", ["Scary", "Foreigner", "Commoner", "Librarian"] },
	{ "Father Dominik Morgan", ["Social", "Smart", "Aristocrat", "Archbishop"] },
};


var ctx = Constrainer.Build()
	.AddConstraint(Constrainer.ContentDistributionGrade)
	.ForceGrade(setting, playerTags, 0.8f);

Console.WriteLine(ctx.ToString());