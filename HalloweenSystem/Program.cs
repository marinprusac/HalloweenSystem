using HalloweenSystem.GameLogic.Parsing;
using HalloweenSystem.GameLogic.Utilities;


const string path = "/home/marin/Development/HalloweenSystem/HalloweenSystem/example";

var setting = Parser.LoadGame(path);

var playerTags = new Dictionary<string, List<string>>()
{
	{ "Marin", ["Old", "Social", "Commoner", "Tavern keeper"] },
	{ "Patrik", ["Loner", "Foreigner", "Commoner", "Librarian"] },
	{ "Juraj", ["Young", "Scary", "Aristocrat", "Army general"] },
	{ "Borna", ["Old", "Scary", "Commoner", "Merchant"] },
	{ "Jelena", ["Social", "Foreigner", "Commoner", "Soldier"] },
	{ "Tin", ["Smart", "Foreigner", "Aristocrat", "Court mage"] },
	{ "Lucija", ["Social", "Smart", "Aristocrat", "Inheritor"] },
	{ "Silvija", ["Scary", "Romantic", "Aristocrat", "Inheritor"] },
	{ "Rino", ["Social", "Romantic", "Aristocrat", "Spymaster"] },
	{ "Nika", ["Queen"] },
	{ "Michelle", ["Young", "Loner", "Commoner", "Executioner"] },
	{ "Mathea", ["Old", "Scary", "Commoner", "Soldier"] },
	{ "Mia", ["Scary", "Foreigner", "Commoner", "Librarian"] },
};


var ctx = Constrainer.Build()
	.AddConstraint(Constrainer.ContentDistributionGrade)
	.ForceGrade(setting, playerTags, 0.8f);

Console.WriteLine(ctx.ToString());