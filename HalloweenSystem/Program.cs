using HalloweenSystem.GameLogic.Parsing;


const string path = "/home/marin/Development/HalloweenSystem/HalloweenSystem/example";

var s = Parser.LoadGame(path);

var ctx = s.RunWithTags(new Dictionary<string, List<string>>()
{
	{"Marin", ["Old", "Social", "Commoner", "Tavern Keeper"]},
	{"Patrik", ["Loner", "Foreigner", "Commoner", "Librarian"]},
	{"Juraj", ["Young", "Scary", "Aristocrat", "Army general"]},
	{"Borna", ["Old", "Scary", "Commoner", "Merchant"]},
	{"Jelena", ["Social", "Foreigner", "Commoner", "Soldier"]},
	{"Tin", ["Smart", "Foreigner", "Aristocrat", "Court mage"]},
	{"Lucija", ["Smart", "Romantic", "Aristocrat", "Inheritor"]},
	{"Silvija", ["Scary", "Romantic", "Aristocrat", "Inheritor"]},
	{"Rino", ["Young", "Social", "Aristocrat", "Spymaster"]},
	{"Nika", ["Queen"]},
	{"Michelle", ["Old", "Loner", "Aristocrat", "Archbishop"]},
	{"Mathea", ["Young", "Social", "Commoner", "Mayor"]},
	{"Mia", ["Scary", "Foreigner", "Commoner", "Librarian"]},
});

Console.WriteLine(ctx.ToString());

